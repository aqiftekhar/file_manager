"use client"

import { Files } from "@/types/db.types";
import { ColumnDef } from "@tanstack/react-table"


export const columns: ColumnDef<Files>[] = [
  {
    accessorKey: "name",
    header: "Name",
    cell: ({ row }) => {
        return <p className="text-14-medium text-left ">{row.original.name}</p>;
      },
  },
  {
    accessorKey: "createDate",
    header: () => <div style={{ textAlign: 'right' }}>Created Date</div>,
    cell: ({ row }) => {
        const createDate = new Date(row.original.createDate);
        return <p className="text-14-medium text-right">{createDate.toLocaleDateString('en-US', { year: '2-digit', month: '2-digit', day: '2-digit' })}</p>;
      },
  },
]
