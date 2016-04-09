using System.Collections.Generic;
using System.Web;
using MVCTaskEF;

namespace MVCTask.Models.Basket
{
    public class BasketService
    {
        private readonly string _customerKey;
        private const string _basketSessionKey = "basket";

        public BasketService(string customerKey)
        {
            _customerKey = customerKey;
        }

        public CustomersOrder Basket
        {
            get
            {
                var basket = HttpContext.Current.Session[_basketSessionKey] as CustomersOrder;

                if (basket == null)
                {
                    HttpContext.Current.Session[_basketSessionKey] = basket = new CustomersOrder
                    {
                        CustomerKey = _customerKey,
                        OrderDetails = new List<OrderDetail>()
                    };
                }

                return basket;
            }
        }

    }
}
