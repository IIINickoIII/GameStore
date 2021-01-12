using GameStore.Dal.Contexts;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;

namespace GameStore.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreContext _context;

        private IBaseRepository<Comment> _commentRepository;

        private IBaseRepository<GameGenre> _gameGenreRepository;

        private IBaseRepository<GamePlatformType> _gamePlatformTypeRepository;

        private IBaseRepository<Game> _gameRepository;

        private IBaseRepository<Genre> _genreRepository;

        private IBaseRepository<PlatformType> _platformTypeRepository;

        private IBaseRepository<Publisher> _publisherRepository;

        private IBaseRepository<OrderItem> _orderItemRepository;

        private IBaseRepository<Order> _orderRepository;

        public UnitOfWork(GameStoreContext context)
        {
            _context = context;
        }

        public IBaseRepository<Comment> CommentRepository =>
            _commentRepository ??= new BaseRepository<Comment>(_context);

        public IBaseRepository<Game> GameRepository => _gameRepository ??= new BaseRepository<Game>(_context);

        public IBaseRepository<Genre> GenreRepository => _genreRepository ??= new BaseRepository<Genre>(_context);

        public IBaseRepository<PlatformType> PlatformTypeRepository =>
            _platformTypeRepository ??= new BaseRepository<PlatformType>(_context);

        public IBaseRepository<GameGenre> GameGenreRepository =>
            _gameGenreRepository ??= new BaseRepository<GameGenre>(_context);

        public IBaseRepository<GamePlatformType> GamePlatformTypeRepository =>
            _gamePlatformTypeRepository ??= new BaseRepository<GamePlatformType>(_context);

        public IBaseRepository<Publisher> PublisherRepository =>
            _publisherRepository ??= new BaseRepository<Publisher>(_context);

        public IBaseRepository<OrderItem> OrderItemRepository =>
            _orderItemRepository ??= new BaseRepository<OrderItem>(_context);

        public IBaseRepository<Order> OrderRepository =>
            _orderRepository ??= new BaseRepository<Order>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}