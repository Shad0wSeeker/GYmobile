﻿
namespace GYmobile.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserRole {  get; set; } = string.Empty;
    }

}
