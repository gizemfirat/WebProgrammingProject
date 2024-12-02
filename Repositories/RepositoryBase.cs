using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
  public abstract class RepositoryBase<T> : IRepositoryBase<T>
  where T : class, new()
  {

    protected readonly RepositoryContext _context;

    protected RepositoryBase(RepositoryContext context)
    {
      _context = context;
    }
    public IQueryable<T> GetAll(bool trackChanges)
    {
      return trackChanges
      ? _context.Set<T>()
      : _context.Set<T>().AsNoTracking();
    }
    public T? Get(Expression<Func<T, bool>> expression, bool trackChanges)
    {
      return trackChanges
      ? _context.Set<T>().Where(expression).SingleOrDefault()
      : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
    }
    public void Add(T entity)
    {
      _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
      _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
    }

  }
}