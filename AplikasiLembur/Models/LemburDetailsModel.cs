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
        public string LemburId { get; set; } 

        public int KaryawanId { get; set; }

        [Required]
        public string Task { get; set; }

        public LemburModel Lembur { get; set; }  

        public  KaryawanModel Karyawan { get; set; }                    
    }
}
