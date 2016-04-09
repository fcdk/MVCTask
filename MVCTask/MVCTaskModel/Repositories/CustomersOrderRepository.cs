using System.Linq;
using MVCTaskEF;
using MVCTaskModel.RepositoryInterfaces;

namespace MVCTaskModel.Repositories
{
    public class CustomersOrderRepository : Repository<CustomersOrder>, ICustomersOrderRepository
    {
        public CustomersOrderRepository(MVCTaskEntities dbEntities) : base(dbEntities) { }

        public CustomersOrder GetCustomersOrderByCustomerKey(string customerKey)
        {
            return DbEntities.CustomersOrders.FirstOrDefault(x => x.CustomerKey == customerKey);
        }
    }
}
