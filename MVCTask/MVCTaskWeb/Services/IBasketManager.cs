using MVCTaskEF;

namespace MVCTask.Services
{
    public interface IBasketManager
    {
        CustomersOrder Basket { get; }
    }
}
