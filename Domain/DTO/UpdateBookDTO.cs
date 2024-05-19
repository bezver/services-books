using System.ComponentModel.DataAnnotations;

namespace Books.Domain.DTO
{
	public class UpdateBookDTO
	{
		[Required(ErrorMessage = "Id is required")]
		public Guid Id { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
