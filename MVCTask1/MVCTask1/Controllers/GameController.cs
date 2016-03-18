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

        public JsonResult Games()
        {
            return Json(_unitOfWork.Games.GetAllGames().Select(game => new{ game.Name, game.Description }), JsonRequestBehavior.AllowGet);
        }
    }
}