using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplikasiLembur.Models
{
    public class KaryawanModel
    {

        private string _namaKaryawan;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [DisplayName("NIK")]
        public int NIK { get; set; }
        
        [Required]
        [DisplayName("NAME")]
        public string NamaKaryawan {
            get { return _namaKaryawan; }
            set { _namaKaryawan = value.ToUpper();  }
        }
        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser IdentityUser { get; set; }

    }
}
