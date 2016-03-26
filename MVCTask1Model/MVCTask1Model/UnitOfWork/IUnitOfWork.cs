using System;
using MVCTask1Model.Repositories;
using MVCTask1Model.RepositoryInterfaces;

namespace MVCTask1Model.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        ICommentRepository Comments { get; }
        void Save();
    }
}
