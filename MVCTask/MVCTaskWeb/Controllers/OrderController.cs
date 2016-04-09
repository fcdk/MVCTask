using System.Web.Mvc;
using AutoMapper;
using MVCTask.Services;
using MVCTask.Models.Order;
using MVCTaskEF;
using MVCTaskModel.UnitOfWork;

namespace MVCTask.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBasketManager _basketManager;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper, IBasketManager basketManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _basketManager = basketManager;
        }

        public ViewResult Buy(string gamekey)
        {
            var model = new OrderDetailsViewModel
            {
                GameKey = gamekey,
                GameName = _unitOfWork.Games.GetByKey(gamekey).Name,
                Discount = 0.10f // fake discount
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Buy(OrderDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var basket = _basketManager.Basket;

                var orderDetails = _mapper.Map<OrderDetail>(model);
                orderDetails.CustomersOrderKey = basket.CustomersOrderKey;

                // calculation of price
                var game = _unitOfWork.Games.GetByKey(orderDetails.GameKey);
                if (game.Price != null && orderDetails.Quantity != null)
                    if(orderDetails.Discount != null)
                        orderDetails.Price = (decimal)(((double)game.Price) * orderDetails.Quantity - ((double)game.Price) * orderDetails.Quantity * ((double)orderDetails.Discount));
                    else orderDetails.Price = game.Price * orderDetails.Quantity;

                _unitOfWork.OrderDetails.Insert(orderDetails);
                _unitOfWork.Save();

                return RedirectToAction("Basket");
            }

            return View(model);
        }

        public ViewResult Basket()
        {
            var model = new BasketViewModel { Orders = _basketManager.Basket.OrderDetails };
            return View(model);
        }

    }
}