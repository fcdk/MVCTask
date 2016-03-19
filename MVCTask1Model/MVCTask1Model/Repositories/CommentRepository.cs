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
            if (string.IsNullOrEmpty(gameKey) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(body))
                throw new ArgumentException("gameKey, name and body arguments mustn`t be null and empty");

            if (_dbEntities.Games.Find(gameKey) == null)
                throw new ArgumentException("DB doesn`t contain game with such primary key like gameKey argument");

            Comment comment = new Comment
            {
                CommentKey = Guid.NewGuid().ToString(),
                ParentCommentKey = parentCommentKey != String.Empty ? parentCommentKey : null,
                Name = name,
                Body = body,
                GameKey = gameKey
            };

            if (parentCommentKey != null)
            {
                Comment parentComment = _dbEntities.Comments.Find(parentCommentKey);

                if (parentComment == null)
                    throw new ArgumentException("DB doesn`t contain comment with such primary key like parentCommentKey argument");

                comment.Body = comment.Body.Insert(0, "[" + parentComment.Name + "] ");
            }            

            _dbEntities.Comments.Add(comment);
        }

        public IEnumerable<Comment> GetCommentsByGame(string gameKey)
        {
            Game game = _dbEntities.Games.Find(gameKey);

            if (game == null)
                throw new ArgumentException("DB doesn`t contain game with such primary key like gameKey argument");

            return game.Comments;
        }

        public CommentRepository(MVCTask1Entities dbEntities)
        {
            _dbEntities = dbEntities;
        }
    }
}
