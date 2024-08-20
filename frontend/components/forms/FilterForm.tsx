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

const FilterForm: React.FC<FilterFormProps> = ({ closeDialog, files, updateFilteredFiles }) => {
    const formMethods = useForm<z.infer<typeof FilterFormValidation>>({
        resolver: zodResolver(FilterFormValidation),
        defaultValues: {
            ...FilterFormValidation,
            name: "",
            description: "",
            hasTags: "",
            tags: "",
            createdCondition: "",
            createDate: null,
        },
    });

    const onSubmit = (values: z.infer<typeof FilterFormValidation>) => {
        // Extract filter values
        const filterDate = values.createDate ? new Date(values.createDate) : null;
        const filterCondition = values.createdCondition || '';
        const tagsFilter = values.tags ? values.tags.split(',').map(tag => tag.trim().toLowerCase()) : [];
        const hasTags = values.hasTags || '';
    
        // Filter files
        const filtered = files?.filter(file => {
    
            // Check `name` filter
            if (values.name && !file.name.toLowerCase().includes(values.name.toLowerCase())) {
                return false;
            }
    
            // Check `description` filter
            if (values.description && !file.description?.toLowerCase().includes(values.description.toLowerCase())) {
                return false;
            }
    
            // Check `tags` filter if `hasTags` is set
            const fileTags = file.tags?.$values.map(tag => tag.tagName.toLowerCase()) || [];
            if (hasTags) {
                switch (hasTags) {
                    case 'All':
                        if (!tagsFilter.every(tag => fileTags.includes(tag))) {
                            return false;
                        }
                        break;
                    case 'Any':
                        if (!tagsFilter.some(tag => fileTags.includes(tag))) {
                            return false;
                        }
                        break;
                    default:
                        return false;
                }
            }
    
            // Check `createDate` filter if `filterDate` is set
            if (filterDate) {
                const fileDate = new Date(file.createDate);
                switch (filterCondition) {
                    case 'Before':
                        if (!(fileDate < filterDate)) {
                            return false;
                        }
                        break;
                    case 'After':
                        if (!(fileDate > filterDate)) {
                            return false;
                        }
                        break;
                    case 'Equal':
                        if (!(fileDate.toDateString() === filterDate.toDateString())) {
                            return false;
                        }
                        break;
                    default:
                        return false;
                }
            }
    
            return true; // Include file if it passes all filters
        });
    
        updateFilteredFiles(filtered || []); // Update the filtered files
        closeDialog();
    };
    
    
    const handleClear = () => {
        updateFilteredFiles(files || []);
        formMethods.reset();  
    };
    return (
        <FormProvider {...formMethods}>
            <form onSubmit={formMethods.handleSubmit(onSubmit)} className="flex-1 space-y-6">
                <CustomFormFields
                    fieldType={FormFieldTypes.INPUT}
                    name="name"
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
                        <label className="mb-0" htmlFor="hasTags">Has Tags</label>
                    </div>
                    <div className="flex flex-col w-[80px]">
                        <CustomFormFields
                            fieldType={FormFieldTypes.SELECT}
                            name="hasTags"
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
                        <label className="mb-0" htmlFor="createdCondition">Created Date:</label>
                    </div>
                    <div className="flex flex-col w-[80px]">
                        <CustomFormFields
                            fieldType={FormFieldTypes.SELECT}
                            name="createdCondition"
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
                            name="createDate"
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
