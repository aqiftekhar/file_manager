"use client";

import React, { useEffect } from "react";
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";
import { FilesDetailFormProps } from "@/types/db.types";

const FilesDetailForm: React.FC<FilesDetailFormProps> = ({ control, reset, selectedFile }) => {

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
    }, [selectedFile, control]);

    return (
       <>
       <CustomFormFields
                fieldType={FormFieldTypes.INPUT}
                name="title"
                label="Title"
                placeholder="Document's Title here"
                control={control}
            />
            <CustomFormFields
                fieldType={FormFieldTypes.TEXTAREA}
                name="description"
                label="Document Description"
                placeholder="Document's description here"
                control={control}
            />
            <CustomFormFields
                fieldType={FormFieldTypes.CHECKBOX}
                name="savepaper"
                label="Save Paper"
                control={control}
            />
            <CustomFormFields
                fieldType={FormFieldTypes.INPUT}
                name="tags"
                label="Tags"
                control={control}
            />
            <CustomFormFields
                fieldType={FormFieldTypes.DATE_PICKER}
                name="createdDate"
                label="Created Date"
                dateFormat="MM/dd/yyyy"
                control={control}
            />
       </>
    );
};

export default FilesDetailForm;
