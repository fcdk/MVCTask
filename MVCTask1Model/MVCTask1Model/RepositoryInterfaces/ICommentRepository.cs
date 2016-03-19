using System.Collections.Generic;
using MVCTask1EF;

namespace MVCTask1Model.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        void Create(string gameKey, string name, string body, string parentCommentKey = null);
        IEnumerable<Comment> GetCommentsByGame(string gameKey);
    }
}
