using System;
using System.Collections.Generic;
using QulixProject.Core.Entities;
using Task = QulixProject.Core.Entities.Task;

namespace QulixProject.BLInterfaces.BLInterfaces
{
    public interface ITaskService : IMainService
    {
        Task CreateTask(string name, int workload, DateTime startDate, DateTime endDate, Status status, int performerId);
        void UpdateTask(Task task);
        void RemoveTask(int id);
        Task GetTaskById(int id);
        List<Task> GetAllTasks();
    }
}
