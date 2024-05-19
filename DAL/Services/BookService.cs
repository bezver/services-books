using Microsoft.Extensions.DependencyInjection;
using Books.DAL.Repositories;
using Books.Domain.Exceptions;
using Books.Domain.Models;
using System.Net;

namespace Books.DAL.Services
{
	public class BookService(IServiceProvider services) : IBookService
	{
		private readonly IRepository<Book, Guid> _bookRepository = services.GetRequiredService<IRepository<Book, Guid>>();

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _bookRepository.GetAllAsync();
		}

		public async Task<Book> GetByIdAsync(Guid id)
		{
			return await _bookRepository.GetByIdAsync(id) ??
				throw new ServiceException("Book does not exist", HttpStatusCode.NotFound);
		}

		public async Task CreateAsync(Book book)
		{
			try
			{
				await _bookRepository.CreateAsync(book);
				await _bookRepository.SaveAsync();
			}
			catch (Exception)
			{
				throw new ServiceException("Book creating error", HttpStatusCode.BadRequest);
			}
		}

		public async Task UpdateAsync(Book book)
		{
			try
			{
				_bookRepository.Update(book);
				await _bookRepository.SaveAsync();
			}
			catch (Exception)
			{
				throw new ServiceException("Book updating error", HttpStatusCode.BadRequest);
			}
		}

		public async Task DeleteAsync(Guid id)
		{
			Book book = await _bookRepository.GetByIdAsync(id) ??
				throw new ServiceException("Book does not exist", HttpStatusCode.NotFound);

			try
			{
				_bookRepository.Delete(book);
				await _bookRepository.SaveAsync();
			}
			catch (Exception)
			{
				throw new ServiceException("Book deleting error", HttpStatusCode.BadRequest);
			}
		}
	}
}
