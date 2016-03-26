using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;
using MVCTask1EF;
using MVCTask1Model.UnitOfWork;

namespace MVCTask1.Controllers
{
    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public JsonResult GetAllGames()
        {
            return Json(_unitOfWork.Games.GetAllGames().Select(game => new{ game.Name, game.Description }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateGame(string name, string description)
        {
            _unitOfWork.Games.Insert(new Game { Name = name, Description = description });
            _unitOfWork.Save();

            return Json(name + " was created");
        }

        [HttpPost]
        public JsonResult UpdateGame(string key, string name, string description)
        {
            Game game = _unitOfWork.Games.GetByKey(key);
            game.Name = name;
            game.Description = description;
            _unitOfWork.Games.Update(game);
            _unitOfWork.Save();

            return Json("game with primary key " + key + " was updated");
        }

        public JsonResult GetGameByKey(string key)
        {
            Game game = _unitOfWork.Games.GetByKey(key);

            return Json(new { game.GameKey, game.Name, game.Description }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteGame(string key)
        {
            _unitOfWork.Games.Delete(key);

            _unitOfWork.Save();

            return Json("game with primary key " + key + " was deleted");
        }

        [HttpPost]
        public JsonResult AddCommentToGame(string gameKey, string name, string body, string parentCommentKey = null)
        {
            if (string.IsNullOrEmpty(gameKey) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(body))
                throw new ArgumentException("gameKey, name and body arguments mustn`t be null and empty");

            if (_unitOfWork.Games.GetByKey(gameKey) == null)
                throw new InvalidOperationException($"Game with ID={gameKey} was not found in the DB");

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
                Comment parentComment = _unitOfWork.Comments.GetByKey(parentCommentKey);

                if (parentComment == null)
                    throw new InvalidOperationException($"Comment with ID={parentCommentKey} was not found in the DB");

                comment.Body = comment.Body.Insert(0, "[" + parentComment.Name + "] ");
            }

            _unitOfWork.Comments.Insert(comment);

            _unitOfWork.Save();

            return Json("user " + name + " has posted the comment: " + body);
        }

        public JsonResult GetAllCommentsByGame(string key)
        {
            return Json(_unitOfWork.Comments.GetCommentsByGame(key).Select(comment =>
                    new { comment.CommentKey, comment.GameKey, Game = _unitOfWork.Games.GetByKey(comment.GameKey).Name, comment.Name, comment.Body }),
                    JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadGame(string key)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                Game game = _unitOfWork.Games.GetByKey(key);
                bf.Serialize(ms, "Name: " + game.Name + Environment.NewLine + "Description: " + game.Description);
                return File(ms.ToArray(), "application/bin", "game.bin");
            }
        }

    }
}