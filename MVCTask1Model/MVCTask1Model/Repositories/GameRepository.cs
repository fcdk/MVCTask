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
        private readonly MVCTask1Entities _dbEntities;
        private bool _disposed = false;

        public void Create(Game item)
        {
            if (item != null)
                _dbEntities.Games.Add(item);
        }        

        public void Update(Game game)
        {
            if (_dbEntities != null && game != null)
                _dbEntities.Entry(game).State = EntityState.Modified;
        }

        public void Delete(string key)
        {
            Game game = _dbEntities.Games.Find(key);
            if (game != null)
                _dbEntities.Games.Remove(game);
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _dbEntities.Games;
        }

        public Game GetGameByKey(string key)
        {            
            return _dbEntities.Games.Find(key);
        }

        //get games by genreName and parent genres of genre with genreName
        public IEnumerable<Game> GetGamesByGenre(string genreName)
        {
            List<Game> result = new List<Game>();

            Genre genre = _dbEntities.Genres.First(g => g.Name == genreName);

            while (genre != null)
            {
                result.AddRange(_dbEntities.Games.Where(game => game.GenreInGames.
                    Any(genreInGames => genreInGames.Genre.Name == genre.Name)));

                genre = genre.Genre2;
            }

            return result.Count == 0 ? null : result;
        }

        public IEnumerable<Game> GetGamesByPlatformType(string platformType)
        {
            return _dbEntities.Games.Where(game => game.PlatformTypeInGames.
                Any(platformTypeInGames => platformTypeInGames.PlatformType.Type == platformType));
        }        

        public GameRepository(MVCTask1Entities dbEntities)
        {
            _dbEntities = dbEntities;
        }
    }
}
