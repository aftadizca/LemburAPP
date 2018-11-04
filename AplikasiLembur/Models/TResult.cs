using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class TResult
    {
        public TResult(bool test)
        {
            Succeeded = test;
        }
        public static TResult Success { get; } = new TResult(true);
        public static TResult Fail { get; }  = new TResult(false);

        public bool Succeeded { get; set; }
    }
}
