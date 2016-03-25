using System.Collections.Generic;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;
using System.Linq;

namespace MVCTask1Model.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(MVCTask1Entities dbEntities) : base(dbEntities) { }

        public IEnumerable<Game> GetAllGames()
        {
            return DbEntities.Games;
        }

        //get games by genreName and child genres of genre with genreName
        public IEnumerable<Game> GetGamesByGenre(string genreName)
        {
            List<Game> result = new List<Game>();

            Genre genre = DbEntities.Genres.First(g => g.Name == genreName);
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
            return DbEntities.Games.Where(game => game.PlatformTypeInGames.
                Any(platformTypeInGames => platformTypeInGames.PlatformType.Type == platformType));
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
