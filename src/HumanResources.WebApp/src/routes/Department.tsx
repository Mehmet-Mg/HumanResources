import { ColDef } from '@ag-grid-community/core';
import { AgGridReact } from '@ag-grid-community/react';
import { useEffect, useState } from 'react';

interface IDepartment {
    departmentId: number;
    departmentName: string;
    managerId?: number;
    locationId?: number;
}

function Department() {
    const [departments, setDepartments] = useState<IDepartment[]>();
    const [colDefs, setColDefs] = useState<ColDef<IDepartment>[]>([
        { field: "departmentId" },
        { field: "departmentName" },
        { field: "managerId" },
        { field: "locationId" },
    ]);
    useEffect(() => {
        populateDepartmentsData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Departments</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <div
                className="ag-theme-quartz" // applying the Data Grid theme
                style={{ height: 650 }} // the Data Grid will fill the size of the parent container
            >
                <AgGridReact
                    rowData={departments}
                    columnDefs={colDefs}
                    loading={departments === undefined}
                />
            </div>
        </div>
    );

    async function populateDepartmentsData() {
        const response = await fetch('Department');
        const data = await response.json();
        setDepartments(data);
    }
}

export default Department;