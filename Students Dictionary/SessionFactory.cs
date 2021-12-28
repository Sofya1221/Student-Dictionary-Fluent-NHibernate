using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary
{
    // Создаем фактори для нашей сессии
    public class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new Object();

        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            iSessionFactory = BuildSessionFactory();
                        }
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            try
            {
                // Для подключения используем MySQL сервер
                string connectionString = System.Configuration.ConfigurationManager.AppSettings["connection_string"];

                return Fluently.Configure()
                     .Database(MySQLConfiguration.Standard
                     .ConnectionString(connectionString))
                     .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                     .ExposeConfiguration(BuildSchema)
                     .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        /* Создаем сессию с объектами с неймспейсом StudentsDictionary.Model */
        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(t => t.Namespace == "StudentsDictionary.Model");
        }

        private static void BuildSchema(Configuration config)
        {
        }

    }
}
