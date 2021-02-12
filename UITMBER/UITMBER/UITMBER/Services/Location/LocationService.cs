using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Location;
using UITMBER.Services.Request;
using Xamarin.Forms;

namespace UITMBER.Services.Location
{
    public class LocationService : ILocationService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();

        public async Task<LocationRequest> SaveMyLocation(LocationRequest input)
        {
            var uri = $"{Settings.SERVERENDPOINT}/Location/SaveMyLocation";
            var result = await _requestService.PostAsync<LocationRequest>(uri, input);
            return result;
        }
    }
}