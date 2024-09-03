using HumanResources.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Services.Contracts
{
    public interface IRegionService
    {
        IEnumerable<Region> GetAllRegion();
        Region? GetRegionById(int regionId);
        bool AddRegion(Region region);
        bool UpdateRegion(Region region);
        bool DeleteRegion(int regionId);
    }
}
