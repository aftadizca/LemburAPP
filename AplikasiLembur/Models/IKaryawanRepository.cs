using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public interface IKaryawanRepository
    {

        IAsyncEnumerable<KaryawanModel> GetAllKaryawan();

        IAsyncEnumerable<KaryawanModel> GetAllKaryawanByUserId(string id);

        Task<KaryawanModel> GetKarywanByNIK(int NIK);

        Task<TResult> AddKaryawan(KaryawanModel karyawan);

        Task<TResult> DelKaryawanAsync(int id);

        Task<TResult> UpdateKaryawan(KaryawanModel karyawan);

    }
}
