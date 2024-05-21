using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApiDemo
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T: class
    {
        public DbContext DbContext { get; set; }
        public RepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();
        }

        public Task<IEnumerable<T>> GetListAsync()
        {
            return Task.FromResult(DbContext.Set<T>().AsEnumerable());
        }

        public async Task<T> GetInfo(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }
    }

    public class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        public TodoDb TodoDbContext { get;}
        private ITodoRepository _todoRepository = null;
        public RepositoryWrapper(TodoDb todoDb)
        {
            TodoDbContext = todoDb;
        }

        public ITodoRepository Todo => _todoRepository ?? new TodoRepository(TodoDbContext);
    }
}
