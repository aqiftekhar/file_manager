export interface Files {
    id: number; 
    volumeId: number; 
    name: string; 
    description: string | null; 
    savePaper: boolean; 
    createDate: Date; 
    modifyDate: Date; 
    binaryData: Buffer; 
  }