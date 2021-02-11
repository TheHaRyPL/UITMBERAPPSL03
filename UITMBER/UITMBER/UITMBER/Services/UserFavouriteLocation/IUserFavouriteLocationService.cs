using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.UserFavouriteLocation;

namespace UITMBER.Services.UserFavouriteLocation
{
    public interface IUserFavouriteLocationService
    {
        Task<bool> AddLocationAsync(LocationRequest obj);
        Task<bool> DeleteLocationAsync(long Id);
        Task<List<LocationsResponse>> GetMyLocationsAsync();
    }
}
