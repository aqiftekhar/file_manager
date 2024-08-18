"use client";

import { FilterFormValidation } from "@/lib/FormValidation";
import { zodResolver } from "@hookform/resolvers/zod";
import React from "react";
import { useForm, FormProvider } from "react-hook-form";
import { z } from "zod";
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";
import SubmitButton from "../SubmitButton";
import { SelectItem } from "../ui/select";
import CustomButton from "../CustomButton";
import { FilterFormProps } from "@/types/db.types";

const FilterForm: React.FC<FilterFormProps> = ({ closeDialog }) => {
    const formMethods = useForm<z.infer<typeof FilterFormValidation>>({
        resolver: zodResolver(FilterFormValidation),
        defaultValues: {
            ...FilterFormValidation,
            title: "",
            description: "",
            tags: "",
            createdDate: new Date(),
        },
    });

    async function onSubmit(values: z.infer<typeof FilterFormValidation>) {
        try {
            console.log("Submit details", values);
        } catch (error) {
            console.log(error);
        }
    }
    const handleClear = () => {
        formMethods.reset();  
    };
    return (
        <FormProvider {...formMethods}>
            <form onSubmit={formMethods.handleSubmit(onSubmit)} className="flex-1 space-y-6">
                <CustomFormFields
                    fieldType={FormFieldTypes.INPUT}
                    name="title"
                    label="Title Contains"
                    placeholder="Document's Title here"
                    control={formMethods.control}
                />
                <CustomFormFields
                    fieldType={FormFieldTypes.INPUT}
                    name="description"
                    label="Description Contains"
                    placeholder="Document's description here"
                    control={formMethods.control}
                />

                <div className="flex flex-row space-x-2">
                    <div className="flex flex-col w-[120px] justify-center">
                        <label className="mb-0" htmlFor="hastags">Has Tags</label>
                    </div>
                    <div className="flex flex-col w-[80px]">
                        <CustomFormFields
                            fieldType={FormFieldTypes.SELECT}
                            name="hastags"
                            placeholder="Select all tags or filter"
                            control={formMethods.control}
                        >
                            <SelectItem value="All" aria-selected>
                                All
                            </SelectItem>
                            <SelectItem value="Any">
                                Any
                            </SelectItem>
                        </CustomFormFields>
                    </div>
                    <div className="flex flex-col w-full">
                        <CustomFormFields
                            fieldType={FormFieldTypes.INPUT}
                            name="tags"
                            placeholder="Document's tags here"
                            control={formMethods.control}
                        />
                    </div>
                </div>

                <div className="flex flex-row space-x-2">
                    <div className="flex flex-col w-[120px] justify-center">
                        <label className="mb-0" htmlFor="createdcondition">Created Date:</label>
                    </div>
                    <div className="flex flex-col w-[80px]">
                        <CustomFormFields
                            fieldType={FormFieldTypes.SELECT}
                            name="createdcondition"
                            placeholder="Select created condition"
                            control={formMethods.control}
                        >
                            <SelectItem value="After">
                                After
                            </SelectItem>
                            <SelectItem value="Before">
                                Before
                            </SelectItem>
                            <SelectItem value="Equal">
                                Equal To
                            </SelectItem>
                        </CustomFormFields>
                    </div>
                    <div className="flex flex-col w-full">
                        <CustomFormFields
                            fieldType={FormFieldTypes.DATE_PICKER}
                            name="createdDate"
                            label=""
                            dateFormat="MM/dd/yyyy"
                            control={formMethods.control}
                        />
                    </div>
                </div>
                <div className="flex flex-row space-x-2 justify-end">
                <CustomButton isLoading={false} className="shad-danger-btn" onclick={closeDialog}>Close</CustomButton>
                <CustomButton isLoading={false} className="shad-gray-btn" onclick={handleClear}>Clear</CustomButton>
                    <SubmitButton isLoading={false} className="shad-primary-btn">
                        Submit
                    </SubmitButton>
                </div>

            </form>
        </FormProvider>
    );
};

export default FilterForm;
