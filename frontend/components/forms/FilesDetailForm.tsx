"use client";

import React, { useEffect } from "react";
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";
import { FilesDetailFormProps } from "@/types/db.types";

const FilesDetailForm: React.FC<FilesDetailFormProps> = ({ control, reset, selectedFile }) => {

    useEffect(() => {
        if (selectedFile) {
            reset({
                name: selectedFile.name || "",
                description: selectedFile.description || "",
                tags: "",
                savePaper: selectedFile.savePaper || false,
                createDate: selectedFile.createDate || new Date(),
            });
        }
    }, [selectedFile, control]);

    return (
       <>
       <CustomFormFields
                fieldType={FormFieldTypes.INPUT}
                name="name"
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
                name="savePaper"
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
                name="createDate"
                label="Created Date"
                dateFormat="MM/dd/yyyy"
                control={control}
            />
       </>
    );
};

export default FilesDetailForm;
