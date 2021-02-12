using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Client;
using UITMBER.Services.Request;
using Xamarin.Forms;

namespace UITMBER.Services.Clients
{
    class ClientsService : IClinetsService
    {

        public IRequestService _requestService => DependencyService.Get<IRequestService>();




        public async Task<ClientDto> GetMyProfile()
        {

            var url = $"{Settings.SERVERENDPOINT}/Clients/GetMyProfile";

            var result = await _requestService.GetAsync<ClientDto>(url);
            return result;
        }


        public async Task<string> UpdatePhoto(string base64Photo)
        {
            var url = $"{Settings.SERVERENDPOINT}/Clients​/UpdatePhoto";
            var result = await _requestService.PutAsync<string, string>(url, base64Photo);
            return result;
        }
    }
}
