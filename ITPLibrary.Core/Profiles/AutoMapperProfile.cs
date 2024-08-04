using AutoMapper;
using ITPLibrary.Core.Dtos.BookDtos;
using ITPLibrary.Data.Entities;

namespace ITPLibrary.Core.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
        }
    }
}
