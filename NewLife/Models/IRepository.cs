using System.Linq.Expressions;

namespace NewLife.Models
{
    public interface IRepository<T> where T : class
    {
       IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProps = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteSelected(IEnumerable<T> entities);
    }
}
