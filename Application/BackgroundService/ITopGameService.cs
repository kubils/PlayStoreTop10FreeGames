using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.BackgroundService
{
    public interface ITopGameService
    {
        Task<GameListDetails> GetFromListTopFreeGames();

        Task<bool> AddTopTenFreeGameToDb();
    }
}
