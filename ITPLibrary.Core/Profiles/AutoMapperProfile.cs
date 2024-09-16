using AutoMapper;
using ITPLibrary.Core.Dtos.BookDtos;
using ITPLibrary.Core.Dtos.OrderDtos;
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
            CreateMap<Book, PopularAndRecentlyAddedBooksDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, PostOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
