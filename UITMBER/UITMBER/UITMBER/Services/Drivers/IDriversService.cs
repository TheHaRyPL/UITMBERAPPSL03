using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models;
using UITMBER.Models.Dto;

namespace UITMBER.Services.Drivers
{
    public interface IDriversService
    {
        Task<List<DriverDto>> GetNerbyDriveres(LatLong latlong);
        Task<DriverDto> GetProfile(int driverId);
    }
}
