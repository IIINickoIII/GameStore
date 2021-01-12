using GameStore.Dal.Entities;

namespace GameStore.Dal.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Comment> CommentRepository { get; }

        IBaseRepository<Game> GameRepository { get; }

        IBaseRepository<Genre> GenreRepository { get; }

        IBaseRepository<PlatformType> PlatformTypeRepository { get; }

        IBaseRepository<GameGenre> GameGenreRepository { get; }

        IBaseRepository<GamePlatformType> GamePlatformTypeRepository { get; }

        IBaseRepository<Publisher> PublisherRepository { get; }

        IBaseRepository<OrderItem> OrderItemRepository { get; }

        IBaseRepository<Order> OrderRepository { get; }

        void Save();
    }
}