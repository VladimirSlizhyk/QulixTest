using System.Collections.Generic;
using System.Linq;
using QulixProject.Core.Entities;

namespace QulixProject.Services
{
    public class IdGenerator<TEntity> where TEntity : MainEntity
    {
        public int Genarate(List<TEntity> entities) //генерация id
        {
            return Max(entities) + 1;
        }

        private int Max(List<TEntity> entities) //получение максимального id
        {
            var max = 0;
            if (entities==null) return max;
            foreach (var entity in entities)
            {
                if (entity.Id>max)
                {
                    max = entity.Id;
                }
            }

            return max;
        }
    }
}
