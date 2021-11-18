using System.Collections.Generic;
using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
    }
}