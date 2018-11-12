using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class LemburModel
    {
        public int Id { get; set; }

        [Required]
        public string Departement { get; set; }
        public string Division { get; set; }
        public string Employee { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Start { get; set; }

        
        [DataType(DataType.Time)]
        public DateTime End { get; set; }

        [Required]
        public int Plan { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        

        public string UserId { get; set; } 
        public virtual IdentityUser User { get; set; }

        public List<LemburDetailsModel> LemburDetails { get; set; }

    }
}
