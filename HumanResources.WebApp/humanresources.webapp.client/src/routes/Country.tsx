import { useEffect, useState } from 'react';

interface ICountry {
    countryId: string;
    countryName?: string;
    regionId?: number;
}

function Country() {
    const [countries, setCountries] = useState<ICountry[]>();

    useEffect(() => {
        populateCountryData();
    }, []);

    const contents = countries === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Country Id</th>
                    <th>Country Name</th>
                    <th>Region Id</th>
                </tr>
            </thead>
            <tbody>
                {countries.map(country =>
                    <tr key={country.countryId}>
                        <td>{country.countryId}</td>
                        <td>{country.countryName}</td>
                        <td>{country.regionId}</td>
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

    async function populateCountryData() {
        const response = await fetch('Country');
        const data = await response.json();
        setCountries(data);
    }
}

export default Country;