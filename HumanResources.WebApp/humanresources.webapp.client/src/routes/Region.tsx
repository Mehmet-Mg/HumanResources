import { useEffect, useState } from 'react';

interface IRegion {
    regionId: number;
    regionName?: string;
}

function Region() {
    const [regions, setRegions] = useState<IRegion[]>();

    useEffect(() => {
        populateRegionsData();
    }, []);

    const contents = regions === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Region Id</th>
                    <th>Region Name</th>
                </tr>
            </thead>
            <tbody>
                {regions.map(region =>
                    <tr key={region.regionId}>
                        <td>{region.regionName}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Region Data</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateRegionsData() {
        const response = await fetch('Region');
        const data = await response.json();
        setRegions(data);
    }
}

export default Region;