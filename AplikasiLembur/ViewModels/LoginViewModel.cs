using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AplikasiLembur.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("USERNAME")]
        public string Username { get; set; }
        
        [Required]
        [DisplayName("PASSWORD")]
        [MinLength(8,ErrorMessage = "Minimum password length is 8 !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
