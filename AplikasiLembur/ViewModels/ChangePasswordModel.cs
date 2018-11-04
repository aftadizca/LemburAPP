using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplikasiLembur.ViewModels
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("CURRENT PASSWORD")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("NEW PASSWORD")]
        [MinLength(8,ErrorMessage = "Password minimum lenght is 8")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage = "Password not match")]
        [MinLength(8, ErrorMessage = "Password minimum lenght is 8")]
        [DisplayName("REPEAT NEW PASSWORD")]
        public string NewPasswordRepeat { get; set; }
    }
}
