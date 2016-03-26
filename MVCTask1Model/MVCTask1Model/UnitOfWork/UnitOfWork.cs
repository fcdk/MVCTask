using System;
using MVCTask1EF;
using MVCTask1Model.Repositories;
using MVCTask1Model.RepositoryInterfaces;

namespace MVCTask1Model.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCTask1Entities _dbEntities = new MVCTask1Entities();
        private IGameRepository _gameRepository;
        private ICommentRepository _commentRepository;
        private bool _disposed = false;

        public IGameRepository Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GameRepository(_dbEntities);
                return _gameRepository;                
            }
        }

        public ICommentRepository Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_dbEntities);
                return _commentRepository;
            }
        }

        public void Save()
        {
            _dbEntities.SaveChanges();
        }        
        
        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        private void CleanUp(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbEntities.Dispose();
                }
            }
            _disposed = true;
        }

    }
}
