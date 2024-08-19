"use client";

import React, { useEffect, useState } from "react";
import { DataTable } from "../table/DataTable";
import { columns } from "../table/columns";
import CustomFormFields from "../CustomFormFields";
import { FormFieldTypes } from "@/lib/FormFieldTypes";
import Image from "next/image";
import { File, FilesFormProps } from "@/types/db.types";
import { SelectItem } from "../ui/select";

const FilesForm: React.FC<FilesFormProps> = ({ control, volumes, files, setIsFilterOpen, setSelectedFile, setFiles }) => {
    const [selectedVolume, setSelectedVolume] = useState<number | null>(null);
    const [filteredFiles, setFilteredFiles] = useState<File[]>(files || []);

    useEffect(() => {
        if (!selectedVolume) {
            setFilteredFiles(files || []);
        }
    }, [selectedVolume, files]);

    const handleVolumeChange = async (event: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedVolumeId = parseInt(event.target.value);
        if (selectedVolumeId === 0) {
            // "All" option selected, show all files
            setFilteredFiles(files || []);
        } else {

            setSelectedVolume(selectedVolumeId);
            const filteredFiles = files?.filter(file => file.volumeId === selectedVolumeId);

            setFilteredFiles(filteredFiles || []);
        }

    };

    return (
        <>
            <div className="flex flex-row items-center justify-items-center space-y-8 space-x-2">
                <CustomFormFields
                    fieldType={FormFieldTypes.SELECT}
                    name="volume"
                    label="Volume"
                    placeholder="Select a Volume"
                    control={control}
                    onChange={handleVolumeChange}
                >
                    <SelectItem key={0} value={"0"} className="hover:bg-dark-600 text-white">
                        <div className="flex cursor-pointer items-center gap-2">
                            <p>All</p>
                        </div>
                    </SelectItem>
                    {volumes.map(volume => (
                        <SelectItem key={volume.id} value={volume.id.toString()} className="hover:bg-dark-600 text-white">
                            <div className="flex cursor-pointer items-center gap-2">
                                <p>{volume.name}</p>
                            </div>
                        </SelectItem>
                    ))}

                </CustomFormFields>
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
                <DataTable columns={columns} data={filteredFiles || []} onRowSelect={(file) => setSelectedFile(file)} />
            </div>
        </>

    );
};

export default FilesForm;
