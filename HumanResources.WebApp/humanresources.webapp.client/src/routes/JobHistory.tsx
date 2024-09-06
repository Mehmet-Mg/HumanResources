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

    useEffect(() => {
        populateJobHistoriesData();
    }, []);

    const contents = jobHistories === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Employee ID</th>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th>Job Id</th>
                    <th>Department Id</th>
                </tr>
            </thead>
            <tbody>
                {jobHistories.map(history =>
                    <tr key={history.employeeId}>
                        <td>{history.startDate}</td>
                        <td>{history.endDate}</td>
                        <td>{history.jobId}</td>
                        <td>{history.departmentId}</td>
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

    async function populateJobHistoriesData() {
        const response = await fetch('JobHistory');
        const data = await response.json();
        setJobHistories(data);
    }
}

export default JobHistory;