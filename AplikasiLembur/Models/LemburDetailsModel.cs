using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class LemburDetailsModel
    {    
        public string LemburId { get; set; } 

        public int KaryawanId { get; set; }

        [Required]
        public string Task { get; set; }

        public LemburModel Lembur { get; set; }  

        public  KaryawanModel Karyawan { get; set; }                    
    }

     public class LemburDetailsComparer : IEqualityComparer<LemburDetailsModel>
    {

        public bool Equals(LemburDetailsModel x, LemburDetailsModel y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.LemburId.Equals(y.LemburId) && x.KaryawanId.Equals(y.KaryawanId);
        }

        public int GetHashCode(LemburDetailsModel obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashLemburId = obj.LemburId.GetHashCode();

            //Get hash code for the Code field. 
            int hashKaryawanId = obj.KaryawanId.GetHashCode(); 

            //Calculate the hash code for the product. 
            return hashKaryawanId ^ hashLemburId;
        }
    }

    public class LemburDetailsTaskComparer : IEqualityComparer<LemburDetailsModel>
    {

        public bool Equals(LemburDetailsModel x, LemburDetailsModel y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.Task.Equals(y.Task);
        }

        public int GetHashCode(LemburDetailsModel obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashTask = obj.Task.GetHashCode();

            //Calculate the hash code for the product. 
            return hashTask;
        }
    }

}
