using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;
        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<TResult> AddTaskAsync(TaskModel taskModel)
        {
            _appDbContext.Tasks.Add(taskModel);
            var result = await _appDbContext.SaveChangesAsync();

            if (result > 0)
            {
                return TResult.Success;
            }
            return TResult.Fail;
        }

        public async Task<TResult> DelTaskAsync(int[] id)
        {
            foreach(var i in id)
            {
                var task = await _appDbContext.Tasks.FirstOrDefaultAsync(p => p.Id == i);
                _appDbContext.Tasks.Remove(task);
            }
            var saves = await _appDbContext.SaveChangesAsync(); 
            if (saves > 0)
            {
                return TResult.Success;
            }
            else
            {
                return TResult.Fail;
            }
        }

        public IAsyncEnumerable<TaskModel> GetAllTask()
        {
            return _appDbContext.Tasks.ToAsyncEnumerable();
        }

        public IAsyncEnumerable<TaskModel> GetAllTaskByUserId(string id)
        {
            return _appDbContext.Tasks.Where(p=>p.UserId == id).ToAsyncEnumerable();
        }

        public async Task<TResult> UpdateTaskAsync(TaskModel taskModel)
        {
            var find = await _appDbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskModel.Id);

            if (find != null)
            {
                find.Task = taskModel.Task;
                var result = await _appDbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return TResult.Success;
                }
            }
            return TResult.Fail;
        }

        public async Task<string> GetTaskById(int id)
        {
            var task = await _appDbContext.Tasks.FirstOrDefaultAsync(p => p.Id == id);
            return task.Task;
            
        }

        public async Task<int> SearchTaskAsync(string task)
        {
            return await _appDbContext.Tasks.CountAsync(p => p.Task == task);  
        }
    }
}
