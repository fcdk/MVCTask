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

        ////[HttpPost("games/new/{name}/{description}")]
        ////[Route("games/new/{name}/{description}")]
        [AcceptVerbs("GET", "POST")]
        [Route("games/new/{name}/{description}")]
        public JsonResult New(string name, string description)
        {
            _unitOfWork.Games.Create(name, description);
            _unitOfWork.Save();
            return Json(name + " was created", JsonRequestBehavior.AllowGet);            
        }
    }
}