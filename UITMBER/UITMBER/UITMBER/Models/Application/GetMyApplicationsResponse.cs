using System;
using System.Collections.Generic;
using System.Text;

namespace UITMBER.Models.Application
{
    class GetMyApplicationsResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public bool Accepted { get; set; }
        public string DriverLicenceNo { get; set; }
        public string DriverLicencePhoto { get; set; }
        public long CarId { get; set; }
    }
}
