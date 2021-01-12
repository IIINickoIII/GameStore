using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Dal.Entities;

namespace GameStore.Bll.Mapper
{
    [ExcludeFromCodeCoverage]
    public class MapperProfileBll : Profile
    {
        public MapperProfileBll()
        {
            CreateMap<Game, GameDto>()
                .ForMember(g => g.Genres, opt => opt.MapFrom(x => x.Genres.Select(y => y.Genre)))
                .ForMember(g => g.PlatformTypes, opt => opt.MapFrom(x => x.PlatformTypes.Select(y => y.PlatformType)))
                .ReverseMap();

            CreateMap<GameCreate, Game>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CommentCreate, Comment>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<PlatformType, PlatformTypeDto>().ReverseMap();
            CreateMap<Publisher, PublisherDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}