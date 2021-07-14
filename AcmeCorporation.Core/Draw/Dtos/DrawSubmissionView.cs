using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw.Dtos
{
	public class DrawSubmissionView
	{
		[Required(ErrorMessage = "Please enter the user's first name.")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Please enter the user's last name.")]
		public string LastName { get; set; }
		[EmailAddress(ErrorMessage = "The Email Address is not valid")]
		[Required(ErrorMessage = "Please enter an email address.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please enter a password")]
		public string Password { get; set; }
		public Guid Serial { get; set; }

		public bool AboveEighteen { get; set; }
	}
}
