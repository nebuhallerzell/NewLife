using System.Linq.Expressions;

namespace NewLife.Models
{
    public interface IRepository<T> where T : class
    {
        //T -> Car Brand Araba Markası
        IEnumerable<T> GetAll(string? includeProps = null, string includeProperties = null);
        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null, string includeProperties = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteSelected(IEnumerable<T> entities);
    }
}
