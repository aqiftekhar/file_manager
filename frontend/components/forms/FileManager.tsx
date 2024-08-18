"use client";

import React, { useState } from "react";
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
import { Files } from "@/types/db.types";

const FileManager = () => {
    const [isFilterOpen, setIsFilterOpen] = useState(false);
    const[selectedFile, setSelectedFile] = useState<Files | null>(null);


    const dummyFiles = [
        {
            id: 1,
            volumeId: 1,
            name: "File 1",
            description: "This is description for file 1",
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 2",
            description: "This is description for file 2",
            savepaper: true,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 3,
            volumeId: 1,
            name: "File 3",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 4,
            volumeId: 1,
            name: "File 4",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 5",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 6",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 7",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 8",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 9",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 10",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 11",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 12",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 13",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 14",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 15",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 16",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 17",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 18",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 19",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 20",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 21",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 22",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 23",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 24",
            description: null,
            savepaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        }
    ];
    const form = useForm<z.infer<typeof FileManagerFormValidation>>({
        resolver: zodResolver(FileManagerFormValidation),
        defaultValues: {
            ...FileManagerFormValidation,
            title: "",
            description: "",
            tags: "",
            savepaper: false,
            createdDate: new Date(),
        },
    });

    async function onSubmit(values: z.infer<typeof FileManagerFormValidation>) {
        try {
            console.log("Submit details");
        } catch (error) {
            console.log(error);
        }
    }
    return (
        <div className="flex h-screen max-h-screen">
            <section className="remove-scrollbar container my-auto">
                <div className="sub-container w-full flex-1 flex-col py-10 ml-5 mt-5">

                    <FormProvider {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col md:flex-row w-full">
                            <div className="flex-1 w-full md:w-1/2 space-y-5">
                                <FilesForm {...form} dummyFiles={dummyFiles} setIsFilterOpen={setIsFilterOpen} setSelectedFile = {setSelectedFile} />
                            </div>
                            <div className="w-full md:w-1/2 md:ml-5 mt-5 md:mt-0 space-y-5">
                                <FilesDetailForm {...form} selectedFile={selectedFile}/>

                                <div className="flex flex-col xs:flex-row py-20 justify-between">
                                    <div className="flex gap-2">
                                        <CustomButton isLoading={false} className="shad-danger-btn">Show PDF</CustomButton>
                                        <div className="hidden md:block md:w-[280px]"></div>
                                        <CustomButton isLoading={false} className="shad-gray-btn">Download</CustomButton>
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
                            <FilterForm />
                        </DialogContent>
                    </Dialog>
                </div>
            </section>
        </div>
    );
};

export default FileManager;

