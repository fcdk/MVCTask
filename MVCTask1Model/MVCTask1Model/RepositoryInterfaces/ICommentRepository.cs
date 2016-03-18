using System.Collections.Generic;
using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        void Create(Game game, string name, string body, Comment parentComment = null);
        IEnumerable<Comment> GetCommentsByGame(Game game);
    }
}
