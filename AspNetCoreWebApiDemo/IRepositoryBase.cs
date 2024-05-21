namespace AspNetCoreWebApiDemo
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetListAsync();
        void Add(T entity);
        Task<T> GetInfo(int id);
    }

    public interface ITodoRepository: IRepositoryBase<Todo>
    {

    }
}
