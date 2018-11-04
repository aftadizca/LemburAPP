using AplikasiLembur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.ViewModels
{
    public class HomeViewModel
    {
        public ChangePasswordModel changePasswordModel { set; get; }

        public KaryawanModel karyawanModel { set; get; }

        public KaryawanModel editKaryawanModel { set; get; }

        public TaskModel taskModel { get; set; }

        public TaskModel editTaskModel { get; set; }
    }                                                                       
}
