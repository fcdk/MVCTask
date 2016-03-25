using System.Collections.Generic;
using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsByGame(string gameKey);
    }
}
