using System;
using System.Runtime.Remoting.Channels;
using MVCTaskEF;
using MVCTaskModel.UnitOfWork;

namespace MVCTask.Services
{
    public class BasketManager : IBasketManager
    {
        private readonly ICustomerService _customer;
        private readonly IUnitOfWork _unitOfWork;

        public BasketManager(ICustomerService customer, IUnitOfWork unitOfWork)
        {
            _customer = customer;
            _unitOfWork = unitOfWork;
        }

        public CustomersOrder Basket
        {
            get
            {
                var basket = _unitOfWork.CustomersOrders.GetCustomersOrderByCustomerKey(_customer.CustomerKey);
                if (basket == null)
                {
                    basket = new CustomersOrder
                    {
                        CustomersOrderKey = Guid.NewGuid().ToString(),
                        CustomerKey = _customer.CustomerKey,
                        OrderDate = DateTime.Now
                    };

                    _unitOfWork.CustomersOrders.Insert(basket);
                    _unitOfWork.Save();
                }

                return basket;
            }
        }
    }
}
