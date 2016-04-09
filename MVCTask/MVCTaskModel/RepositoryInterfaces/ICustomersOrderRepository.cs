using MVCTaskEF;

namespace MVCTaskModel.RepositoryInterfaces
{
    public interface ICustomersOrderRepository : IRepository<CustomersOrder>
    {
        CustomersOrder GetCustomersOrderByCustomerKey(string customerKey);
    }
}
