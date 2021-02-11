using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.UserFavouriteLocation;
using UITMBER.Services.Request;
using Xamarin.Forms;

namespace UITMBER.Services.UserFavouriteLocation
{
    public class UserFavouriteLocationService : IUserFavouriteLocationService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();
        private const string UFLEndpoint = "UFLocations";
        public async Task<bool> AddLocationAsync(LocationRequest obj)
        {
            var uri = $"{Settings.SERVERENDPOINT}/{UFLEndpoint}/AddLocation";
            await _requestService.PostAsync<LocationRequest>(uri, obj);
            return true;
        }

        /// <param name="Id">the record id in the database is not a user id!</param>
        public async Task<bool> DeleteLocationAsync(long Id)
        {
            var uri = $"{Settings.SERVERENDPOINT}/{UFLEndpoint}​/DeleteLocation​/{Id}";
            var response = await _requestService.DeleteAsync<bool>(uri);
            return response;
        }

        /// <returns>returns a list of the user's favorite locations</returns>
        public async Task<List<LocationsResponse>> GetMyLocationsAsync()
        {
            var uri = $"{Settings.SERVERENDPOINT}/{UFLEndpoint}/GetMyLocations";
            var response = await _requestService.GetAsync<List<LocationsResponse>>(uri);
            return response;
        }
    }
}
