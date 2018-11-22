using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public interface ILemburRepository
    {
        Task<TResult> AddLemburAsync(LemburModel lemburModel);

        Task<bool> CheckIdAsync(LemburModel lemburModel);

        Task<TResult> UpdateLemburAsync(LemburModel lemburModel);
    }

}
