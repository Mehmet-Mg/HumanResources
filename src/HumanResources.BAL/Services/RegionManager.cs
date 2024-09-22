using HumanResources.BLL.Services.Contracts;
using HumanResources.DAL.Repositories;
using HumanResources.DTO.Models;

namespace HumanResources.BLL.Services
{
    public class RegionManager : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionManager(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public bool AddRegion(Region region)
        {
            return _regionRepository.Add(region);
        }

        public bool DeleteRegion(int regionId)
        {
            return _regionRepository.Delete(regionId);
        }

        public IEnumerable<Region> GetAllRegion()
        {
            return _regionRepository.GetAll();
        }

        public Region? GetRegionById(int regionId)
        {
            return _regionRepository.Get(regionId);
        }

        public bool UpdateRegion(Region region)
        {
            return _regionRepository.Update(region);
        }
    }
}