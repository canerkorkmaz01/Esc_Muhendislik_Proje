using Web_App_Data.Infrastructure;

namespace Web_App_Data.Interface
{
    public interface IRepository
    {
        public interface IRepository<T> where T : BaseEntity
        {
            Task<IEnumerable<T>> GetAllAsync();
            Task<T> GetByIdAsync(int id);
            Task AddAsync(T entity);
            void Update(T entity);
            void Delete(T entity);
            Task SaveChangesAsync();
            Task AddRangeAsync(IEnumerable<T> entities);
        }
    }
}
