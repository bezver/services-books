using Microsoft.AspNetCore.Mvc;
using Books.DAL.Services;
using Books.Domain.DTO;
using Books.Domain.Models;

namespace Books.Controllers
{
	[ApiController]
	[Route("/books")]
	public class BookController(IBookService bookService) : ControllerBase
	{
		private readonly IBookService _bookService = bookService;

		[HttpGet]
		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _bookService.GetAllAsync();
		}

		[HttpGet("{id}")]
		public async Task<Book> GetByIdAsync([FromRoute] Guid id)
		{
			return await _bookService.GetByIdAsync(id);
		}

		[HttpPost]
		public async Task CreateAsync([FromBody] CreateBookDTO bookDTO)
		{
			Book book = new()
			{
				Id = Guid.NewGuid(),
				Name = bookDTO.Name,
				Description = bookDTO.Description
			};

			await _bookService.CreateAsync(book);
		}

		[HttpPut]
		public async Task UpdateAsync([FromBody] UpdateBookDTO bookDTO)
		{
			Book book = await _bookService.GetByIdAsync(bookDTO.Id);

			if (!string.IsNullOrWhiteSpace(bookDTO.Name))
			{
				book.Name = bookDTO.Name;
			}

			if (!string.IsNullOrWhiteSpace(bookDTO.Description))
			{
				book.Description = bookDTO.Description;
			}

			await _bookService.UpdateAsync(book);
		}

		[HttpDelete("{id}")]
		public async Task Delete([FromRoute] Guid id)
		{
			await _bookService.DeleteAsync(id);
		}
	}
}
