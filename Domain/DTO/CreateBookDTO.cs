using System.ComponentModel.DataAnnotations;

namespace Books.Domain.DTO
{
	public class CreateBookDTO
	{
		[Required(ErrorMessage = "Name is required")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string? Description { get; set; }
	}
}
