import { ColDef } from '@ag-grid-community/core';
import { AgGridReact } from '@ag-grid-community/react';
import { useEffect, useState } from 'react';

interface IRegion {
    regionId: number;
    regionName?: string;
}

function Region() {
    const [regions, setRegions] = useState<IRegion[]>();
    const [colDefs, setColDefs] = useState<ColDef<IRegion>[]>([
        { field: "regionId" },
        { field: "regionName" },
    ]);
    useEffect(() => {
        populateRegionsData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Regions</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <div
                className="ag-theme-quartz" // applying the Data Grid theme
                style={{ height: 650 }} // the Data Grid will fill the size of the parent container
            >
                <AgGridReact

                    rowData={regions}
                    columnDefs={colDefs}
                    loading={regions === undefined}
                />
            </div>
        </div>
    );

    async function populateRegionsData() {
        const response = await fetch('Region');
        const data = await response.json();
        setRegions(data);
    }
}

export default Region;