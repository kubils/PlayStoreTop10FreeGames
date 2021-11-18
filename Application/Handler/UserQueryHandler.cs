using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Handler
{
    public class UserQueryHandler : IRequestHandler<LoginQuery, string>
                                    ,IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IGetJwtToken _tokenGenarator;

        public UserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserQueryHandler(IUserRepository userRepository, IMapper mapper, IGetJwtToken tokenGenarator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenGenarator = tokenGenarator;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Authenticate(request.Email, request.Password);

            if (user == null) return null;

            var result = await _tokenGenarator.GenerateToken(user);
            
            return result;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAll();
            return _mapper.Map<List<UserDto>>(result);
        }
    }
}