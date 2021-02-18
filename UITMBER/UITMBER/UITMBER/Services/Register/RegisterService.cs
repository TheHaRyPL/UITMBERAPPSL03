using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Authentication;
using UITMBER.Models.Register;
using UITMBER.Services.Request;
using Xamarin.Forms;

namespace UITMBER.Services.Register
{
    public class RegisterService : IRegisterService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();



        public async Task<bool> Register(RegisterRequest input)
        {
            var uri = $"{Settings.SERVERENDPOINT}/Authentication/Register";

            await _requestService.PostAsync<RegisterRequest>(uri, input);

            return true;

        }
    }
}
