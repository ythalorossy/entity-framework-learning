using Application.DTOs;
using AutoMapper;
using Domain.Blogs;
using Domain.Posts;

namespace Application.Mappings;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        CreateMap<Blog, BlogDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ReverseMap();

        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId.Value))
            .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.BlogId.Value))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.PublishedOn))
            .ReverseMap();
    }
}