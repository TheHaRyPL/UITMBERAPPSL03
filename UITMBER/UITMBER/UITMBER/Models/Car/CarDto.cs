using System;
using System.Collections.Generic;
using System.Text;

namespace UITMBER.Models.Car
{
    public class CarDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string PlateNo { get; set; }
        public string Photo { get; set; }
        public int Year { get; set; }

        public CarType Type { get; set; }

        public string Color { get; set; }

        public bool IsActive { get; set; }
    }

    public enum CarType
    {
        Standard = 1,
        Luxury = 2,
        Seater7 = 3
    }
}
