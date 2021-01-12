using GameStore.Bll.Dto;

namespace GameStore.Bll.Interfaces
{
    public interface IOrderItemService
    {
        void AddOrderItem(OrderItemDto orderItemDto);

        OrderItemStockAvailabilityResult CheckIfEnoughItemsInStock(int gameId, int quantityToBuy);

        void SoftDelete(int orderItemId);
    }
}
