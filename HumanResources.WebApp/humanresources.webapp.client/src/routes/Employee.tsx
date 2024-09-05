import { useEffect, useState } from 'react';

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

    useEffect(() => {
        populateEmployeesData();
    }, []);

    const contents = employees === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Employee Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th>Hire Date</th>
                    <th>Job Id</th>
                    <th>Salary</th>
                    <th>Commission Percent</th>
                    <th>Manager Id</th>
                    <th>Department Id</th>
                </tr>
            </thead>
            <tbody>
                {employees.map(employee =>
                    <tr key={employee.employeeId}>
                        <td>{employee.firstName}</td>
                        <td>{employee.lastName}</td>
                        <td>{employee.email}</td>
                        <td>{employee.phoneNumber}</td>
                        <td>{employee.hireDate}</td>
                        <td>{employee.jobId}</td>
                        <td>{employee.salary}</td>
                        <td>{employee.commissionPercent}</td>
                        <td>{employee.managerId}</td>
                        <td>{employee.departmentId}</td>
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

    async function populateEmployeesData() {
        const response = await fetch('Employee');
        const data = await response.json();
        setEmployees(data);
    }
}

export default Employee;