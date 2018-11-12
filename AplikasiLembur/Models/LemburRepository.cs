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
        public Task<TResult> AddLembur(LemburModel lemburModel)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> GetLastId()
        {
            throw new NotImplementedException();
        }
    }
}
