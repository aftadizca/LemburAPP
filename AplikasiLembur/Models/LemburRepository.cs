using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class LemburRepository : ILemburRepository
    {
        private readonly AppDbContext _appDbContext;
        public LemburRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<TResult> AddLemburAsync(LemburModel lemburModel)
        {
            await _appDbContext.Lemburs.AddAsync(lemburModel);
            await _appDbContext.LemburDetails.AddRangeAsync(lemburModel.LemburDetails);
            var result = _appDbContext.SaveChanges();

            if (result > 0)
            {
                return TResult.Success;
            }
            else
            {
                return TResult.Fail;
            } 
        }

        public async Task<bool> CheckIdAsync(LemburModel lemburModel)
        {
            var result = await _appDbContext.Lemburs.FindAsync(lemburModel.Id);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<TResult> UpdateLemburAsync(LemburModel lemburModel)
        {
            _appDbContext.Update(lemburModel);
            _appDbContext.UpdateRange(lemburModel.LemburDetails);

            var result = await _appDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return TResult.Success;
            }
            else
            {
                return TResult.Fail;
            }
        }
    }
}
