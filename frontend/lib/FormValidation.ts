import { z } from "zod";

export const FileManagerFormValidation = z.object({
    volume: z.string().min(2, "Select at least one volume"),
  name: z
    .string()
    .min(10, "Title must be at least 10 characters")
    .max(50, "Title must be at most 50 characters"),
  description: z.string().min(20, "Description must be at least 20 characters"),
  savePaper: z.boolean().default(false).optional(),
  tags: z.string().min(20, "Tags must be at least 20 characters"),
  createdDate: z.coerce.date(),
});

export const VolumeFormValidation = z.object({
  name: z.string().min(2, "Select at least one volume"),
});

export const FilterFormValidation = z.object({
  name: 
  z.string()
  .min(10, "Title must be at least 10 characters")
  .max(50, "Title must be at most 50 characters"),

  description: 
  z.string()
  .min(20, "Description must be at least 20 characters"),

  tags: z.string().min(20, "tags must be at least 20 characters"),

  createdDate: z.coerce.date(),
  // createdcondition: z.coerce.string().optional(),
  // hastags: z.coerce.string().optional()
});