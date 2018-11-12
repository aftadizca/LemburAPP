using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class KaryawanRepository : IKaryawanRepository
    {
        private readonly AppDbContext _appDbContext;
        public KaryawanRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TResult> AddKaryawan(KaryawanModel karyawan)
        {
            _appDbContext.Karyawans.Add(karyawan);
            var result = await _appDbContext.SaveChangesAsync();
            if (result>0)
            {
                return TResult.Success;
            }
            else
            {
                return TResult.Fail;
            }
        }

        public async Task<TResult> DelKaryawanAsync(int id)
        {
            var karyawan =  await _appDbContext.Karyawans.FirstOrDefaultAsync(p => p.Id == id);
            _appDbContext.Karyawans.Remove(karyawan);
            var saves = await _appDbContext.SaveChangesAsync();
            if (saves>0)
            { 
                return TResult.Success;
            }
            else
            {
                return TResult.Fail;
            } 
        }

        public  IAsyncEnumerable<KaryawanModel> GetAllKaryawan()
        {
            return _appDbContext.Karyawans.OrderBy(p => p.NamaKaryawan).ToAsyncEnumerable(); 
        }

        public IAsyncEnumerable<KaryawanModel> GetAllKaryawanByUserId(string id)
        {
            return _appDbContext.Karyawans.Where(p => p.UserId==id).ToAsyncEnumerable();
        }

        public Task<KaryawanModel> GetKarywanByNIK(int NIK)
        {
            return _appDbContext.Karyawans.FirstOrDefaultAsync(p => p.NIK == NIK);
        }

        public async Task<TResult> UpdateKaryawan(KaryawanModel karyawan)
        {
            var find = await _appDbContext.Karyawans.FirstOrDefaultAsync(p=>p.Id == karyawan.Id);
            var nikCount = await _appDbContext.Karyawans.CountAsync(p => p.NIK == karyawan.NIK);

            if (find != null && (nikCount==0 || karyawan.NIK == find.NIK ))
            {
                find.NamaKaryawan = karyawan.NamaKaryawan;
                find.NIK = karyawan.NIK;
                var result = await _appDbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return TResult.Success;
                } 
            }
            return TResult.Fail;
        }



    }
}
