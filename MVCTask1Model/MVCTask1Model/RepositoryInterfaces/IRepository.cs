using System;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface IRepository<T>
        where T: class
    {
        void Create(T item);
    }
}
