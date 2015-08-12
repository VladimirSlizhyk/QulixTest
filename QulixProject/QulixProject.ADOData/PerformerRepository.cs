using System.Collections.Generic;
using System.Data;
using System.Linq;
using QulixProject.Core.Entities;
using QulixProject.DALInterfaces;

namespace QulixProject.ADOData
{
    public class PerformerRepository : MainRepository, IRepositoryGeneric<Performer, int>
    {
        public PerformerRepository(QulixContext context) : base(context)
        {
        }

        public void Create(Performer value) //добавление исполнителя в базу данных
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"INSERT INTO Performer (Id, FirstName, LastName, PatronymicName) VALUES(@Id, @FirstName, @LastName, @PatronymicName)";
                command.AddParameter("Id", value.Id);
                command.AddParameter("FirstName", value.FirstName);
                command.AddParameter("LastName", value.LastName);
                command.AddParameter("PatronymicName", value.PatronymicName);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Performer value) //обновление записи исполнителя
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"UPDATE Performer SET (FirstName=@FirstName, LastName=@LastName, PatronymicName=@PatronymicName) WHERE Id = @Id";
                command.AddParameter("FirstName", value.FirstName);
                command.AddParameter("LastName", value.LastName);
                command.AddParameter("PatronymicName", value.PatronymicName);
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int id) //удаление запили исполнителя с id
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"DELETE FROM Performer WHERE Id = @Id";
                command.AddParameter("Id", id);
                command.ExecuteNonQuery();
            }
        }

        public Performer GetEntityById(int id) //получение исполнителя по id
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Performer WHERE Id = @Id";
                command.AddParameter("Id", id);
                return ToList(command)[0];
            }
        }

        public List<Performer> GetAllEntities() //получение всех исполнителей
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Performer";
                return ToList(command);
            }
        }

        public List<Performer> ToList(IDbCommand command) //метод получения исполнителей в List
        {
            using (var reader = command.ExecuteReader())
            {
                var entities = new List<Performer>();
                while (reader.Read())
                {
                    var entity = new Performer();
                    Map(reader, entity);
                    entities.Add(entity);
                }
                if (!entities.Any())
                {
                    return null;
                }
                else return entities;
            }
        }

        public void Map(IDataRecord record, Performer entity) //мэппинг исполнителя
        {
            entity.Id = (int)record["Id"];
            entity.FirstName = (string)record["FirstName"];
            entity.LastName = (string)record["LastName"];
            entity.PatronymicName = (string)record["PatronymicName"];
        }
    }
}
