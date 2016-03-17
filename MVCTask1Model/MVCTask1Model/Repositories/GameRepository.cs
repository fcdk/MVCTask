using System;
using System.Collections.Generic;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;
using System.Data.Entity;
using System.Linq;

namespace MVCTask1Model.Repositories
{
    public class GameRepository : IGameRepository
    {
        private MVCTask1Entities _dbEntities;
        private bool _disposed = false;

        public void Create(Game item)
        {
            if (item != null)
                _dbEntities?.Games.Add(item);
        }        

        public void Update(Game game)
        {
            if (_dbEntities != null && game != null)
                _dbEntities.Entry(game).State = EntityState.Modified;
        }

        public void Delete(Game game)
        {
            // тут надо на id поменять!!!!!!!!!!!
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _dbEntities?.Games;
        }

        public Game GetGameByKey(string key)
        {            
            return _dbEntities?.Games.Find(key);
        }

        public IEnumerable<Game> GetGamesByGenre(string genreName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Game> GetGamesByPlatformType(string platformType)
        {
            ////_dbEntities.Games.Where(game => game.PlatformTypeInGames.Any())
            throw new System.NotImplementedException();
        }

        private void CleanUp(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbEntities.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        public GameRepository(MVCTask1Entities dbEntities)
        {
            _dbEntities = dbEntities;
        }
    }
}
