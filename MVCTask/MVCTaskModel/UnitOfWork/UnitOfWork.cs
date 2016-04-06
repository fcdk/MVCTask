using System;
using MVCTaskEF;
using MVCTaskModel.Repositories;
using MVCTaskModel.RepositoryInterfaces;

namespace MVCTaskModel.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCTaskEntities _dbEntities = new MVCTaskEntities();
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
