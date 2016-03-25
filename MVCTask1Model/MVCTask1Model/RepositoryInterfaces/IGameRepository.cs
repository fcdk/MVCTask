using System.Collections.Generic;
using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAllGames();
        IEnumerable<Game> GetGamesByGenre(string genreName);
        IEnumerable<Game> GetGamesByPlatformType(string platformType);
    }
}
