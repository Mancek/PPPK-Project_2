using System.Collections.Generic;

namespace Zadatak.Dal
{
    interface ISqlRepository
    {
        void Add<T>(T item);
        void Delete<T>(T item);
        IList<T> GetList<T>();
        T Get<T>(int id);
        void Update<T>(T item);
    }
}