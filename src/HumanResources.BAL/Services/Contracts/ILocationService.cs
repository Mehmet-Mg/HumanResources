using HumanResources.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Services.Contracts
{
    public interface ILocationService
    {
        IEnumerable<Location> GetAllLocation();
        Location? GetLocationById(int locationId);
        bool AddLocation(Location location);
        bool UpdateLocation(Location location);
        bool DeleteLocation(int locationId);
    }
}
