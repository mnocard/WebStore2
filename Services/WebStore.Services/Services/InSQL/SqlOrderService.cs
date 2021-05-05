using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly ILogger _Logger;

        public SqlOrderService(WebStoreDB db, UserManager<User> UserManager, ILogger<SqlOrderService> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _Logger = Logger;
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync())
           .Select(i => i.ToDTO());

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .FirstOrDefaultAsync(order => order.Id == id))
           .ToDTO();

        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            _Logger.LogInformation("Формирование нового заказа.");
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
            {
                _Logger.LogError("Пользователь, формирующий заказ, с именем {0} в БД отсутствует.", UserName);
                throw new InvalidOperationException($"Пользователь с именем {UserName} в БД отсутствует");
            }

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = OrderModel.OrderViewModel.Name,
                Address = OrderModel.OrderViewModel.Address,
                Phone = OrderModel.OrderViewModel.Phone,
                User = user
            };

            //var product_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

            //var cart_products = await _db.Products
            //  .Where(p => product_ids.Contains(p.Id))
            //  .ToArrayAsync();

            //order.Items = Cart.Items.Join(
            //    cart_products,
            //    cart_item => cart_item.Product.Id,
            //    product => product.Id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Price = product.Price,  // место, где могут быть применены скидки
            //        Quantity = cart_item.Quantity,
            //    }).ToArray();

            foreach (var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.ProductId);
                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product,
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            _Logger.LogInformation("Заказ для пользователя с именем {0} сформирован.", UserName);

            return order.ToDTO();
        }
    }
}
