using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using System;

namespace GameStore.Bll.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderItemService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void AddOrderItem(OrderItemDto orderItemDto)
        {
            var item = _mapper.Map<OrderItem>(orderItemDto);

            if (EnoughItemsInStock(orderItemDto.GameId, orderItemDto.Quantity))
            {
                var gameInDb = _uow.GameRepository.Single(x => x.Id == orderItemDto.GameId);
                gameInDb.UnitInStock -= orderItemDto.Quantity;
                _uow.GameRepository.Update(gameInDb);
                _uow.Save();
            }

            if (_uow.OrderRepository.Any(x => x.IsClosedForEdit == false))
            {
                var openedOrder = _uow.OrderRepository.Single(x => x.IsClosedForEdit == false);

                if (_uow.OrderItemRepository.Any(x => x.IsDeleted == false &
                                                      x.OrderId == openedOrder.Id &
                                                      x.GameId == orderItemDto.GameId))
                {
                    var sameItemInOrder = _uow.OrderItemRepository.Single(x =>
                        x.IsDeleted == false &
                        x.OrderId == openedOrder.Id &
                        x.GameId == orderItemDto.GameId);
                    sameItemInOrder.Price = item.Price;
                    sameItemInOrder.Discount = item.Discount;
                    sameItemInOrder.Quantity += item.Quantity;
                    _uow.OrderItemRepository.Update(sameItemInOrder);
                }
                else
                {
                    item.OrderId = openedOrder.Id;
                    _uow.OrderItemRepository.Add(item);
                }
            }
            else
            {
                var order = new Order()
                {
                    Date = DateTime.Now,
                    CustomerId = 1
                };
                _uow.OrderRepository.Add(order);
                _uow.Save();
                item.OrderId = order.Id;
                _uow.OrderItemRepository.Add(item);
            }

            _uow.Save();
        }

        public OrderItemStockAvailabilityResult CheckIfEnoughItemsInStock(int gameId, int quantityToBuy)
        {
            var error = default(string);

            if (!_uow.GameRepository.Any(x => x.Id == gameId))
            {
                throw new ArgumentException($"Game with Id = {gameId} does not exist!");
            }

            var gameInDb = _uow.GameRepository.Single(x => x.Id == gameId);
            var itemsInStockAfterSale = gameInDb.UnitInStock - quantityToBuy;

            if (itemsInStockAfterSale < 0)
            {
                error = gameInDb.UnitInStock > 0 ? $"We have only {gameInDb.UnitInStock} items of {gameInDb.Name}" : $"All copies of {gameInDb.Name} were sold";
            }

            return new OrderItemStockAvailabilityResult()
            {
                Error = error
            };
        }

        public void SoftDelete(int orderItemId)
        {
            if (!_uow.OrderItemRepository.Any(o => o.Id == orderItemId))
            {
                throw new InvalidOperationException($"No OrderItems with Id = {orderItemId} in the Database");
            }

            var orderItemInDb = _uow.OrderItemRepository.Single(o => o.Id == orderItemId);

            if (!_uow.GameRepository.Any(x => x.Id == orderItemInDb.GameId))
            {
                throw new InvalidOperationException($"No Game with Id = {orderItemInDb.GameId} in the Database");
            }

            var gameInDb = _uow.GameRepository.Single(x => x.Id == orderItemInDb.GameId);
            gameInDb.UnitInStock += orderItemInDb.Quantity;
            _uow.GameRepository.Update(gameInDb);
            _uow.Save();
            _uow.OrderItemRepository.SoftDelete(orderItemInDb);
            _uow.Save();
        }

        private bool EnoughItemsInStock(int gameId, int wantToBuyCount)
        {
            if (!_uow.GameRepository.Any(x => x.Id == gameId))
            {
                throw new ArgumentException($"Game with Id = {gameId} does not exist!");
            }

            var itemsInStock = _uow.GameRepository.Single(x => x.Id == gameId).UnitInStock;
            var itemsInStockAfterSale = itemsInStock - wantToBuyCount;
            return itemsInStockAfterSale >= 0;
        }
    }
}