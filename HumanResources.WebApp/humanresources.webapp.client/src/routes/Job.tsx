import { useEffect, useState } from 'react';

interface IJob {
    jobId: string;
    jobTitle: string;
    minSalary?: number;
    maxSalary?: number;
}

function Job() {
    const [jobs, setJobs] = useState<IJob[]>();

    useEffect(() => {
        populateJobsData();
    }, []);

    const contents = jobs === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Job Id</th>
                    <th>Job Title</th>
                    <th>Min Salary</th>
                    <th>Max Salary</th>
                </tr>
            </thead>
            <tbody>
                {jobs.map(jobs =>
                    <tr key={jobs.jobId}>
                        <td>{jobs.jobTitle}</td>
                        <td>{jobs.minSalary}</td>
                        <td>{jobs.maxSalary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Country Data</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateJobsData() {
        const response = await fetch('Job');
        const data = await response.json();
        setJobs(data);
    }
}

export default Job;