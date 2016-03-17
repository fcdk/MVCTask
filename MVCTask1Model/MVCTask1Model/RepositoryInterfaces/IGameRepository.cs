using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        void Edit(Game game);
        void Delete(Game game);
        Game[] GetAllGames();
        Game GetGameByKey(string key);
        Game[] GetGamesByGenre(string genreName);
        Game[] GetGamesByPlatformType(string platformType);
    }
}
