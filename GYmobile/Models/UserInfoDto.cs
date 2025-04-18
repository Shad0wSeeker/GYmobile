﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Models
{
    public class UserInfoDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public bool IsLandlord { get; set; }
        public string AvatarUrl { get; set; }
    }
}
