using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Car;

namespace UITMBER.Services.Car
{
    public interface ICarService
    {
        Task<List<CarDto>> GetMyCars();
        Task<string> Add(CarDto car);
        Task<string> Update(CarDto car);
        Task<string> Delete(long carID);
    }
}
