"use client";

import React, { useEffect } from "react";
import { useForm, UseFormReturn } from "react-hook-form";
import { z } from "zod";
import { FileManagerFormValidation } from "@/lib/FormValidation";
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";
import { Files } from "@/types/db.types";


type FilesDetailFormProps = UseFormReturn<z.infer<typeof FileManagerFormValidation>> & {
    selectedFile?: Files | null; 
};
const FilesDetailForm: React.FC<FilesDetailFormProps> = ({ control, selectedFile }) => {

    console.log( selectedFile?.createDate);
    
    const form = useForm({
        defaultValues: {
            title: "",
            description: "",
            tags: "",
            savepaper: false,
            createdDate: new Date(),
        },
    });

    const { reset } = form;

    useEffect(() => {
        if (selectedFile) {
            reset({
                title: selectedFile.name || "",
                description: selectedFile.description || "",
                tags: "",
                savepaper: selectedFile.savepaper || false,
                createdDate: selectedFile.createDate || new Date(),
            });
        }
    }, [selectedFile, reset]);
    return (
       <>
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
                name="savepaper"
                label="Save Paper"
                control={form.control}
            />
            <CustomFormFields
                fieldType={FormFieldTypes.INPUT}
                name="tags"
                label="Tags"
                control={form.control}
            />
            <CustomFormFields
                fieldType={FormFieldTypes.DATE_PICKER}
                name="createdDate"
                label="Created Date"
                dateFormat="MM/dd/yyyy"
                control={form.control}
            />
       </>
    );
};

export default FilesDetailForm;
