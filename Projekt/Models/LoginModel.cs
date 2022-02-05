using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjektASPNET.Models
{
    public enum RegisterStatus
    {
        OK,
        LoginAlreadyTaken,
        LoginOrPasswordTooLong,
        PasswordsNotEqual
    }

    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string Password2 { get; set; } // sprawdzane przy rejestracji czy jest to samo, więc luz
    }
}