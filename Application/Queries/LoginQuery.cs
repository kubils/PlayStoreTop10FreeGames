using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries
{
    public class LoginQuery : IRequest<string>
    {
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}