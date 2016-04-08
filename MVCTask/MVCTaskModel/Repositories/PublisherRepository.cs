using System.Linq;
using MVCTaskEF;
using MVCTaskModel.RepositoryInterfaces;

namespace MVCTaskModel.Repositories
{
    class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(MVCTaskEntities dbEntities) : base(dbEntities) { }
        
        public Publisher GetPublisherByName(string name)
        {
            return DbEntities.Publishers.First(x => x.CompanyName == name);
        }
    }
}
