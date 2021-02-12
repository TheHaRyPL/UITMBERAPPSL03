using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Client;

namespace UITMBER.Services.Clients
{
    public interface IClinetsService
    {

        Task<ClientDto> GetMyProfile();
        Task<string> UpdatePhoto(string base64Photo);
    }
}
