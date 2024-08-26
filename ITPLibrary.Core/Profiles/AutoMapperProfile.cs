using AutoMapper;
using ITPLibrary.Core.Dtos.BookDtos;
using ITPLibrary.Core.Dtos.UserDtos;
using ITPLibrary.Data.Entities;

namespace ITPLibrary.Core.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, PromotedBookDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
