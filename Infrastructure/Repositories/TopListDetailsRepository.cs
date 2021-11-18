using Domain;
using Domain.IRepositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class TopListDetailsRepository : BaseMongoRepository, ITopListDetailsCommandMongoRepository
                                            , ITopListDetailsQueryMongoRepository
    {
        private readonly IMongoCollection<GameListDetails> _mongoCollection;

        public TopListDetailsRepository(IMongoDbSettings mongoDbSettings) : base(mongoDbSettings)
        {
            _mongoCollection = MongoDatabase.GetCollection<GameListDetails>(mongoDbSettings.TopFreeGamesCollectionName);
        }

        public async Task AddTopGames(GameListDetails gameListDetails)
        {
            await _mongoCollection.InsertOneAsync(gameListDetails);
        }

        public async Task<GameListDetails> GetTopGames()
        {

            var result = await _mongoCollection.Find(_ => true)
                .SortByDescending(s => s.CreateDate).FirstAsync();

            return result;

        }

        public Task<GameDetails> GetGameDetailsWithTrackId(string trackId)
        {
           
            GameDetails result = new();

            var list =  _mongoCollection.Find(_ => true).SortByDescending(s => s.CreateDate).ToList();

            foreach (var app in list)
            {
                result = app.GameDetailsList.FirstOrDefault(x => x.TrackId.Equals(trackId));
                if (result != null) break;
            }

            return Task.FromResult(result);
        }
    }
}
