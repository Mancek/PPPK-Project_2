using System;

namespace Zadatak.Dal
{
    static class RepositoryFactory
    {
        private static readonly Lazy<ISqlRepository> repository = new Lazy<ISqlRepository>(() => new SqlRepository());
        public static ISqlRepository GetRepository() => repository.Value;
    }
}
