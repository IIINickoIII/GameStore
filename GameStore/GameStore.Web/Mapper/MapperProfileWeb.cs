using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Mapper
{
    [ExcludeFromCodeCoverage]
    public class MapperProfileWeb : Profile
    {
        public MapperProfileWeb()
        {
            CreateMap<CommentCreate, CommentCreateViewModel>().ReverseMap();
            CreateMap<PlatformTypeDto, PlatformTypeViewModel>().ReverseMap();
            CreateMap<PublisherDto, PublisherViewModel>().ReverseMap();
            CreateMap<GameCreate, GameViewModel>().ReverseMap();
            CreateMap<GenreDto, GenreViewModel>().ReverseMap();
            CreateMap<GameDto, GameViewModel>().ReverseMap();
            CreateMap<OrderItemDto, OrderItemViewModel>().ReverseMap();
        }
    }
}
