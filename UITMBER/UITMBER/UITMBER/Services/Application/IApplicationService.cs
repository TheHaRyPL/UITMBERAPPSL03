using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Application;

namespace UITMBER.Services.Application
{
    public interface IApplicationService
    {
        Task<bool> SendApplicationAsync(SendApplicationRequest input);
        Task<List<GetMyApplicationsResponse>> GetMyApplicationsAsync();
    }
}
