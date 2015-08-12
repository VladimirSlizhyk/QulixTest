using System.Collections.Generic;
using System.Web.Mvc;
using QulixProject.ADOData;
using QulixProject.Core.Entities;
using QulixProject.Services.Services;
using QulixProject.UI.Models;

namespace QulixProject.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTask() //добавление задачи
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var performerService = new PerformerService(uow, repositoryFactory);
                var performers = performerService.GetAllPerformers();
                if (performers == null)
                {
                    performers=new List<Performer>();
                }
                var addTaskViewModel = new AddTaskViewModel()
                {
                    PerformerModels = performers
                };
                return View(addTaskViewModel);
            }
        }

        [HttpPost]
        public ActionResult AddTask(AddTaskViewModel taskModel) 
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var taskService = new TaskService(uow, repositoryFactory);
                var task = taskService.CreateTask(taskModel.Name, taskModel.Workload, taskModel.StartDate,
                    taskModel.EndDate, taskModel.Status, taskModel.PerformerId);
                return RedirectToAction("ViewTasks", "Home");
            }
        }

        [HttpGet]
        public ActionResult AddPerformer() //добавление исполнителя
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPerformer(PerformerModel performerModel)
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var performerService = new PerformerService(uow, repositoryFactory);
                var performer = performerService.CreatePerformer(performerModel.FirstName, performerModel.LastName, performerModel.PatronymicName);
                return RedirectToAction("ViewPerformers", "Home");
            }
        }

        public ActionResult ViewTasks() //просмотр всех задач
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var taskService = new TaskService(uow, repositoryFactory);
                var tasks = taskService.GetAllTasks();

                var model = new TaskListViewModel()
                {
                    Tasks = new List<TaskModel>()
                };

                var performerService = new PerformerService(uow, repositoryFactory);

                foreach (var task in tasks)
                {
                    var performer = performerService.GetPerformerById(task.PerformerId);
                    model.Tasks.Add(new TaskModel()
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Workload = task.Workload,
                        StartDate = task.StartDate,
                        EndDate = task.EndDate,
                        Status = task.Status,
                        PerformerId = task.PerformerId,
                        Performer = performer
                    });
                }

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeleteTasks(int idValue) //удаление задачи
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var taskService = new TaskService(uow, repositoryFactory);
                taskService.RemoveTask(idValue);
                uow.SaveChanges();
                return RedirectToAction("ViewTasks", "Home");
            }
        }

        public ActionResult ViewPerformers() //просмотр всех исполнителей
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var performerService = new PerformerService(uow, repositoryFactory);
                var performers = performerService.GetAllPerformers();

                var model = new PerformerListViewModel()
                {
                    Performers = new List<PerformerModel>()
                };

                if (performers == null)
                {
                    return View(model);
                }

                foreach (var performer in performers)
                {
                    model.Performers.Add(new PerformerModel()
                    {
                        Id = performer.Id,
                        FirstName = performer.FirstName,
                        LastName = performer.LastName,
                        PatronymicName = performer.PatronymicName
                    });
                }

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeletePerformers(int idValue) //удаление исполнителя
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var performerService = new PerformerService(uow, repositoryFactory);
                performerService.RemovePerformer(idValue);
                uow.SaveChanges();
                return RedirectToAction("ViewPerformers", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditTasks(EditTaskPartialViewModel editTaskPartialViewModel) //обновление задачи
        {
            var connectionFactory = new ConnectionFactory(ConnectionResource.ConnectionString);
            var context = new QulixContext(connectionFactory);
            var task = new Task()
            {
                Id = editTaskPartialViewModel.Id,
                Name = editTaskPartialViewModel.Name,
                Workload = editTaskPartialViewModel.Workload,
                StartDate = editTaskPartialViewModel.StartDate,
                EndDate = editTaskPartialViewModel.EndDate,
                Status = editTaskPartialViewModel.Status,
                PerformerId = editTaskPartialViewModel.PerformerId
            };

            using (var uow = context.CreateUnitOfWork())
            {
                var repositoryFactory = new RepositoryFactory(context);
                var taskService = new TaskService(uow, repositoryFactory);
                taskService.UpdateTask(task);
                uow.SaveChanges();
                return RedirectToAction("ViewTasks", "Home");
            }
        }
    }
}
