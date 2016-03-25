using System;
using MVCTask1Model.Repositories;

namespace MVCTask1Model.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        GameRepository Games { get; }
        CommentRepository Comments { get; }
        void Save();
    }
}
