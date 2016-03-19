using System;
using System.Linq;
using System.Web.Mvc;
using MVCTask1EF;
using MVCTask1Model;

namespace MVCTask1.Controllers
{
    public class GameController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public GameController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [Route("games")]
        public JsonResult GetAllGames()
        {
            return Json(_unitOfWork.Games.GetAllGames().Select(game => new{ game.Name, game.Description }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("games/new")]
        public JsonResult CreateGame(string name, string description)
        {
            try
            {
                _unitOfWork.Games.Create(name, description);
            }
            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }

            _unitOfWork.Save();

            return Json(name + " was created");
        }

        [HttpPost]
        [Route("games/update")]
        public JsonResult UpdateGame(string key, string name, string description)
        {
            try
            {
                _unitOfWork.Games.Update(key, name, description);
            }
            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
            
            _unitOfWork.Save();

            return Json("game with primary key " + key + " was updated");
        }

        [Route("game/{key}")]
        public JsonResult GetGameByKey(string key)
        {
            try
            {
                Game game = _unitOfWork.Games.GetGameByKey(key);

                return Json(new { name = game.Name, description = game.Description }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("games/remove")]
        public JsonResult DeleteGame(string key)
        {
            try
            {
                _unitOfWork.Games.Delete(key);
            }
            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }

            _unitOfWork.Save();

            return Json("game with primary key " + key + " was deleted");
        }

        [HttpPost]
        [Route("game/{key}/newcomment")]
        public JsonResult AddCommentToGame(string key, string name, string body, string parentCommentKey = null)
        {
            try
            {
                _unitOfWork.Comments.Create(key, name, body, parentCommentKey);
            }
            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }

            _unitOfWork.Save();

            return Json("user " + name + " has posted the comment: " + body);
        }

        [Route("game/{key}/comments")]
        public JsonResult GetAllCommentsByGame(string key)
        {
            try
            {
                return Json(_unitOfWork.Comments.GetCommentsByGame(key).Select(comment =>
                    new { game = _unitOfWork.Games.GetGameByKey(comment.GameKey).Name , name = comment.Name, comment.Body }),
                    JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}