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
    public class StudentRepositoryImpl : IRepository<Model.Student>
    {
        private readonly ISession session;

        public StudentRepositoryImpl(ISession session)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public bool Delete(int id)
        {
            Student student = this.GetById(id);
            if (student == null)
            {
                return false;
            }

            ITransaction transaction = session.BeginTransaction();
            try
            {
                this.session.Delete(student);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public IList<Student> GetAll()
        {
            return this.session.Query<Student>().ToList();
        }

        public Student GetById(int id)
        {
            return this.session.Get<Student>(id);
        }

        public bool Save(Student entity)
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
            } catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}
