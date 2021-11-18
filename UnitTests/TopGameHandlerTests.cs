using System;
using System.Collections.Generic;
using System.Threading;
using Application.DTOs;
using Application.Handler;
using Application.Queries;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.IRepositories;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace UnitTests
{
    public class TopGameHandlerTests : IClassFixture<CommonClassFixture>
    {
        private readonly Mock<ITopListDetailsQueryMongoRepository> _topListDetailsCommandRepository;
        private readonly IMapper _mapper;

        public TopGameHandlerTests(CommonClassFixture classFixture)
        {
            _topListDetailsCommandRepository = new Mock<ITopListDetailsQueryMongoRepository>();
            _mapper = classFixture.Mapper;;
        }

        [Fact]
        public async void TopGamesList_GetAll_Test()
        {
            var games = GetAllGameDetails();
            _topListDetailsCommandRepository.Setup(t => t.GetTopGames()).ReturnsAsync(games);

            var request = new GetTopGamesQuery();
            var handler = new TopAppsQueryHandler(_topListDetailsCommandRepository.Object, _mapper);

            var result = await handler.Handle(request, CancellationToken.None);
            
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(games.GameDetailsList.Count, result.Count);
        }

        [Fact]
        public async void Get_Game_With_TrackId_Query_Exist_Top_List()
        {
            var game = GetAllGameDetails().GameDetailsList[0];
            _topListDetailsCommandRepository.Setup(t => t.GetGameDetailsWithTrackId(It.IsAny<string>())).ReturnsAsync(game);

            var request = new GetGameWithTrackIdQuery(game.TrackId);
            var handler = new TopAppsQueryHandler(_topListDetailsCommandRepository.Object, _mapper);

            var result = await handler.Handle(request, CancellationToken.None);
            
            Assert.NotNull(result);
            Assert.Equal(game.TrackId, result.TrackId);
        } 

        private GameListDetails GetAllGameDetails()
        {
            GameListDetails gameListDetails = new();
            
            List<GameDetails> gameDetailsList = new();

            for (int i = 1; i < 6; i++)
            {
                GameDetails gameDetails = new();
                GameMutableDetails gameMutableDetails = new();
                
                gameMutableDetails.Description = $"Description: {i}";
                gameMutableDetails.Size = $"Size:{i}";
                gameMutableDetails.CurrentVersion = $"{i}";
                gameMutableDetails.TotalReviewCount = $"{i}";
                gameMutableDetails.TotalInstallCount = $"{i}";
                gameMutableDetails.LastUpdateDate = $"{i}";

                gameDetails.TrackId = $"{i}.Game";
                gameDetails.Author = $"Author: {i}";
                gameDetails.Title = $"Title: {i}";
                gameDetails.GameMutableDetails = gameMutableDetails;
                gameDetailsList.Add(gameDetails);
            }

            gameListDetails.Id = new ObjectId();
            gameListDetails.CreateDate = DateTime.Now;
            gameListDetails.GameDetailsList = gameDetailsList;
            return gameListDetails;
        }
    }
}