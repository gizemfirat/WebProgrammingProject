using System.Linq.Expressions;
using System.Linq;

namespace Repositories.Contracts
{
  public interface IRepositoryBase<T>
  {
    IQueryable<T> GetAll(bool trackChanges);
    T? Get(Expression<Func<T, bool>> expression, bool trackChanges);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
}