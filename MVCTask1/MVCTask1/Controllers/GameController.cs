using System;
using System.Linq;
using System.Web.Mvc;
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
        public JsonResult Games()
        {
            return Json(_unitOfWork.Games.GetAllGames().Select(game => new{ game.Name, game.Description }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("games/new")]
        public JsonResult New(string name, string description)
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
        public JsonResult Update(string key, string name, string description)
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
    }
}