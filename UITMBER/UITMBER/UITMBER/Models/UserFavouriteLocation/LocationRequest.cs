using System;
using System.Collections.Generic;
using System.Text;

namespace UITMBER.Models.UserFavouriteLocation
{
    public class LocationRequest
    {
        public long UserId { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Name { get; set; }
    }
}
