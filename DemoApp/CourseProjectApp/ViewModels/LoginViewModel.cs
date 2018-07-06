﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter USername")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me?")]
        public bool RememberMe { get; set; }


    }
}
