using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment GetCommentsByGame(string gameKey);
    }
}
