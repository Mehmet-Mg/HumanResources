import { ColDef } from '@ag-grid-community/core';
import { AgGridReact } from '@ag-grid-community/react';
import { useEffect, useState } from 'react';

interface IJob {
    jobId: string;
    jobTitle: string;
    minSalary?: number;
    maxSalary?: number;
}

function Job() {
    const [jobs, setJobs] = useState<IJob[]>();
    const [colDefs, setColDefs] = useState<ColDef<IJob>[]>([
        { field: "jobId" },
        { field: "jobTitle" },
        { field: "minSalary" },
        { field: "maxSalary" },
    ]);
    useEffect(() => {
        populateJobsData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Jobs</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <div
                className="ag-theme-quartz" // applying the Data Grid theme
                style={{ height: 650 }} // the Data Grid will fill the size of the parent container
            >
                <AgGridReact
                    rowData={jobs}
                    columnDefs={colDefs}
                    loading={jobs === undefined}
                />
            </div>
        </div>
    );

    async function populateJobsData() {
        const response = await fetch('Job');
        const data = await response.json();
        setJobs(data);
    }
}

export default Job;