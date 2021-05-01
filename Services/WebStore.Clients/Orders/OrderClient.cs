using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrderClient : BaseClient, IOrderService
    {
        public OrderClient(IConfiguration Configuration) : base(Configuration, WebApi.Orders)
        {

        }
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => 
            await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}");

        public async Task<OrderDTO> GetOrderById(int id) =>
            await GetAsync<OrderDTO>($"{Address}/{id}");

        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var resopnse = await PostAsync($"{Address}/{UserName}", OrderModel);
            var result = await resopnse.Content.ReadAsAsync<OrderDTO>();
            return result;
        }
    }
}
