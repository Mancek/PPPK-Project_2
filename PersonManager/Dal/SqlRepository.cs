using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Zadatak.Utils;

namespace Zadatak.Dal
{
    class SqlRepository : ISqlRepository
    {
        public static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString, "fru1tc@k3");

        public void Add<T>(T item)
        {
            Type repository = GetRepositoryType<T>();
            // Invoke Add method on specific repository class
            repository.GetMethod(MethodBase.GetCurrentMethod().Name).MakeGenericMethod(typeof(T)).Invoke(Activator.CreateInstance(repository), new object[] { item });
        }

        public void Delete<T>(T item)
        {
            Type repository = GetRepositoryType<T>();
            // Invoke Delete method on specific repository class
            repository.GetMethod(MethodBase.GetCurrentMethod().Name).MakeGenericMethod(typeof(T)).Invoke(Activator.CreateInstance(repository), new object[] { item });
        }

        public IList<T> GetList<T>()
        {
            Type repository = GetRepositoryType<T>();
            // Invoke GetList method on specific repository class
            return (IList<T>)repository.GetMethod(MethodBase.GetCurrentMethod().Name).MakeGenericMethod(typeof(T)).Invoke(Activator.CreateInstance(repository), null);
        }

        public T Get<T>(int id)
        {
            Type repository = GetRepositoryType<T>();
            // Invoke Get method on specific repository class
            object item = repository.GetMethod(MethodBase.GetCurrentMethod().Name).MakeGenericMethod(typeof(T)).Invoke(Activator.CreateInstance(repository), new object[] { id });
            return (T)item;
        }

        public void Update<T>(T item)
        {
            Type repository = GetRepositoryType<T>();
            // Invoke Update method on specific repository class
            repository.GetMethod(MethodBase.GetCurrentMethod().Name).MakeGenericMethod(typeof(T)).Invoke(Activator.CreateInstance(repository), new object[] { item });
        }
        private Type GetRepositoryType<T>()
        {
            // Get current class name "Repository" + generics class name e.g. RepositoryPerson 
            string typename = MethodBase.GetCurrentMethod().DeclaringType.FullName + typeof(T).Name;
            // Make type from typename
            Type repository = Type.GetType(typename);
            return repository;
        }
    }
}
