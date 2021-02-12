using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Register;

namespace UITMBER.Services.Register
{
    public interface IRegisterService
    {
        Task<bool> Register(RegisterRequest input);
    }
}
