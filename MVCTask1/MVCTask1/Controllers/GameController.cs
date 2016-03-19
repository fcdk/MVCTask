using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Microsoft.Web.Infrastructure;
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
            _unitOfWork.Games.Create(name, description);
            _unitOfWork.Save();
            return Json(name + " was created");
        }
    }
}