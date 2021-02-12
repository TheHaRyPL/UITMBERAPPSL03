using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models;
using UITMBER.Models.Dto;
using UITMBER.Services.Request;
using Xamarin.Forms;

namespace UITMBER.Services.Drivers
{
   public  class DriversService : IDriversService
    {
        private IRequestService _requestService => DependencyService.Get<IRequestService>();

        public async Task<List<DriverDto>> GetNerbyDriveres(LatLong latlong)
        {
            var url = $"{Settings.SERVERENDPOINT}/Driver/GetNerbyDriveres?latitude={latlong.Lat.ToString().Replace(',','.')}&longitude={latlong.Long.ToString().Replace(',', '.')}";
            var data = await _requestService.GetAsync<List<DriverDto>>(url);

            return data;
        }

        public async Task<DriverDto> GetProfile(int driverId)
        {
            var url = $"{Settings.SERVERENDPOINT}/Driver/GetProfile?id={driverId}";
            var data = await _requestService.GetAsync<DriverDto>(url);

            return data;
        }
    }
}
