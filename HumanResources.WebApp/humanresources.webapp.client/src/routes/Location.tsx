import { useEffect, useState } from 'react';

interface ILocation {
    locationId: number;
    streetAddress?: string;
    postalCode?: string;
    city: string;
    stateProvince?: string;
    countryId?: string;
}

function Location() {
    const [locations, setLocations] = useState<ILocation[]>();

    useEffect(() => {
        populateLocationsData();
    }, []);

    const contents = locations === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Location Id</th>
                    <th>Street Address</th>
                    <th>Postal Code</th>
                    <th>City</th>
                    <th>State Province</th>
                    <th>Country Id</th>
                </tr>
            </thead>
            <tbody>
                {locations.map(location =>
                    <tr key={location.locationId}>
                        <td>{location.streetAddress}</td>
                        <td>{location.postalCode}</td>
                        <td>{location.city}</td>
                        <td>{location.stateProvince}</td>
                        <td>{location.countryId}</td>
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

    async function populateLocationsData() {
        const response = await fetch('Location');
        const data = await response.json();
        setLocations(data);
    }
}

export default Location;