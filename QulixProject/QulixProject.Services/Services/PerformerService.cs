using System;
using System.Collections.Generic;
using System.Linq;
using QulixProject.BLInterfaces.BLInterfaces;
using QulixProject.BLInterfaces.Exceptions;
using QulixProject.Core.Entities;
using QulixProject.DALInterfaces;

namespace QulixProject.Services.Services
{
    public class PerformerService : MainService, IPerformerService
    {
        public PerformerService(IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, repositoryFactory)
        {
        }

        public Performer CreatePerformer(string firstName, string lastName, string patronymicName) //создание исполнителя
        {
            var performerRepository = RepositoryFactory.GetPerformerRepository();
            var performers = performerRepository.GetAllEntities();
            ;
            var performer = new Performer()
            {
                Id = new IdGenerator<Performer>().Genarate(performers),
                FirstName = firstName,
                LastName = lastName,
                PatronymicName = patronymicName
            };

            performerRepository.Create(performer);
            try
            {
                UnitOfWork.SaveChanges();
            }
            catch (PerformerServiceException exception)
            {
                throw new PerformerServiceException(exception);
            }
            UnitOfWork.Dispose();

            return performer;
        }

        public void UpdatePerformer(Performer performer) //обновление исполнителя
        {
            var performerRepository = RepositoryFactory.GetPerformerRepository();

            try
            {
                performerRepository.Update(performer);
            }
            catch (PerformerServiceException exception)
            {
                throw new PerformerServiceException(exception);
            }
        }

        public void RemovePerformer(int id) //удаление исполнителя
        {
            var performerRepository = RepositoryFactory.GetPerformerRepository();
            try
            {
                performerRepository.Remove(id);
            }
            catch (PerformerServiceException exception)
            {
                throw new PerformerServiceException(exception);
            }
        }

        public Performer GetPerformerById(int id) //получение исполнителя по id
        {
            var performerRepository = RepositoryFactory.GetPerformerRepository();
            try
            {
                var performer = performerRepository.GetEntityById(id);
                return performer;
            }
            catch (PerformerServiceException exception)
            {
                throw new PerformerServiceException(exception);
            }
        }

        public List<Performer> GetAllPerformers() //получение всех исполнителей
        {
            var performerRepository = RepositoryFactory.GetPerformerRepository();
            try
            {
                var performers = performerRepository.GetAllEntities();
                return performers;
            }
            catch (PerformerServiceException exception)
            {
                throw new PerformerServiceException(exception);
            }
        }
    }
}
