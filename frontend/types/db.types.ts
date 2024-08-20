import {
  FileManagerFormValidation,
  VolumeFormValidation,
} from "@/lib/FormValidation";
import { SetStateAction } from "react";
import { UseFormReturn } from "react-hook-form";
import { z } from "zod";

export interface Tag {
  id: number;
  tagName: string;
}

export interface TagAssignment {
  fileId: number;
  tagId: number;
  file: File;
  tag: Tag;
}

export interface File {
  id: number;
  volumeId: number;
  name: string;
  description: string | null;
  savePaper: boolean;
  createDate: Date;
  modifyDate: Date;
  binaryData: Buffer;
  tagAssignments?: TagAssignment[];
  tags?: { $values: Tag[] };
  volume?: Volume;
}

export interface SaveFileProps {
  // Id: number;
  VolumeId: number;
  Name: string;
  Description: string | null;
  SavePaper: boolean;
  CreateDate: Date;
  ModifyDate: Date;
  BinaryData: Buffer;
  tagAssignments?: TagAssignment[];
  tags?: Tag[];
  volume?: Volume;
}

export interface Volume {
  id: number;
  name: string;
  description: string | null;
  files: {
    $values: File[];
  };
}

export interface FilterFormProps {
  closeDialog: () => void;
  files: Array<{
    id: number;
    volumeId: number;
    name: string;
    description: string | null;
    savePaper: boolean;
    createDate: Date;
    modifyDate: Date;
    binaryData: Buffer;
    tagAssignments?: TagAssignment[];
    tags?: { $values: Tag[] };
    volume?: Volume;
  }> | null;
  updateFilteredFiles: (filteredFiles: File[] | null) => void;
}
export type FilesFormProps = UseFormReturn<
  z.infer<typeof FileManagerFormValidation>
> & {
  files: Array<{
    id: number;
    volumeId: number;
    name: string;
    description: string | null;
    savePaper: boolean;
    createDate: Date;
    modifyDate: Date;
    binaryData: Buffer;
    tagAssignments?: TagAssignment[];
    tags?: { $values: Tag[] };
    volume?: Volume;
  }> | null;
  volumes: Volume[];
  setIsFilterOpen: React.Dispatch<React.SetStateAction<boolean>>;
  setSelectedFile: React.Dispatch<SetStateAction<File | null>>;
  setFiles: React.Dispatch<React.SetStateAction<File[] | null>>;
};

export type FilesDetailFormProps = UseFormReturn<
  z.infer<typeof FileManagerFormValidation>
> & {
  reset: (values?: Partial<z.infer<typeof FileManagerFormValidation>>) => void;
  selectedFile?: File | null;
};
