using HumanResources.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Services.Contracts
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAllCountry();
        Country? GetCountryById(string countryId);
        bool AddCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(string countryId);
    }
}
