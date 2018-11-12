using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class LemburDetailsModel
    {    
        public int LemburId { get; set; } 

        public int KaryawanId { get; set; }

        [Required]
        public string Task { get; set; }

        public virtual LemburModel Lembur { get; set; }  

        public virtual KaryawanModel Karyawan { get; set; }                    
    }
}
