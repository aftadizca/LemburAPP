using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Task { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
