using AutoMapper;
using ITPLibrary.Core.Dtos.OrderDtos;
using ITPLibrary.Core.Dtos.PaymentDtos;
using ITPLibrary.Core.Services.IServices;
using ITPLibrary.Core.Utilities;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Enums;
using ITPLibrary.Data.Repositories;
using ITPLibrary.Data.Repositories.IRepositories;
using Stripe;

namespace ITPLibrary.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(
            IShoppingCartRepository shoppingCartRepository,
            IOrderRepository orderRepository,
            OrderItemRepository orderItemRepository,
            IMapper mapper,
            PaymentConfigUtility stripe
        )
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemRepository = (IOrderItemRepository?)orderItemRepository!;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersAsync(userId);
            if (orders == null)
            {
                return null;
            }

            List<OrderDto> mappedOrders = _mapper.Map<List<OrderDto>>(orders);
            return mappedOrders;
        }

        public async Task<bool> PostOrderAsync(PostOrderDto newOrder, int userId)
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

        public async Task<bool> UpdateOrderAsync(UpdateOrderDto updatedOrder)
        {
            Order unchangedOrder = await _orderRepository.GetOrderAsync(updatedOrder.Id);

            if (unchangedOrder == null)
            {
                return false;
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.New)
            {
                unchangedOrder = await UpdateNewOrder(updatedOrder, unchangedOrder);
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.Processing)
            {
                unchangedOrder = await UpdateProcessingOrder(updatedOrder, unchangedOrder);
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.Dispatched)
            {
                unchangedOrder = await UpdateDispatchedOrder(updatedOrder, unchangedOrder);
            }

            await _orderRepository.UpdateOrderAsync(unchangedOrder);
            return true;
        }

        public async Task<Charge> ProcessPayment(CreditCardDto creditCard, int userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetUserShoppingcartItemsAsync(userId);
            if (shoppingCart == null)
            {
                return null;
            }

            TokenCardOptions cardOptions = new TokenCardOptions
            {
                Name = creditCard.FirstName + " " + creditCard.LastName,
                Number = creditCard.CardNumber,
                ExpYear = creditCard.ExpirationYear.ToString(),
                ExpMonth = creditCard.ExpirationMonth.ToString(),
                Cvc = creditCard.CVV2.ToString()
            };

            TokenCreateOptions tokenOptions = new TokenCreateOptions { Card = cardOptions };
            TokenService tokenService = new TokenService();
            Token createdToken = await tokenService.CreateAsync(tokenOptions);

            Customer customer = await CreateCustomer(creditCard.Email, createdToken);

            ChargeService chargeService = new ChargeService();
            Charge charge = await chargeService.CreateAsync(
                new ChargeCreateOptions
                {
                    Amount = CalculateOrderPrice(shoppingCart),
                    Currency = "ron",
                    Source = createdToken.Id,
                    Customer = customer.Id,
                    ReceiptEmail = customer.Email
                }
            );

            return charge;
        }

        #region Private Methods

        private async Task<Customer> CreateCustomer(string customerEmail, Token token)
        {
            CustomerCreateOptions customer = new CustomerCreateOptions()
            {
                Email = customerEmail,
                Source = token.Id,
                Name = token.Card.Name
            };

            var customerService = new CustomerService();
            Customer stripeCustomer = customerService.Create(customer);

            return stripeCustomer;
        }

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

        private async Task<Order> UpdateNewOrder(UpdateOrderDto newOrder, Order originalOrder)
        {
            if (newOrder.BillingAddress != null)
            {
                await UpdateBillingAddress(originalOrder, newOrder.BillingAddress);
            }

            if (newOrder.AdditionalInformation != null)
            {
                originalOrder.AdditionalInformation = newOrder.AdditionalInformation;
            }

            if (newOrder.PaymentType != null)
            {
                originalOrder.PaymentTypeId = (int)newOrder.PaymentType;
            }

            return originalOrder;
        }

        private async Task UpdateBillingAddress(Order order, AddressDto newAddress)
        {
            order.BillingAddress.AddressName = newAddress.AddressName;
            order.BillingAddress.PhoneNumber = newAddress.PhoneNumber;
            order.BillingAddress.City = newAddress.City;

            await _orderRepository.UpdateAddressAsync(order.BillingAddress);
        }

        private async Task<Order> UpdateProcessingOrder(
            UpdateOrderDto newOrder,
            Order originalOrder
        )
        {
            if (newOrder.AdditionalInformation != null)
            {
                originalOrder.AdditionalInformation = newOrder.AdditionalInformation;
            }

            if (newOrder.PaymentType != null)
            {
                originalOrder.PaymentTypeId = (int)newOrder.PaymentType;
            }

            return originalOrder;
        }

        private async Task<Order> UpdateDispatchedOrder(
            UpdateOrderDto newOrder,
            Order originalOrder
        )
        {
            if (newOrder.AdditionalInformation != null)
            {
                originalOrder.AdditionalInformation = newOrder.AdditionalInformation;
            }

            return originalOrder;
        }

        #endregion
    }
}
