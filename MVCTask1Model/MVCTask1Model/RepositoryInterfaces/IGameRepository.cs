using System.Collections.Generic;
using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface IGameRepository
    {
        void Create(string name, string description);
        void Update(Game game, string name, string description);
        void Delete(string key);
        IEnumerable<Game> GetAllGames();
        Game GetGameByKey(string key);
        IEnumerable<Game> GetGamesByGenre(string genreName);
        IEnumerable<Game> GetGamesByPlatformType(string platformType);
    }
}
