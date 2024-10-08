import { Control } from "react-hook-form";
import { FormFieldTypes } from "./FormFieldTypes";

export interface IFormCustomProps {
  control: Control<any>;
  fieldType: FormFieldTypes;
  name: string;
  label?: string;
  placeholder?: string;
  iconSrc?: string;
  iconAlt?: string;
  disabled?: boolean;
  dateFormat?: string;
  showTimeSelect?: boolean;
  children?: React.ReactNode;
  renderSkeleton?: (field: any) => React.ReactNode;
  onChange?: (event: React.ChangeEvent<any>) => void; // Add this line
}
