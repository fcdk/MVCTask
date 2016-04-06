using System;
using System.Collections.Generic;
using MVCTaskEF;
using MVCTaskModel.RepositoryInterfaces;

namespace MVCTaskModel.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(MVCTaskEntities dbEntities) : base(dbEntities) { }
        
        public IEnumerable<Comment> GetCommentsByGame(string gameKey)
        {
            Game game = DbEntities.Games.Find(gameKey);

            if (game == null)
                throw new InvalidOperationException(string.Format("{0} with ID={1} was not found in the DB", typeof(Comment).Name, gameKey));

            return game.Comments;
        }        
    }
}
