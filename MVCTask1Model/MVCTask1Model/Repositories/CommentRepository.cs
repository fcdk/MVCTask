using System;
using System.Collections.Generic;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;

namespace MVCTask1Model.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MVCTask1Entities _dbEntities;

        public void Create(Game game, string name, string body, Comment parentComment = null)
        {
            Comment comment = new Comment
            {
                CommentKey = Guid.NewGuid().ToString(),
                ParentCommentKey = parentComment?.CommentKey,
                Name = name,
                Body = body,
                GameKey = game.GameKey
            };


            if (comment.Name != null && comment.Body != null && comment.GameKey != null && comment.Name != String.Empty
                && comment.Body != String.Empty && comment.GameKey != String.Empty)
            {
                if (parentComment != null)
                    comment.Body = comment.Body.Insert(0, "[" + parentComment.Name + "] ");
                _dbEntities.Comments.Add(comment);
            }            
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
