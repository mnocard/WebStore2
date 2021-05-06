using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem OrderItem) => OrderItem is null
            ? null
            : new OrderItemDTO
            {
                Id = OrderItem.Id,
                ProductId = OrderItem.Product?.Id ?? 0,
                Price = OrderItem.Price,
                Quantity = OrderItem.Quantity,
            };

        public static OrderItem FromDTO(this OrderItemDTO OrderItem) => OrderItem is null
            ? null
            : new OrderItem
            {
                Id = OrderItem.Id,
                Product = new Product {  Id = OrderItem.Id },
                Price = OrderItem.Price,
                Quantity = OrderItem.Quantity,
            };

        public static OrderDTO ToDTO(this Order Order) => Order is null
            ? null
            : new OrderDTO
            {
                Id = Order.Id,
                Name = Order.Name,
                Phone = Order.Phone,
                Address = Order.Address,
                Date = Order.Date,
                Items = Order.Items.ToDTO(),
            };

        public static Order ToDTO(this OrderDTO Order) => Order is null
            ? null
            : new Order
            {
                Id = Order.Id,
                Name = Order.Name,
                Phone = Order.Phone,
                Address = Order.Address,
                Date = Order.Date,
                Items = Order.Items.FromDTO().ToList(),
            };

        public static IEnumerable<OrderItemDTO> ToDTO(this IEnumerable<OrderItem> OrderItems) => OrderItems.Select(ToDTO);
        public static IEnumerable<OrderItem> FromDTO(this IEnumerable<OrderItemDTO> OrderItems) => OrderItems.Select(FromDTO);
    }
}
