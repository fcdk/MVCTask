using System.Collections.Generic;
using MVCTaskEF;

namespace MVCTaskModel.RepositoryInterfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetCommentsByGame(string gameKey);
    }
}
