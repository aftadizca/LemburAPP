using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikasiLembur.Models
{
    public interface ITaskRepository
    {
        IAsyncEnumerable<TaskModel> GetAllTask();

        IAsyncEnumerable<TaskModel> GetAllTaskByUserId(string id);

        Task<TResult> AddTaskAsync(TaskModel taskModel);

        Task<TResult> DelTaskAsync(int[] id);

        Task<TResult> UpdateTaskAsync(TaskModel taskModel);
    }
}
