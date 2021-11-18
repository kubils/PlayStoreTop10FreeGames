using Application.DTOs;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameDetails, GameDetailsDto>()
                .ForMember(dest => dest.Description, 
                    opt => opt.MapFrom(src => src.GameMutableDetails.Description))
                .ForMember(dest => dest.TotalReviewCount,
                    opt => opt.MapFrom(src => src.GameMutableDetails.TotalReviewCount))
                .ForMember(dest => dest.TotalInstallCount,
                    opt => opt.MapFrom(src => src.GameMutableDetails.TotalInstallCount))
                .ForMember(dest => dest.CurrentVersion,
                    opt => opt.MapFrom(src => src.GameMutableDetails.CurrentVersion))
                .ForMember(dest => dest.LastUpdateDate,
                    opt => opt.MapFrom(src => src.GameMutableDetails.LastUpdateDate))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.GameMutableDetails.Size));
                //.ForMember(dest => dest.AppDetailsCreateDate, opt => opt.MapFrom(src => src.GameMutableDetails.AppDetailsCreateDate));


            CreateMap<User, CreateUserCommand>();
            CreateMap<CreateUserCommand, User>();
            
            CreateMap<User, UserDto>();
            
        }
    }
}
