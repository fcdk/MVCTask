using System.Collections.Generic;
using MVCTaskEF;

namespace MVCTask.Models.Order
{
    public class BasketViewModel
    {
        public IEnumerable<OrderDetail> Orders { get; set; }
    }
}
