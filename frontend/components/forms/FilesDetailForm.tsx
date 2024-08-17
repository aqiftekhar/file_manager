"use client";

import React from "react";
import { UseFormReturn } from "react-hook-form";
import { z } from "zod";
import { FileManagerFormValidation } from "@/lib/FormValidation";
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";

type FilesDetailFormProps = UseFormReturn<z.infer<typeof FileManagerFormValidation>>;

const FilesDetailForm: React.FC<FilesDetailFormProps> = ({ control }) => {

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
                showTimeSelect
                dateFormat="MM/dd/yyyy  -  h:mm aa"
                control={control}
            />
       </>
    );
};

export default FilesDetailForm;
