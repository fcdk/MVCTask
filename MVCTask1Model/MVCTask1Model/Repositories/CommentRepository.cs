using System;
using System.Collections.Generic;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;

namespace MVCTask1Model.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(MVCTask1Entities dbEntities) : base(dbEntities) { }
        
        public IEnumerable<Comment> GetCommentsByGame(string gameKey)
        {
            Game game = DbEntities.Games.Find(gameKey);

            if (game == null)
                throw new InvalidOperationException(string.Format("{0} with ID={1} was not found in the DB", typeof(Comment).Name, gameKey));

            return game.Comments;
        }        
    }
}
