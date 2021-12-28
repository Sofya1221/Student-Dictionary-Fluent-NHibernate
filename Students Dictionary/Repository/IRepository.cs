using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Repository
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);

        IList<TEntity> GetAll();

        bool Save(TEntity entity);

        bool Delete(int id);

    }
}
