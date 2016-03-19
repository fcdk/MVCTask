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

        public void Create(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name argument mustn`t be null and empty");

            Game game = new Game
            {
                GameKey = Guid.NewGuid().ToString(),
                Name = name,
                Description = description != String.Empty ? description : null
            };

            _dbEntities.Games.Add(game);
        }

        public void Update(string key, string name, string description)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(name))
                throw new ArgumentException("key and name arguments must not be null and empty");

            Game game = _dbEntities.Games.Find(key);

            if(game == null)
                throw new ArgumentException("DB doesn`t contain game with such primary key");

            game.Name = name;
            game.Description = description != String.Empty ? description : null;
            _dbEntities.Entry(game).State = EntityState.Modified;
        }

        public void Delete(string key)
        {
            Game game = _dbEntities.Games.Find(key);

            if (game == null)
                throw new ArgumentException("DB doesn`t contain game with such primary key");

            _dbEntities.Games.Remove(game);
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _dbEntities.Games;
        }

        public Game GetGameByKey(string key)
        {
            Game game = _dbEntities.Games.Find(key);

            if (game == null)
                throw new ArgumentException("DB doesn`t contain the game with such primary key");
            
            return game;
        }

        //get games by genreName and child genres of genre with genreName
        public IEnumerable<Game> GetGamesByGenre(string genreName)
        {
            List<Game> result = new List<Game>();

            Genre genre = _dbEntities.Genres.First(g => g.Name == genreName);
            if (genre == null)
                return null;
            result.AddRange(genre.GenreInGames.Select(genreInGame => genreInGame.Game));

            List<Genre> parentGenres = new List<Genre>();
            GetAllChildGenres(genre, ref parentGenres);
            if (parentGenres.Count == 0)
                return result.Count == 0 ? null : result;
            foreach (var g in parentGenres)
            {
                result.AddRange(g.GenreInGames.Select(genreInGame => genreInGame.Game));
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


        //recursive function: in resultChildGenres will be all child genres of genre argument
        private void GetAllChildGenres(Genre genre, ref List<Genre> resultChildGenres)
        {
            IEnumerable<Genre> parentGenres = genre?.Genre1;

            if (parentGenres == null)
                return;
            foreach (var g in parentGenres)
            {
                resultChildGenres.Add(g);
                GetAllChildGenres(g, ref resultChildGenres);
            }
        }
    }
}
