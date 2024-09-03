using HumanResources.BLL.Services.Contracts;
using HumanResources.DAL.Repositories;
using HumanResources.DTO.Models;

namespace HumanResources.BLL.Services
{
    public class LocationManager : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationManager(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public bool AddLocation(Location location)
        {
            return _locationRepository.Add(location);
        }

        public bool DeleteLocation(int locationId)
        {
            return _locationRepository.Delete(locationId);
        }

        public IEnumerable<Location> GetAllLocation()
        {
            return _locationRepository.GetAll();
        }

        public Location? GetLocationById(int locationId)
        {
            return _locationRepository.Get(locationId);
        }

        public bool UpdateLocation(Location location)
        {
            return _locationRepository.Update(location);
        }
    }
}