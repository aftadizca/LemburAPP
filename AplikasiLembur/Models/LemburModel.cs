using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class LemburModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = Guid.NewGuid().ToString();                                

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
        public IdentityUser User { get; set; }

        public List<LemburDetailsModel> LemburDetails { get; set; } = new List<LemburDetailsModel>();

    }
}
