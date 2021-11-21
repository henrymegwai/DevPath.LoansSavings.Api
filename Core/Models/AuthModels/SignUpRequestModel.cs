using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.AuthModels
{
    public class SignUpRequestModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string  DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BVN { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
    }

    public class ForgotPasswordRequestModel
    { 
        public string PhoneNumber { get; set; }
       
    }
}
