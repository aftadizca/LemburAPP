using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public interface ILemburRepository
    {
        Task<TResult> AddLembur(LemburModel lemburModel);

        Task<TResult> GetLastId();
    }
}
