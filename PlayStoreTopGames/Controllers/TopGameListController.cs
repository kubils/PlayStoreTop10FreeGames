using System;
using Application.BackgroundService;
using Application.Queries;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PlayStoreTopGames.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TopGameListController : ControllerBase
    {
        private readonly ITopGameService _topGameService;
        private readonly IMediator _mediator;
        
        public TopGameListController(ITopGameService topGameService, IMediator mediator)
        {
            _topGameService = topGameService;
            _mediator = mediator;
        }
        
        [ObsoleteAttribute("This property is obsolete. Using for just send to db manually.")]
        [HttpPost("SendToDbManually")]
        public async Task<IActionResult> PostTopTenFreeGamesManually()
        {
            var result = await _topGameService.AddTopTenFreeGameToDb();

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("GetTopGamesWithDetails")]
        public async Task<IActionResult> GetTopTenFreeGames()
        {
            var query = new GetTopGamesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //Get Game By TrackId, Its In Top Ten List Or Before In Top Ten Lists
        [HttpGet("GetGameIsInTopListsByTrackId/{trackId}")]
        public async Task<IActionResult> GetFreeGameByTrackId(string trackId)
        {
            var query = new GetGameWithTrackIdQuery(trackId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
