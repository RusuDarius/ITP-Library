using AutoMapper;
using ITPLibrary.Core.Dtos.OrderDtos;
using ITPLibrary.Core.Services.IServices;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Enums;
using ITPLibrary.Data.Repositories;
using ITPLibrary.Data.Repositories.IRepositories;

namespace ITPLibrary.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(
            IShoppingCartRepository shoppingCartRepository,
            IOrderRepository orderRepository,
            OrderItemRepository orderItemRepository,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemRepository = (IOrderItemRepository?)orderItemRepository!;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders(int userId)
        {
            var orders = await _orderRepository.GetOrdersAsync(userId);
            if (orders == null)
            {
                return null;
            }

            List<OrderDto> mappedOrders = _mapper.Map<List<OrderDto>>(orders);
            return mappedOrders;
        }

        public async Task<bool> PostOrder(PostOrderDto newOrder, int userId)
        {
            if (newOrder == null)
            {
                return false;
            }

            var productList = await _shoppingCartRepository.GetUserShoppingcartItemsAsync(userId);
            if (productList == null)
            {
                return false;
            }

            int totalPrice = CalculateOrderPrice(productList);
            var mappedOrder = OrderMapper(newOrder, userId, totalPrice);
            await _orderRepository.PostOrderAsync(mappedOrder);
            if (mappedOrder.Id == 0)
            {
                return false;
            }

            await _orderItemRepository.PostOrderItem(
                OrderItemMapper((ShoppingCart)productList, mappedOrder.Id)
            );
            await _shoppingCartRepository.EmptyShoppingCartAsync(userId);
            return true;
        }

        public async Task<bool> UpdateOrder(UpdateOrderDto updatedOrder) { }

        #region

        private static int CalculateOrderPrice(IEnumerable<ShoppingCart> productList)
        {
            int totalPrice = 0;
            foreach (var product in productList)
            {
                totalPrice += product.Quantity * product.Book.Price;
            }

            return totalPrice;
        }

        private Order OrderMapper(PostOrderDto order, int userId, int totalPrice)
        {
            var mappedNewOrder = _mapper.Map<Order>(order);
            mappedNewOrder.UserId = userId;
            mappedNewOrder.OrderStatusId = (int)OrderStatusEnum.New;
            mappedNewOrder.TotalPrice = totalPrice;

            return mappedNewOrder;
        }

        private OrderItem OrderItemMapper(ShoppingCart item, int orderId)
        {
            var orderItem = _mapper.Map<OrderItem>(item);
            orderItem.OrderId = orderId;
            return orderItem;
        }

        #endregion
    }
}
