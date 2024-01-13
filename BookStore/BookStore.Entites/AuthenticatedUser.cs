using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entites
{
	public class AuthenticatedUser
	{
		public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string ContactNumber { get; set; }
		public string Address { get; set; }
		public int RoleId { get; set; }
	}
}
