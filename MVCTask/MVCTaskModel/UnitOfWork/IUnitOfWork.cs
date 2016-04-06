using System;
using MVCTaskModel.Repositories;
using MVCTaskModel.RepositoryInterfaces;

namespace MVCTaskModel.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        ICommentRepository Comments { get; }
        void Save();
    }
}
