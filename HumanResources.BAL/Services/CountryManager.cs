using HumanResources.BLL.Services.Contracts;
using HumanResources.DAL.Repositories;
using HumanResources.DTO.Models;

namespace HumanResources.BLL.Services
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryManager(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public bool AddCountry(Country country)
        {
            return _countryRepository.Add(country);
        }

        public bool DeleteCountry(string countryId)
        {
            return _countryRepository.Delete(countryId);
        }

        public IEnumerable<Country> GetAllCountry()
        {
            return _countryRepository.GetAll();
        }

        public Country? GetCountryById(string countryId)
        {
            return _countryRepository.Get(countryId);
        }

        public bool UpdateCountry(Country country)
        {
            return _countryRepository.Update(country);
        }
    }
}
