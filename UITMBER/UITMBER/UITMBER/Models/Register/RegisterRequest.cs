using System;
using System.Collections.Generic;
using System.Text;

namespace UITMBER.Models.Register
{
    public class RegisterRequest
    {

        public string Email { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string PhoneNumber { get; set; }
    }
}