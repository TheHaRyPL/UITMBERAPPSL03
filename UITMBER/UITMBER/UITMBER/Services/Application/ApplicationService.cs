using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Application;
using UITMBER.Services.Request;
using Xamarin.Forms;

namespace UITMBER.Services.Application
{
    class ApplicationService : IApplicationService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();

        public async Task<List<GetMyApplicationsResponse>> GetMyApplicationsAsync()
        {
            var uri = $"{Settings.SERVERENDPOINT}/Application/GetMyApplications";
            return await _requestService.GetAsync<List<GetMyApplicationsResponse>>(uri);
        }

        public async Task<bool> SendApplicationAsync(SendApplicationRequest input)
        {
            var uri = $"{Settings.SERVERENDPOINT}/Application/SendApplication";
            await _requestService.PostAsync<SendApplicationRequest>(uri, input);
            return true;
        }

    }
}
