using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemService _orderItemService;
        private readonly IGameService _gameService;

        public OrderItemsController(IMapper mapper, IOrderItemService orderItemService, IGameService gameService)
        {
            _mapper = mapper;
            _orderItemService = orderItemService;
            _gameService = gameService;
        }

        [Route("/Game/{gameId}/AddToBasket")]
        public IActionResult CreateOrderItem(int gameId)
        {
            var orderItemViewModel = new OrderItemViewModel()
            {
                GameId = gameId,
                Game = _gameService.GetGameById(gameId) ?? new GameDto()
            };
            return View("OrderItemForm", orderItemViewModel);
        }

        [HttpPost]
        public IActionResult Save([FromForm] OrderItemViewModel orderItemViewModel)
        {
            var enoughOrderItems =
                _orderItemService.CheckIfEnoughItemsInStock(orderItemViewModel.GameId, orderItemViewModel.Quantity);
            if (!(enoughOrderItems.Error is null))
            {
                ModelState.AddModelError(nameof(orderItemViewModel.Quantity), enoughOrderItems.Error);
            }

            if (!ModelState.IsValid)
            {
                orderItemViewModel.Game = _gameService.GetGameById(orderItemViewModel.GameId);
                return View("OrderItemForm", orderItemViewModel);
            }

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItemViewModel);
            _orderItemService.AddOrderItem(orderItemDto);
            return RedirectToAction("ShowMyBasket", "Orders");
        }

        [Route("/OrderItems/{orderItemId}/delete")]
        public IActionResult DeleteOrderItem(int orderItemId)
        {
            _orderItemService.SoftDelete(orderItemId);
            return RedirectToAction("ShowMyBasket", "Orders");
        }
    }
}
