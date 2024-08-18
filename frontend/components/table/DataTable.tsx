"use client";

import {
    getPaginationRowModel,
    ColumnDef,
    ColumnFiltersState,
    flexRender,
    getCoreRowModel,
    getFilteredRowModel,
    useReactTable,
} from "@tanstack/react-table";
import Image from "next/image";
import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import React from "react";

interface DataTableProps<TData, TValue> {
    columns: ColumnDef<TData, TValue>[];
    data: TData[];
    onRowSelect?: (row: TData) => void;
}

export function DataTable<TData, TValue>({
    columns,
    data,
    onRowSelect,
}: DataTableProps<TData, TValue>) {

    const [columnFilters, setColumnFilters] = React.useState<ColumnFiltersState>([]);
    const [selectedRowId, setSelectedRowId] = useState<string | null>(null); 


    const table = useReactTable({
        data,
        columns,
        getCoreRowModel: getCoreRowModel(),
        getPaginationRowModel: getPaginationRowModel(),
        onColumnFiltersChange: setColumnFilters,
        getFilteredRowModel: getFilteredRowModel(),
        state: {
            columnFilters,
        },
    });

    const handleRowClick = (rowId: string, rowValues: TData) => {
        setSelectedRowId(rowId);
        if (onRowSelect) {
            onRowSelect(rowValues); 
        }
    };

    return (
        <div className="data-table">
            <div className="flex items-center py-4 justify-end">
                <Input
                    placeholder="Filter Files..."
                    value={(table.getColumn("name")?.getFilterValue() as string) ?? ""}
                    onChange={(event) =>
                        table.getColumn("name")?.setFilterValue(event.target.value)
                    }
                    className="max-w-sm"
                />
            </div>
            <Table className="shad-table">
                <TableHeader className="bg-dark-200">
                    {table.getHeaderGroups().map((headerGroup) => (
                        <TableRow key={headerGroup.id} className="shad-table-row-header">
                            {headerGroup.headers.map((header) => {
                                return (
                                    <TableHead key={header.id}>
                                        {header.isPlaceholder
                                            ? null
                                            : flexRender(
                                                header.column.columnDef.header,
                                                header.getContext()
                                            )}
                                    </TableHead>
                                );
                            })}
                        </TableRow>
                    ))}
                </TableHeader>
                <TableBody>
                    {table.getRowModel().rows?.length ? (
                        table.getRowModel().rows.map((row) => (
                            <TableRow
                                key={row.id}
                                onClick={() => handleRowClick(row.id, row.original)}
                                className={`shad-table-row ${
                                    selectedRowId === row.id ? "bg-dark-600 text-white" : "hover:bg-dark-600 text-white"
                                }`}
                            >
                                {row.getVisibleCells().map((cell) => (
                                    <TableCell key={cell.id}>
                                        {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                    </TableCell>
                                ))}
                            </TableRow>
                        ))
                    ) : (
                        <TableRow>
                            <TableCell colSpan={columns.length} className="h-24 text-center">
                                No results.
                            </TableCell>
                        </TableRow>
                    )}
                </TableBody>
            </Table>
            <div className="table-actions">
                <Button
                    variant="outline"
                    size="sm"
                    onClick={(event) => {
                        event.preventDefault();
                        table.previousPage();
                    }}
                    disabled={!table.getCanPreviousPage()}
                    className="shad-gray-btn"
                >
                    <Image
                        src="/assets/icons/arrow.svg"
                        width={24}
                        height={24}
                        alt="arrow"
                    />
                </Button>
                <Button
                    variant="outline"
                    size="sm"
                    onClick={(event) => {
                        event.preventDefault();
                        table.nextPage();
                    }}
                    disabled={!table.getCanNextPage()}
                    className="shad-gray-btn"
                >
                    <Image
                        src="/assets/icons/arrow.svg"
                        width={24}
                        height={24}
                        alt="arrow "
                        className="rotate-180"
                    />
                </Button>
            </div>
        </div>
    );
}



