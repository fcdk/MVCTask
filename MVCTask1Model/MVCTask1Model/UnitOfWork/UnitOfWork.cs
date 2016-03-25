using System;
using MVCTask1EF;
using MVCTask1Model.Repositories;

namespace MVCTask1Model.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCTask1Entities _dbEntities = new MVCTask1Entities();
        private GameRepository _gameRepository;
        private CommentRepository _commentRepository;
        private bool _disposed = false;

        public GameRepository Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GameRepository(_dbEntities);
                return _gameRepository;                
            }
        }

        public CommentRepository Comments
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
