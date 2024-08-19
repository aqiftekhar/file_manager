"use client";

import React, { useEffect, useRef, useState } from "react";
import FilesForm from "./FilesForm";
import FilesDetailForm from "./FilesDetailForm";
import { FormProvider, useForm } from "react-hook-form";
import CustomButton from "../CustomButton";
import SubmitButton from "../SubmitButton";
import { FileManagerFormValidation } from "@/lib/FormValidation";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import FilterForm from "./FilterForm";

import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
} from "@/components/ui/dialog";
import { File, SaveFileProps, Volume } from "@/types/db.types";
import { getVolumes } from "@/lib/data/volume.actions";
import { getFiles, saveFile } from "@/lib/data/files.actions";

const FileManager = () => {
    const pdfContainerRef = useRef(null);
    const [isFilterOpen, setIsFilterOpen] = useState(false);
    const [selectedFile, setSelectedFile] = useState<File | null>(null);
    const [volumes, setVolumes] = useState<Volume[]>([]);
    const [files, setFiles] = useState<File[] | null>(null);

    useEffect(() => {
        const fetchVolumes = async () => {
            const data = await getVolumes();
            if (data) {
                setVolumes(data.$values);
                const extractedFiles = data.$values.flatMap(volume => volume.files.$values);

                setFiles(extractedFiles);
            }
        };
        fetchVolumes();
    }, []);

    const form = useForm<z.infer<typeof FileManagerFormValidation>>({
        resolver: zodResolver(FileManagerFormValidation),
        defaultValues: {
            ...FileManagerFormValidation,
            name: "",
            description: "",
            tags: "",
            savePaper: false,
            createdDate: new Date(),
        },
    });

    const { reset, handleSubmit } = form;

    async function onSubmit(values: z.infer<typeof FileManagerFormValidation>) {
        try {
            console.log("Submit details");
        } catch (error) {
            console.log(error);
        }
    }

    const closeDialog = () => {
        setIsFilterOpen(false);
    };


    const handelShowPDF = () => {
        if (selectedFile && selectedFile.binaryData) {
            try {
                const binaryDataString = selectedFile.binaryData.toString('base64');
                console.log("Binary Data String:", binaryDataString);

                const iframe = document.createElement('iframe');
                iframe.src = `data:application/pdf;base64,${binaryDataString}`;
                iframe.frameBorder = '0';
                iframe.width = '100%';
                iframe.height = '100%';

                if (pdfContainerRef.current) {
                    (pdfContainerRef.current as HTMLElement).innerHTML = '';
                    (pdfContainerRef.current as HTMLElement).appendChild(iframe);
                } else {
                    console.error("Element with ID 'pdf-container' not found");
                }

                const newTab = window.open();
                newTab?.document.body.appendChild(iframe);
            } catch (error) {
                console.error("Error loading PDF:", error);
            }
        } else {
            console.error("No file selected or binary data is missing.");
        }
    };


    const handleDownload = async () => {
        if (selectedFile && selectedFile?.binaryData) {
            try {
                const binaryDataString = selectedFile.binaryData.toString('base64');

                const link = document.createElement('a');

                link.href = `data:application/pdf;base64,${binaryDataString}`;
                link.download = `${selectedFile.name}.pdf` || 'download.pdf'
                document.body.appendChild(link);
                link.click();

                document.body.removeChild(link);


            } catch (error) {
                console.error("Error downloading PDF:", error);
            }
        } else {
            console.error("No File selected or Binary data is missing.")
        }
    };

    // function handleUpload(): void {
    //     const pdfUrl = "https://pdfobject.com/pdf/sample.pdf";

    //     fetch(pdfUrl)
    //         .then((response) => response.blob())
    //         .then((blob) => {
    //             const formData = new FormData();
    //             formData.append('file', blob, 'downloaded.pdf'); 

    //             const file_data: SaveFileProps = {
    //                 VolumeId: 1,
    //                 Name: 'Sample PDF document',
    //                 Description: 'Description of the PDF',
    //                 SavePaper: true,
    //                 CreateDate: new Date(),
    //                 ModifyDate: new Date(),
    //                 BinaryData: Buffer.from('') 
    //             };

    //             saveFile(formData, file_data);
    //         })
    //         .catch((error) => {
    //             console.error("Error fetching PDF:", error);
    //         });
    // }


    return (
        <div className="flex h-screen max-h-screen">
            <section className="remove-scrollbar container my-auto">
                <div className="sub-container w-full flex-1 flex-col py-10 ml-5 mt-5">

                    <FormProvider {...form}>
                        <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col md:flex-row w-full">
                            <div className="flex-1 w-full md:w-1/2 space-y-5">
                                <FilesForm {...form} volumes={volumes} files={files} setIsFilterOpen={setIsFilterOpen} setSelectedFile={setSelectedFile} setFiles={setFiles} />
                            </div>
                            <div className="w-full md:w-1/2 md:ml-5 mt-5 md:mt-0 space-y-5">
                                <FilesDetailForm {...form} reset={reset} selectedFile={selectedFile} />

                                <div className="flex flex-col xs:flex-row py-20 justify-between">
                                    <div className="flex gap-2">
                                        <CustomButton isLoading={false} className="shad-danger-btn" onclick={handelShowPDF}>Show PDF</CustomButton>
                                        <div className="hidden md:block md:w-[280px]"></div>
                                        <CustomButton isLoading={false} className="shad-gray-btn" onclick={handleDownload}>Download</CustomButton>
                                        <SubmitButton isLoading={false} className="shad-primary-btn">Upload</SubmitButton>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </FormProvider>
                    <Dialog open={isFilterOpen} onOpenChange={setIsFilterOpen}>
                        <DialogContent className="shad-dialog sm:max-w-md">
                            <DialogHeader className="mb-4 space-y-3">
                                <DialogTitle className="capitalize">Filter Files</DialogTitle>
                                <DialogDescription>
                                    Please fill in the following details to filter Files.
                                </DialogDescription>
                            </DialogHeader>
                            <FilterForm closeDialog={closeDialog} />
                        </DialogContent>
                    </Dialog>
                    <div ref={pdfContainerRef} id="pdf-container"></div>
                </div>
            </section>
        </div>
    );
};

export default FileManager;

