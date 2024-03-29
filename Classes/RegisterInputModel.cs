﻿using System.ComponentModel.DataAnnotations;

namespace gravimetry_api.Classes
{
    public class RegisterInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
