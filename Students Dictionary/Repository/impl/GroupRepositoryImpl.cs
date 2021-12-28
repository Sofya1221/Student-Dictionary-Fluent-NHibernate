using NHibernate;
using NHibernate.Linq;
using StudentsDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Repository.impl
{
    public class GroupRepositoryImpl : IRepository<Model.Group>
    {
        private readonly ISession session;

        public GroupRepositoryImpl(ISession session)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public bool Delete(int id)
        {
            Group group = this.GetById(id);
            if (group == null)
            {
                return false;
            }

            ITransaction transaction = session.BeginTransaction();
            try
            {
                this.session.Delete(group);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public IList<Group> GetAll()
        {
            return this.session.Query<Group>().ToList();
        }

        public Group GetById(int id)
        {
            return this.session.Get<Group>(id);
        }

        public bool Save(Group entity)
        {
            if (entity == null)
            {
                return false;
            }

            ITransaction transaction = session.BeginTransaction();
            try
            {
                this.session.Save(entity);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}
