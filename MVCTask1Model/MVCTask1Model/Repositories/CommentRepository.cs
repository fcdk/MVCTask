using System.Collections.Generic;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;

namespace MVCTask1Model.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MVCTask1Entities _dbEntities;

        public void Create(Comment item)
        {
            if (item != null)
                _dbEntities?.Comments.Add(item);
        }

        public IEnumerable<Comment> GetCommentsByGame(Game game)
        {
            return game.Comments;
        }

        public CommentRepository(MVCTask1Entities dbEntities)
        {
            _dbEntities = dbEntities;
        }
    }
}
