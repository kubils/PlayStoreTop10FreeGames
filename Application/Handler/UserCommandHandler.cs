using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace Application.Handler
{
   public class UserCommandHandler : IRequestHandler<CreateUserCommand, bool>
   {
       private readonly IUserRepository _userRepository;
       private readonly IMapper _mapper;

       public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
       {
           _userRepository = userRepository;
           _mapper = mapper;
       }

       public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
       {
           var result = await _userRepository.Add(_mapper.Map<User>(request));

           return result != null;
       }
    }
}