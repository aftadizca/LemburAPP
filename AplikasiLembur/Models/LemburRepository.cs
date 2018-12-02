using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public object GetListLembur(string userId)
        {
            var result = _appDbContext.Lemburs.Where(x=>x.UserId == userId).Select(x=> new{id = x.Id, plan = x.Plan, date= x.CreatedDate}).OrderByDescending(x=>x.date).ToList();
            return result;
        }

        public LemburModel GetLemburModels(string lemburId)
        {
            
            var data = _appDbContext.Lemburs.Find(lemburId);
            var details = _appDbContext.LemburDetails.Where(x => x.LemburId == lemburId).ToList();
            System.Diagnostics.Debug.WriteLine("COUNT : "+details.Count);
            var lembur = new LemburModel
            {
                CreatedDate = data.CreatedDate,
                Departement = data.Departement,
                Division = data.Division,
                Employee = data.Employee,
                End = data.End,
                Id = data.Id,
                Plan = data.Plan,
                Start = data.Start,
                UserId = data.UserId
            };
            foreach (var d in details)
            {
                lembur.LemburDetails.Add(new LemburDetailsModel(){KaryawanId = d.KaryawanId,LemburId = d.LemburId,Task = d.Task});
            }
            return lembur;
        }

        public string[] GetTask()
        {
            return _appDbContext.LemburDetails.Where(x=>x.Task!="").Select(x => x.Task).Distinct().ToArray();
        }

        public async Task<TResult> UpdateLemburAsync(LemburModel lemburModel)
        {
            var res = _appDbContext.Lemburs.FirstOrDefault(p => p.Id == lemburModel.Id);
            if (res != null)
            {
                res.Plan = lemburModel.Plan;
                res.Start = lemburModel.Start;
                res.CreatedDate = lemburModel.CreatedDate;
                res.Departement = lemburModel.Departement;
                res.Division = lemburModel.Division;
                res.Employee = lemburModel.Employee;
                res.End = lemburModel.End;
                
            }

            var res2 = _appDbContext.LemburDetails.Where(p => p.LemburId == lemburModel.Id).ToList();

            var added = lemburModel.LemburDetails.Except(res2,new LemburDetailsComparer());

            if (added.Any())
            {
                _appDbContext.LemburDetails.AddRange(added);
            }

            var removed = res2.Except(lemburModel.LemburDetails, new LemburDetailsComparer());
            if (removed.Any())
            {
                _appDbContext.LemburDetails.RemoveRange(removed);
            }

            var changed = res2.Except(lemburModel.LemburDetails, new LemburDetailsTaskComparer());
            if (changed.Any())
            {
                foreach (var item in changed)
                {
                    var selected = lemburModel.LemburDetails.FirstOrDefault(x => x.LemburId == item.LemburId && x.KaryawanId == item.KaryawanId);
                    if (selected != null){
                        item.Task = selected.Task;
                    }
                }
            }


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
