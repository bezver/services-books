using Books.Domain.Models;

namespace Books.DAL.Services
{
	public interface IBookService
	{
		Task<IEnumerable<Book>> GetAllAsync();
		Task<Book> GetByIdAsync(Guid id);
		Task CreateAsync(Book book);
		Task UpdateAsync(Book book);
		Task DeleteAsync(Guid id);
	}
}
