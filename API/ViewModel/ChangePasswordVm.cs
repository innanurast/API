﻿namespace API.ViewModel
{
    public class ChangePasswordVm
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
