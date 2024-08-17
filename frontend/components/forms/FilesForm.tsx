"use client";

import React, { useState } from "react";
import { DataTable } from "../table/DataTable";
import { columns } from "../table/columns";
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";
import Image from "next/image";
import { FilesFormProps } from "@/types/db.types";

const FilesForm: React.FC<FilesFormProps> = ({ control, dummyFiles, setIsFilterOpen }) => {
    return (
        <>
            <div className="flex flex-row items-center justify-items-center space-y-8 space-x-2">
                <CustomFormFields
                    fieldType={FormFieldTypes.SELECT}
                    name="volume"
                    label="Volume"
                    placeholder="Select a Volume"
                    control={control}
                />
                <button onClick={(e) => {
                    e.preventDefault();
                    setIsFilterOpen(true)
                }
                }
                    className="cursor-pointer items-center">
                    <Image
                        src="/assets/icons/filter.svg"
                        height={32}
                        width={32}
                        alt="Filter"
                        className="h-8 w-fit"
                    />
                </button>
            </div>

            <div className="h-screen overflow-y-auto w-full">
                <DataTable columns={columns} data={dummyFiles} />
            </div>
        </>

    );
};

export default FilesForm;
