namespace Books.DAL.Repositories
{
	public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity?> GetByIdAsync(TKey id);
		Task CreateAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		Task SaveAsync();
	}
}
