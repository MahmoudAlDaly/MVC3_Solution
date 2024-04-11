using System.ComponentModel.DataAnnotations;

namespace Demo_PL.ViewModels.User
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="username is required")]
        public string UserName { get; set; }



		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage ="invalid email")]
        public string Email { get; set; }


		[Required(ErrorMessage ="password is required")]
		[MinLength(5,ErrorMessage ="min pasword length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "confirm password is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage = "confirm password is not match with password")]
		public string ConfirmPassword { get; set; }


        public bool IsAgree { get; set; }

		[Required(ErrorMessage ="First name is required")]
		[Display(Name ="First Name")]
        public string FirstName { get; set; }


		[Required(ErrorMessage = "Last name is required")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

    }
}
