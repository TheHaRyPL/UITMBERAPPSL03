using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Location;

namespace UITMBER.Services.Location
{
    public interface ILocationService
    {
        Task<LocationRequest> SaveMyLocation(LocationRequest input);
    }
}