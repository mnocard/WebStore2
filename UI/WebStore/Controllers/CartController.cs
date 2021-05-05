﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _CartServices;

        public CartController(ICartServices CartServices) => _CartServices = CartServices;

        public IActionResult Index() => View(new CartOrderViewModel { Cart = _CartServices.GetViewModel() });

        public IActionResult Add(int id)
        {
            _CartServices.Add(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            _CartServices.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int id)
        {
            _CartServices.Decrement(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _CartServices.Clear();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel OrderModel, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _CartServices.GetViewModel(),
                    Order = OrderModel
                });

            //var order = await OrderService.CreateOrder(
            //    User.Identity!.Name,
            //    _CartServices.GetViewModel(),
            //    OrderModel
            //    );

            var order = await OrderService.CreateOrder(User.Identity!.Name, new CreateOrderModel
            {
                OrderViewModel = OrderModel,
                Items = _CartServices.GetViewModel().Items.Select(item => new OrderItemDTO
                {
                    Id = item.Product.Id,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            });

            _CartServices.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region API
        public IActionResult GetCartView() => ViewComponent("Cart");
        public IActionResult AddAPI(int id)
        {
            _CartServices.Add(id);
            return Json(new { id, message = $"Товар с id {id} был добавлен в корзину." });
        }

        public IActionResult RemoveAPI(int id)
        {
            _CartServices.Remove(id);
            return Ok(new { id, message = $"Товар с id {id} был удален из корзины." });
        }

        public IActionResult DecrementAPI(int id)
        {
            _CartServices.Decrement(id);
            return Ok();
        }

        public IActionResult ClearAPI()
        {
            _CartServices.Clear();
            return Ok();
        }

        #endregion
    }
}
