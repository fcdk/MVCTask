using System.Collections.Generic;
using MVCTaskEF;

namespace MVCTaskModel.RepositoryInterfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetAllGames();
        IEnumerable<Game> GetGamesByGenre(string genreName);
        IEnumerable<Game> GetGamesByPlatformType(string platformType);
    }
}
