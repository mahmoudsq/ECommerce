using ECommerce.Core.Common;
using System.Linq.Expressions;

namespace ECommerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<Result<IReadOnlyList<T>>> GetAll();
        Task<Result<IReadOnlyList<T>>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<Result<T?>> GetById(int id);
        Task<Result<T?>> GetById(int id, params Expression<Func<T, object>>[] includes);
        Task<Result> Add(T entity);
        Task<Result> Update(T entity);
        Task<Result> Delete(int id);
    }
}
