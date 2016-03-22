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

        public ActionResult DownloadGame(string key)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    Game game = _unitOfWork.Games.GetGameByKey(key);
                    bf.Serialize(ms, "Name: " + game.Name + Environment.NewLine + "Description: " + game.Description);
                    return File(ms.ToArray(), "application/bin", "game.bin");
                }                
            }
            catch (ArgumentException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}