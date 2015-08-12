using System;
using System.Collections.Generic;
using System.Linq;
using QulixProject.BLInterfaces.BLInterfaces;
using QulixProject.BLInterfaces.Exceptions;
using QulixProject.Core.Entities;
using QulixProject.DALInterfaces;

namespace QulixProject.Services.Services
{
    public class TaskService: MainService, ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, repositoryFactory)
        {
        }

        public Task CreateTask(string name, int workload, DateTime startDate, DateTime endDate, Status status, int performerId) //добавление задачи
        {
            var taskRepository = RepositoryFactory.GetTaskRepository();
            var tasks = taskRepository.GetAllEntities();
;
            var task = new Task()
            {
                Id = new IdGenerator<Task>().Genarate(tasks),
                Name = name,
                Workload = workload,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                PerformerId = performerId
            };

            taskRepository.Create(task);
            try
            {
                UnitOfWork.SaveChanges();
            }
            catch (TaskServiceException exception)
            {
                throw new TaskServiceException(exception);
            }
            UnitOfWork.Dispose();

            return task;
        }

        public void UpdateTask(Task task) //обновление задачи
        {
            var taskRepository = RepositoryFactory.GetTaskRepository();

            try
            {
                taskRepository.Update(task);
            }
            catch (TaskServiceException exception)
            {
                throw new TaskServiceException(exception);
            }
        }

        public void RemoveTask(int id) //удаление задачи
        {
            var taskRepository = RepositoryFactory.GetTaskRepository();
            try
            {
                taskRepository.Remove(id);
            }
            catch (TaskServiceException exception)
            {
                throw new TaskServiceException(exception);
            }
        }

        public Task GetTaskById(int id) //получение задачи по id
        {
            var taskRepository = RepositoryFactory.GetTaskRepository();
            try
            {
                var task = taskRepository.GetEntityById(id);
                return task;
            }
            catch (TaskServiceException exception)
            {
                throw new TaskServiceException(exception);
            }
        }

        public List<Task> GetAllTasks() //получение всех задач
        {
            var taskRepository = RepositoryFactory.GetTaskRepository();
            try
            {
                var tasks = taskRepository.GetAllEntities();
                return tasks;
            }
            catch (TaskServiceException exception)
            {
                throw new TaskServiceException(exception);
            }
        }
    }
}
