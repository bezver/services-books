using Microsoft.EntityFrameworkCore;
using Books.DAL.Contexts;

namespace Books.DAL.Repositories
{
	public class Repository<TEntity, TKey>(Context context) : IRepository<TEntity, TKey> where TEntity : class
	{
		private readonly Context _context = context;
		private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<TEntity?> GetByIdAsync(TKey id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task CreateAsync(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		public void Delete(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		#region dispose
		private bool disposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}

			disposed = true;
		}
		#endregion
	}
}
