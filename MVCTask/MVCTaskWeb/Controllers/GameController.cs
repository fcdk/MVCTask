using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;
using AutoMapper;
using MVCTask.Models.Game;
using MVCTaskEF;
using MVCTaskModel.UnitOfWork;

namespace MVCTask.Controllers
{
    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public JsonResult All()
        {
            return Json(_unitOfWork.Games.GetAllGames().Select(game => new{ game.Name, game.Description }), JsonRequestBehavior.AllowGet);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = _mapper.Map<Game>(model);
                _unitOfWork.Games.Insert(game);
                _unitOfWork.Save();

                return RedirectToAction("All");
            }

            return View();
        }

        [HttpPost]
        public JsonResult Update(string key, string name, string description, decimal? price = null, short? unitsInStock = null, bool? discontinued = null)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException("name argument mustn`t be null and empty");

            Game game = _unitOfWork.Games.GetByKey(key);

            game.Name = name;
            game.Description = description;
            game.Price = price;
            game.UnitsInStock = unitsInStock;
            game.Discontinued = discontinued;

            _unitOfWork.Games.Update(game);
            _unitOfWork.Save();

            return Json("game with primary key " + key + " was updated");
        }

        public ViewResult Details(string key)
        {
            var game = _mapper.Map<GameDetailsViewModel>(_unitOfWork.Games.GetByKey(key));

            return View(game);
        }

        [HttpPost]
        public JsonResult Delete(string key)
        {
            _unitOfWork.Games.Delete(key);
            _unitOfWork.Save();

            return Json("game with primary key " + key + " was deleted");
        }

        //adding comment
        [HttpPost]
        public ActionResult AddComment(CommentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.Games.GetByKey(model.GameKey) == null)
                    throw new InvalidOperationException($"Game with ID={model.GameKey} was not found in the DB");

                var comment = _mapper.Map<Comment>(model);

                if (comment.ParentCommentKey != null)
                {
                    Comment parentComment = _unitOfWork.Comments.GetByKey(comment.ParentCommentKey);

                    comment.Body = comment.Body.Insert(0, "[" + parentComment.Name + "] ");
                }

                _unitOfWork.Comments.Insert(comment);
                _unitOfWork.Save();

                return RedirectToAction("Comments", new { key = model.GameKey });
            }

            model.AllComments = _unitOfWork.Comments.GetCommentsByGame(model.GameKey);

            return View("Comments", model);
        }

        public ViewResult Comments(string key)
        {
            var model = new CommentsViewModel();
            model.AllComments = _unitOfWork.Comments.GetCommentsByGame(key);
            model.GameKey = key;
            return View(model);
        }

        public ActionResult Download(string key)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                Game game = _unitOfWork.Games.GetByKey(key);
                bf.Serialize(ms, "Name: " + game.Name + Environment.NewLine + "Description: " + game.Description);
                return File(ms.ToArray(), "application/bin", "game.bin");
            }
        }

        [OutputCache(Duration = 60)]
        public PartialViewResult Total()
        {
            ViewBag.NumberOfGames = _unitOfWork.Games.GetAllGames().Count();
            return PartialView("_Total");
        }

    }
}