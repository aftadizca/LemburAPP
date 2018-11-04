using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.ViewModels
{
    public class Register
    {
        [Required]
        public string Username { get; set; }
    }
}
