using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>
    /// Управление заказами
    /// </summary>
    [Route(WebApi.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;
        public OrdersApiController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <param name="OrderModel">Данные о заказе</param>
        /// <returns></returns>
        [HttpPost("{UserName}")]
        public Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel) => _OrderService.CreateOrder(UserName, OrderModel);

        /// <summary>
        /// Получение заказа по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Собственно заказ</returns>
        [HttpGet("{id}")]
        public Task<OrderDTO> GetOrderById(int id) => _OrderService.GetOrderById(id);

        /// <summary>
        /// Полученеи всех заказов определенного пользователя
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>Список заказов</returns>
        [HttpGet("user/{UserName}")]
        public Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => _OrderService.GetUserOrders(UserName);
    }
}
