namespace SozonovBackend.Repository
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Update(T entity);
    }
}
