using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Contexts;
using QulixProject.Core.Entities;
using QulixProject.DALInterfaces;

namespace QulixProject.ADOData
{
    public class TaskRepository : MainRepository, IRepositoryGeneric<Task, int>
    {
        public TaskRepository(QulixContext context)
            : base(context)
        {
        }

        public void Create(Task value) //добавление записи задачи
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText =
                    @"INSERT INTO Task (Id, Name, Workload, StartDate, EndDate, Status, PerformerId) VALUES (@Id, @Name, @Workload, @StartDate, @EndDate, @Status, @PerformerId)";
                command.AddParameter("Id", value.Id);
                command.AddParameter("Name", value.Name);
                command.AddParameter("Workload", value.Workload);
                command.AddParameter("StartDate", value.StartDate);
                command.AddParameter("EndDate", value.EndDate);
                command.AddParameter("Status", value.Status);
                command.AddParameter("PerformerId", value.PerformerId);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Task value) //изменени записи задачи
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText =
                    @"UPDATE Task SET Name=@Name, Workload=@Workload, StartDate=@StartDate, EndDate=@EndDate, Status=@Status, PerformerId=@PerformerId WHERE Id = @Id";
                command.AddParameter("Name", value.Name);
                command.AddParameter("Workload", value.Workload);
                command.AddParameter("StartDate", value.StartDate);
                command.AddParameter("EndDate", value.EndDate);
                command.AddParameter("Status", value.Status);
                command.AddParameter("PerformerId", value.PerformerId);
                command.AddParameter("Id", value.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int id) //удаление записи задачи
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"DELETE FROM Task WHERE Id = @Id";
                command.AddParameter("Id", id);
                command.ExecuteNonQuery();
            }
        }

        public Task GetEntityById(int id) //получение задачи по id
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Task WHERE Id = @Id";
                command.AddParameter("Id", id);
                var result = ToList(command);
                if (result == null)
                {
                    return null;
                }
                else return result[0];
            }
        }

        public List<Task> GetAllEntities() // получение всех записей задач
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Task";
                    //WHERE CompanyId = @companyId AND FirstName LIKE @firstName";
                //command.AddParameter("companyId", LoggedInUser.companyId);
                //command.AddParameter("firstName", firstName + "%");
                return ToList(command);
            }
        }

        public List<Task> ToList(IDbCommand command) //получение записей задач в List
        {
            using (var reader = command.ExecuteReader())
            {
                var entities = new List<Task>();
                while (reader.Read())
                {
                    var entity = new Task();
                    Map(reader, entity);
                    entities.Add(entity);
                }
                return entities;
            }
        }

        public void Map(IDataRecord record, Task entity) //мэппинг задач
        {
            entity.Id = (int) record["Id"];
            entity.Name = (string) record["Name"];
            entity.Workload = (int) record["Workload"];
            entity.StartDate = (DateTime) record["StartDate"];
            entity.EndDate = (DateTime) record["EndDate"];
            entity.Status = (Status) record["Status"];
            entity.PerformerId = (int) record["PerformerId"];
        }
    }
}
