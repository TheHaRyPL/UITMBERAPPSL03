using System;
using System.Collections.Generic;
using System.Text;

namespace UITMBER.Models.Application
{
    class SendApplicationRequest
    {
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public string DriverLicenceNo { get; set; }
        public string DriverLicencePhoto { get; set; }
        public long CarId { get; set; }
    }
}
