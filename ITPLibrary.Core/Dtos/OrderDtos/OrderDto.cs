using ITPLibrary.Data.Enums;

namespace ITPLibrary.Core.Dtos.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int NumberOfItems { get; set; }
        public int TotalPrice { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
