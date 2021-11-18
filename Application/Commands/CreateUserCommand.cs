using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(150, ErrorMessage = "Must be between 1 and 150 characters", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(150, ErrorMessage = "Must be between 1 and 150 characters", MinimumLength = 1)]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{5,255}$", 
            ErrorMessage = "Passwords must be at least 5 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}