import { AgGridReact } from '@ag-grid-community/react';
import { ColDef } from '@ag-grid-community/core';
import { useEffect, useState } from 'react';

interface IJobHistory {
    employeeId: string;
    startDate: Date;
    endDate: Date;
    jobId: string;
    departmentId?: number;
}

function JobHistory() {
    const [jobHistories, setJobHistories] = useState<IJobHistory[]>();
    const [colDefs, setColDefs] = useState<ColDef<IJobHistory>[]>([
        { field: "employeeId" },
        { field: "startDate" },
        { field: "endDate" },
        { field: "jobId" },
        { field: "departmentId" },
    ]);
    useEffect(() => {
        populateJobHistoriesData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Job Histories</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <div
                className="ag-theme-quartz" // applying the Data Grid theme
                style={{ height: 650 }} // the Data Grid will fill the size of the parent container
            >
                <AgGridReact
                    rowData={jobHistories}
                    columnDefs={colDefs}
                    loading={jobHistories === undefined}
                />
            </div>
        </div>
    );

    async function populateJobHistoriesData() {
        const response = await fetch('JobHistory');
        const data = await response.json();
        setJobHistories(data);
    }
}

export default JobHistory;