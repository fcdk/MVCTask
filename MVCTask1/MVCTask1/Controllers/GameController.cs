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

    }
}