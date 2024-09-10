import { useEffect, useState } from 'react';
import "ag-grid-community/styles/ag-grid.css"; // Mandatory CSS required by the Data Grid
import "ag-grid-community/styles/ag-theme-quartz.css"; // Optional Theme applied to the Data Grid
import { ColDef, ModuleRegistry } from '@ag-grid-community/core';
import { AgGridReact } from '@ag-grid-community/react';

interface ICountry {
    countryId: string;
    countryName?: string;
    regionId?: number;
}


function Country() {
    const [countries, setCountries] = useState<ICountry[]>();
    const [colDefs, setColDefs] = useState<ColDef<ICountry>[]>([
        { field: "countryId" },
        { field: "countryName" },
        { field: "regionId" },
    ]);

    useEffect(() => {
        populateCountryData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Country Data</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <div
                className="ag-theme-quartz" // applying the Data Grid theme
                style={{ height: 500 }} // the Data Grid will fill the size of the parent container
            >
                <AgGridReact
                    rowData={countries}
                    columnDefs={colDefs}
                    loading={countries === undefined}
                />
            </div>
        </div>
    );

    async function populateCountryData() {
        const response = await fetch('Country');
        const data = await response.json();
        setCountries(data);
    }
}

export default Country;