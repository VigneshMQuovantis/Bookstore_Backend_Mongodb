﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.UserModels
{
    public class LoginResponseModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string JwtToken { get; set; }
    }
}