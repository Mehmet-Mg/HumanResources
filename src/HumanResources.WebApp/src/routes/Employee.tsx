import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ColDef, ModuleRegistry } from '@ag-grid-community/core';
import { AgGridReact } from '@ag-grid-community/react';
import { useEffect, useState } from 'react';

ModuleRegistry.registerModules([ClientSideRowModelModule]);

interface IEmployee {
    employeeId: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    jobId: string;
    salary: number;
    commissionPercent?: number;
    managerId?: number;
    departmentId?: number;
}

function Employee() {
    const [employees, setEmployees] = useState<IEmployee[]>();
    const [colDefs, setColDefs] = useState<ColDef<IEmployee>[]>([
        { field: "employeeId" },
        { field: "firstName" },
        { field: "lastName" },
        { field: "email" },
        { field: "phoneNumber" },
        { field: "hireDate" },
        { field: "jobId" },
        { field: "salary" },
        { field: "commissionPercent" },
        { field: "managerId" },
        { field: "departmentId" },
    ]);
    useEffect(() => {
        populateEmployeesData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Employees</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <div
                className="ag-theme-quartz" // applying the Data Grid theme
                style={{ height: 650 }} // the Data Grid will fill the size of the parent container
            >
                <AgGridReact
                    rowData={employees}
                    columnDefs={colDefs}
                    loading={employees === undefined}
                />
            </div>
        </div>
    );

    async function populateEmployeesData() {
        const response = await fetch('Employee');
        const data = await response.json();
        setEmployees(data);
    }
}

export default Employee;