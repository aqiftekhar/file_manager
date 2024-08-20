import { File, SaveFileProps } from "@/types/db.types";

export const getFilesByVolumeId = async (
    volumeId: number
  ): Promise<File[] | null> => {
    try {
      const response = await fetch(
        `http://localhost:5065/api/file/filter/${volumeId}`
      );
  
      if (!response.ok) {
        throw new Error("Network response was not ok.");
      }
  
      const data = await response.json();
      return data.$values;
    } catch (error) {
      console.error("Error fetching files:", error);
      return null;
    }
  };

export const getFiles = async (): Promise<File[] | null> => {
  try {
    const response = await fetch(`http://localhost:5065/api/file`);

    if (!response.ok) {
      throw new Error("Network response was not ok.");
    }

    const data = await response.json();
    return data as File[];
  } catch (error) {
    console.error("Error fetching files:", error);
    return null;
  }
};

export const saveFile = async (formData: FormData, file_data: SaveFileProps): Promise<void> => {
    try {
        formData.append('fileDataJson', JSON.stringify(file_data));
        
        const response = await fetch('http://localhost:5065/api/file', {
            method: 'POST',
            body: formData,
        });

        if (!response.ok) {
            throw new Error("Network response was not ok.");
        }

        const data = await response.json();
    } catch (error) {
        console.error("Error posting file:", error);
    }
};
