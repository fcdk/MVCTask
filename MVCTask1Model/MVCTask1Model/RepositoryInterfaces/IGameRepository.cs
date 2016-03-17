using System.Collections.Generic;
using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        void Update(Game game);
        void Delete(Game game);
        IEnumerable<Game> GetAllGames();
        Game GetGameByKey(string key);
        IEnumerable<Game> GetGamesByGenre(string genreName);
        IEnumerable<Game> GetGamesByPlatformType(string platformType);
    }
}
