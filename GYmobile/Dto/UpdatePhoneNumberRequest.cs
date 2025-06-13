using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Dto
{
    public class UpdatePhoneNumberRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
