using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handler
{
    public class TopAppsQueryHandler : IRequestHandler<GetTopGamesQuery, List<GameDetailsDto>>
                                      ,IRequestHandler<GetGameWithTrackIdQuery, GameDetailsDto>
    {

        private readonly ITopListDetailsQueryMongoRepository _topListDetailsCommandRepository;
        private readonly IMapper _mapper;

        public TopAppsQueryHandler(ITopListDetailsQueryMongoRepository topListDetailsCommandRepository, IMapper mapper)
        {
            _topListDetailsCommandRepository = topListDetailsCommandRepository;
            _mapper = mapper;
        }

        public async Task<List<GameDetailsDto>> Handle(GetTopGamesQuery request, CancellationToken cancellationToken)
        {
            var topGames = await _topListDetailsCommandRepository.GetTopGames();

            return  _mapper.Map<List<GameDetailsDto>>(topGames.GameDetailsList);
        }

        public async Task<GameDetailsDto> Handle(GetGameWithTrackIdQuery request, CancellationToken cancellationToken)
        {
           
            var topGames = await _topListDetailsCommandRepository.GetGameDetailsWithTrackId(request.TrackId);

            return _mapper.Map<GameDetailsDto>(topGames);
        }
    }
}
