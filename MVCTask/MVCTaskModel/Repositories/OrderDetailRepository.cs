using MVCTaskEF;
using MVCTaskModel.RepositoryInterfaces;

namespace MVCTaskModel.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(MVCTaskEntities dbEntities) : base(dbEntities) { }
    }
}
