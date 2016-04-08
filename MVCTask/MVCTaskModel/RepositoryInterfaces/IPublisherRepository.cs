using MVCTaskEF;

namespace MVCTaskModel.RepositoryInterfaces
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Publisher GetPublisherByName(string name);
    }
}
