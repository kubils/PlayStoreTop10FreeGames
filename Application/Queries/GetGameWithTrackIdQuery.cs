using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public  class GetGameWithTrackIdQuery : IRequest<GameDetailsDto>
    {
        public GetGameWithTrackIdQuery(string trackId)
        {
            TrackId = trackId;
        }

        public string TrackId { get; set; }
    }
}
