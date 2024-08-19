import { Volume } from "@/types/db.types";

export const getVolumes = async (): Promise<{
  $id: string;
  $values: Volume[];
} | null> => {
  try {
    const response = await fetch("http://localhost:5065/api/file/volumes");
    //   console.log("response = ", response);

    if (!response.ok) {
      throw new Error("Network response was not ok.");
    }

    const volumes = await response.json();

    return volumes;
  } catch (error) {
    console.error("An error occurred while retrieving volumes:", error);
    return null;
  }
};
