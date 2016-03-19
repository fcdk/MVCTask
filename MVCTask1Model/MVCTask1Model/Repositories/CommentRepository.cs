using System;
using System.Collections.Generic;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;

namespace MVCTask1Model.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MVCTask1Entities _dbEntities;

        public void Create(string gameKey, string name, string body, string parentCommentKey = null)
        {
            if (string.IsNullOrEmpty(gameKey) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(body) || parentCommentKey == String.Empty)
                return;            

            Comment comment = new Comment
            {
                CommentKey = Guid.NewGuid().ToString(),
                ParentCommentKey = parentCommentKey,
                Name = name,
                Body = body,
                GameKey = gameKey
            };

            if (parentCommentKey != null)
                comment.Body = comment.Body.Insert(0, "[" + _dbEntities.Comments.Find(parentCommentKey).Name + "] ");

            _dbEntities.Comments.Add(comment);
        }

        public IEnumerable<Comment> GetCommentsByGame(string gameKey)
        {
            return _dbEntities.Games.Find(gameKey).Comments;
        }

        public CommentRepository(MVCTask1Entities dbEntities)
        {
            _dbEntities = dbEntities;
        }
    }
}
