import { ColDef } from '@ag-grid-community/core';
import { AgGridReact } from '@ag-grid-community/react';
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
    const [colDefs, setColDefs] = useState<ColDef<ILocation>[]>([
        { field: "locationId" },
        { field: "streetAddress" },
        { field: "postalCode" },
        { field: "city" },
        { field: "stateProvince" },
        { field: "countryId" },
    ]);
    useEffect(() => {
        populateLocationsData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Locations</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <div
                className="ag-theme-quartz" // applying the Data Grid theme
                style={{ height: 650 }} // the Data Grid will fill the size of the parent container
            >
                <AgGridReact
                    rowData={locations}
                    columnDefs={colDefs}
                    loading={locations === undefined}
                />
            </div>
        </div>
    );

    async function populateLocationsData() {
        const response = await fetch('Location');
        const data = await response.json();
        setLocations(data);
    }
}

export default Location;