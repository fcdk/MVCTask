namespace MVCTask1Model.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByKey(string key);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(string key);
    }
}
