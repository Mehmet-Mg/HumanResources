import { useEffect, useState } from 'react';

interface IDepartment {
    departmentId: number;
    departmentName: string;
    managerId?: number;
    locationId?: number;
}

function Department() {
    const [departments, setDepartments] = useState<IDepartment[]>();

    useEffect(() => {
        populateDepartmentsData();
    }, []);

    const contents = departments === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Department Id</th>
                    <th>Department Name</th>
                    <th>Manager Id</th>
                    <th>Location Id</th>
                </tr>
            </thead>
            <tbody>
                {departments.map(department =>
                    <tr key={department.departmentId}>
                        <td>{department.departmentId}</td>
                        <td>{department.departmentName}</td>
                        <td>{department.managerId}</td>
                        <td>{department.locationId}</td>
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

    async function populateDepartmentsData() {
        const response = await fetch('Department');
        const data = await response.json();
        setDepartments(data);
    }
}

export default Department;