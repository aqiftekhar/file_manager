import { ICustomButtonProps } from "@/lib/ICustomButtonProps";
import React from "react";
import { Button } from "./ui/button";
import Image from "next/image";



const ShowPDFButton = ({ isLoading = false, className, children }: ICustomButtonProps) => {
    return (
        <Button type="button" disabled={isLoading} className={className ?? 'shad-primary-btn w-full'}>
            {isLoading ? (
                <div className="flex items-center gap-4">
                    <Image
                        src='/assets/icons/loader.svg'
                        alt="loader"
                        width={24}
                        height={24}
                        className="animate-spin"
                    />
                    Loading ...
                </div>
            ) : (children)
            }
        </Button>
    )
}

export default ShowPDFButton;
