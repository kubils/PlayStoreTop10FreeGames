using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ITopListDetailsQueryMongoRepository
    {
        Task<GameListDetails> GetTopGames();
        Task<GameDetails> GetGameDetailsWithTrackId(string trackId);

    }
}
