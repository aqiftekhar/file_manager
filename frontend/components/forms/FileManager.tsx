"use client";

import React from "react";
import { useForm } from "react-hook-form";
import { Form, FormControl } from "@/components/ui/form"
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { FileManagerFormValidation } from "@/lib/FormValidation";
import { DataTable } from "../table/DataTable";
import { columns } from "../table/columns";
import ShowPDFButton from "../ShowPDFButton";

const FileManager = () => {

    const dummyFiles = [
        {
            id: 1,
            volumeId: 1,
            name: "File 1",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 2",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 3,
            volumeId: 1,
            name: "File 3",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 4,
            volumeId: 1,
            name: "File 4",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 5",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 6",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 7",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 8",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 9",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 10",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 11",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 12",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 13",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 14",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 15",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 16",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 17",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 18",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 19",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 20",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 21",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 22",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 1,
            volumeId: 1,
            name: "File 23",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        },
        {
            id: 2,
            volumeId: 1,
            name: "File 24",
            description: null,
            savePaper: false,
            createDate: new Date(),
            modifyDate: new Date(),
            binaryData: Buffer.from(''),
        }
    ];
    // 1. Define your form.
    const form = useForm<z.infer<typeof FileManagerFormValidation>>({
        resolver: zodResolver(FileManagerFormValidation),
        defaultValues: {
            ...FileManagerFormValidation,
            volume: "",
            title: "",
            description: "",
            tags: "",
            savePaper: false,
        },
    })

    // 2. Define a submit handler.
    async function onSubmit(values: z.infer<typeof FileManagerFormValidation>) {
        // Do something with the form values.
        // ✅ This will be type-safe and validated.

        try {
            console.log("first");

        }
        catch (error) {
            console.log(error)
        }
    }
    return (
        <div className="flex h-screen max-h-screen" >
            <section className="remove-scrollbar container my-auto">
                <div className="sub-container w-full flex-1 flex-col py-10 ml-5 mt-5">
                    <Form {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)} className=" flex flex-col md:flex-row w-full">
                            <div className="flex-1 w-full md:w-1/2 space-y-5">
                                <CustomFormFields
                                    fieldType={FormFieldTypes.SELECT}
                                    name="volume"
                                    label="Volume"
                                    placeholder="Select a Volume"
                                    control={form.control}
                                />
                                <div className="h-screen overflow-y-auto w-full">
                                    <DataTable columns={columns} data={dummyFiles} />
                                </div>
                            </div>
                            <div
                                className="w-full md:w-1/2 md:ml-5 mt-5 md:mt-0 space-y-5"
                            >
                                <CustomFormFields
                                    fieldType={FormFieldTypes.INPUT}
                                    name="title"
                                    label="Title"
                                    placeholder="Document's Title here"
                                    control={form.control}
                                />
                                <CustomFormFields
                                    fieldType={FormFieldTypes.TEXTAREA}
                                    name="description"
                                    label="Document Description"
                                    placeholder="Document's description here"
                                    control={form.control}
                                />
                                <CustomFormFields
                                    fieldType={FormFieldTypes.CHECKBOX}
                                    control={form.control}
                                    name="savepaper"
                                    label="Save Paper"
                                />
                                <CustomFormFields
                                    fieldType={FormFieldTypes.INPUT}
                                    control={form.control}
                                    name="tags"
                                    label="Tags"
                                />
                                <CustomFormFields
                                    fieldType={FormFieldTypes.DATE_PICKER}
                                    control={form.control}
                                    name="createdDate"
                                    label="Created Date"
                                    showTimeSelect
                                    dateFormat="MM/dd/yyyy  -  h:mm aa"
                                />

                                <div className="flex flex-col xs:flex-row py-20 justify-between">
                                    <div className="flex gap-2">
                                        <ShowPDFButton isLoading={false} >
                                            Show PDF
                                        </ShowPDFButton>
                                        <div className="hidden md:block md:w-[280px]"></div>
                                        <ShowPDFButton isLoading={false}  >
                                            Download
                                        </ShowPDFButton>
                                        <ShowPDFButton isLoading={false} >
                                            Upload
                                        </ShowPDFButton>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </Form>
                </div>
            </section>
        </div>


    );
};

export default FileManager;
