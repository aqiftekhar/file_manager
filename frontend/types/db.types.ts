import { FileManagerFormValidation, VolumeFormValidation } from "@/lib/FormValidation";
import { SetStateAction } from "react";
import { UseFormReturn } from "react-hook-form";
import { z } from "zod";

export interface Volume {
  id: number,
  name: string,
  description: string | null
}

export interface Files {
  id: number;
  volumeId: number;
  name: string;
  description: string | null;
  savepaper: boolean;
  createDate: Date;
  modifyDate: Date;
  binaryData: Buffer;
}

export interface FilterFormProps {
  closeDialog: () => void;
}
export type FilesFormProps = UseFormReturn<
  z.infer<typeof FileManagerFormValidation>
> & {
  dummyFiles: Array<{
    id: number;
    volumeId: number;
    name: string;
    description: string | null;
    savepaper: boolean;
    createDate: Date;
    modifyDate: Date;
    binaryData: Buffer;
  }>;
  setIsFilterOpen: React.Dispatch<React.SetStateAction<boolean>>;
  setSelectedFile: React.Dispatch<SetStateAction<Files | null>>;
};

export type FilesDetailFormProps = UseFormReturn<z.infer<typeof FileManagerFormValidation>> & {
  reset: (values?: Partial<z.infer<typeof FileManagerFormValidation>>) => void;
  selectedFile?: Files | null; 
};