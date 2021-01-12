using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Bll.Services
{
    public class GameService : IGameService
    {
        private readonly Func<IQueryable<Game>, IIncludableQueryable<Game, object>> _includes;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _includes = games => games
                .Include(game => game.Genres)
                .ThenInclude(link => link.Genre)
                .Include(game => game.PlatformTypes)
                .ThenInclude(link => link.PlatformType)
                .Include(game => game.Publisher);
        }

        public void AddGame(GameCreate gameDto)
        {
            CheckIfGameCreateArgumentIsValid(gameDto);
            CheckIfNewGameKeyIsAvailable(gameDto.Key);
            CheckIfGenresExist(gameDto.GenreIds);
            CheckIfPlatformTypesExist(gameDto.PlatformTypeIds);

            var game = _mapper.Map<Game>(gameDto);
            _uow.GameRepository.Add(game);
            _uow.Save();

            var gameGenres = gameDto.GenreIds.Select(genreId => new GameGenre
            {
                GameId = game.Id,
                GenreId = genreId
            });
            var gamePlatformTypes = gameDto.PlatformTypeIds.Select(platformTypeId => new GamePlatformType
            {
                GameId = game.Id,
                PlatformTypeId = platformTypeId
            });
            _uow.GameGenreRepository.AddRange(gameGenres);
            _uow.GamePlatformTypeRepository.AddRange(gamePlatformTypes);
            _uow.Save();
        }

        public void DeleteGame(int gameId)
        {
            if (!_uow.GameRepository.Any(g => g.Id == gameId))
            {
                throw new InvalidOperationException($"No Game with Id = {gameId} in the Database");
            }

            var gameInDb = _uow.GameRepository.Single(g => g.Id == gameId);
            _uow.GameRepository.SoftDelete(gameInDb);
            _uow.Save();
        }

        public void EditGame(GameCreate gameDto, int gameId)
        {
            CheckIfGameCreateArgumentIsValid(gameDto);
            CheckIfNewGameKeyIsAvailable(gameDto.Key, gameId);
            CheckIfGenresExist(gameDto.GenreIds);
            CheckIfPlatformTypesExist(gameDto.PlatformTypeIds);
            UpdateGameGenres(gameId, gameDto.GenreIds);
            UpdateGamePlatformTypes(gameId, gameDto.PlatformTypeIds);

            var game = _mapper.Map<Game>(gameDto);
            game.Id = gameId;
            _uow.GameRepository.Update(game);
            _uow.Save();
        }

        public GameDto GetGameById(int gameId)
        {
            if (!_uow.GameRepository.Any(g => g.Id == gameId))
            {
                throw new InvalidOperationException($"No game with Id = {gameId} in the Database");
            }

            var gameInDb = _uow.GameRepository.Single(g => g.Id == gameId, _includes);
            var gameDto = _mapper.Map<GameDto>(gameInDb);
            return gameDto;
        }

        public IEnumerable<GameDto> GetAllGames()
        {
            var gamesInDb = _uow.GameRepository.GetAll(_includes);

            if (!gamesInDb.Any())
            {
                throw new ArgumentException("No Games found in the Database");
            }

            var games = _mapper.Map<IEnumerable<GameDto>>(gamesInDb);
            return games;
        }

        public IEnumerable<GameDto> GetGamesByGenre(int genreId)
        {
            var gameIds = _uow.GameGenreRepository
                .Find(g => g.GenreId == genreId)
                .Select(g => g.GameId)
                .ToList();

            if (!gameIds.Any())
            {
                throw new ArgumentException($"No Games with GenreId = {genreId} in the Database");
            }

            var gamesInDb = _uow.GameRepository
                .Find(g => gameIds.Contains(g.Id), _includes);
            var games = _mapper.Map<IEnumerable<GameDto>>(gamesInDb);
            return games;
        }

        public GameDto GetGameByKey(string gameKey)
        {
            if (!_uow.GameRepository.Any(g => g.Key == gameKey))
            {
                throw new InvalidOperationException($"No Game with Key = {gameKey} in the Database");
            }

            var gameInDb = _uow.GameRepository.Single(g => g.Key == gameKey, _includes);
            var game = _mapper.Map<GameDto>(gameInDb);
            return game;
        }

        public IEnumerable<GameDto> GetGamesByPlatformTypes(List<int> platformTypeIds)
        {
            var gameIds = _uow.GamePlatformTypeRepository
                .Find(g => platformTypeIds.Contains(g.PlatformTypeId))
                .Select(g => g.GameId)
                .ToList();

            if (!gameIds.Any())
            {
                throw new ArgumentException("No Games with required Platform Types found in the Database");
            }

            var gamesInDb = _uow.GameRepository
                .Find(g => gameIds.Contains(g.Id), _includes);
            var games = _mapper.Map<List<GameDto>>(gamesInDb);
            return games;
        }

        public bool NewGameKeyIsAvailable(string newGameKey, int gameId = 0)
        {
            var gameKeyIsAvailable = !_uow.GameRepository.Any(g => g.Key == newGameKey);

            if (gameId == 0)
            {
                return gameKeyIsAvailable;
            }

            if (!_uow.GameRepository.Any(g => g.Id == gameId))
            {
                throw new ArgumentException($"No game with Id = {gameId} in the Database");
            }

            var oldGame = _uow.GameRepository.Single(g => g.Id == gameId);
            return oldGame.Key.Equals(newGameKey) || gameKeyIsAvailable;
        }

        public int GetNumberOfGames()
        {
            var numberOfGames = _uow.GameRepository.Find(g => g.IsDeleted == false).Count();
            return numberOfGames;
        }

        public void CheckIfGenresExist(IEnumerable<int> genreIds)
        {
            var genreIdsInDb = _uow.GenreRepository
                .Find(x => genreIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToList();

            if (genreIds.Count() != genreIdsInDb.Count())
            {
                var notExistingGenreIds = genreIds.Except(genreIdsInDb).ToList();
                var notExistingGenreIdsString =
                    notExistingGenreIds.Aggregate(new StringBuilder(), (acc, item) => acc.Append(item + ", "));
                throw new ArgumentException($"No Genres with Id = {notExistingGenreIdsString} in the Database");
            }
        }

        public void CheckIfPlatformTypesExist(IEnumerable<int> platformTypeIds)
        {
            var platformTypeIdsInDb = _uow.PlatformTypeRepository
                .Find(x => platformTypeIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToList();

            if (platformTypeIds.Count() != platformTypeIdsInDb.Count())
            {
                var notExistingPlatformTypeIds = platformTypeIds.Except(platformTypeIdsInDb).ToList();
                var notExistingPlatformTypeIdsString =
                    notExistingPlatformTypeIds.Aggregate(new StringBuilder(), (acc, item) => acc.Append(item + ", "));
                throw new ArgumentException(
                    $"No Platform Types with Id = {notExistingPlatformTypeIdsString} in the Database");
            }
        }

        public void CheckIfGameCreateArgumentIsValid(GameCreate gameDto)
        {
            if (gameDto == null)
            {
                throw new ArgumentNullException(nameof(gameDto), $"Argument {nameof(gameDto)} is null");
            }

            if (!gameDto.PlatformTypeIds.Any())
            {
                throw new ArgumentException("PlatformTypes are required");
            }
        }

        private void CheckIfNewGameKeyIsAvailable(string newGameKey, int gameId = 0)
        {
            var gameKeyIsAvailable = !_uow.GameRepository.Any(g => g.Key == newGameKey);

            if (gameId == 0)
            {
                if (!gameKeyIsAvailable)
                {
                    throw new ArgumentException("This game Key is already taken");
                }
                return;
            }

            if (!_uow.GameRepository.Any(g => g.Id == gameId))
            {
                throw new ArgumentException($"No Game with Id = {gameId} in the Database");
            }

            var oldGameKey = _uow.GameRepository.Single(g => g.Id == gameId).Key;

            if (!oldGameKey.Equals(newGameKey) && !gameKeyIsAvailable)
            {
                throw new ArgumentException("This game Key is already taken");
            }
        }

        public void UpdateGameGenres(int gameId, IEnumerable<int> newGenreIds)
        {
            var genreInDbIds = _uow.GameGenreRepository.Find(g => g.GameId == gameId)
                .Select(g => g.GenreId);
            var genreToDeleteIds = genreInDbIds.Except(newGenreIds);
            var genresToDelete = genreToDeleteIds.Select(genreId => new GameGenre
            {
                GameId = gameId,
                GenreId = genreId
            });
            var genreToAddIds = newGenreIds.Except(genreInDbIds);
            var genresToAdd = genreToAddIds.Select(genreId => new GameGenre
            {
                GameId = gameId,
                GenreId = genreId
            });
            _uow.GameGenreRepository.HardDeleteRange(genresToDelete);
            _uow.GameGenreRepository.AddRange(genresToAdd);
            _uow.Save();
        }

        public void UpdateGamePlatformTypes(int gameId, IEnumerable<int> newPlatformTypeIds)
        {
            var platformTypeInDbIds = _uow.GamePlatformTypeRepository.Find(g => g.GameId == gameId)
                .Select(g => g.PlatformTypeId);
            var platformTypeToDeleteIds = platformTypeInDbIds.Except(newPlatformTypeIds);
            var platformTypesToDelete = platformTypeToDeleteIds.Select(platformTypeId => new GamePlatformType
            {
                GameId = gameId,
                PlatformTypeId = platformTypeId
            });
            var platformTypeToAddIds = newPlatformTypeIds.Except(platformTypeInDbIds);
            var platformTypesToAdd = platformTypeToAddIds.Select(platformTypeId => new GamePlatformType
            {
                GameId = gameId,
                PlatformTypeId = platformTypeId
            });
            _uow.GamePlatformTypeRepository.HardDeleteRange(platformTypesToDelete);
            _uow.GamePlatformTypeRepository.AddRange(platformTypesToAdd);
            _uow.Save();
        }

        public string GetGameKeyByCommentId(int commentId)
        {
            if(!_uow.CommentRepository.Any(x => x.Id == commentId))
            {
                throw new ArgumentException($"No comment with Id = {commentId} in the Database");
            }

            var gameId = _uow.CommentRepository.Single(x => x.Id == commentId).GameId;

            if(!_uow.GameRepository.Any(x => x.Id == gameId))
            {
                throw new ArgumentException($"No game with Id = {gameId} in the Database");
            }

            return _uow.GameRepository.Single(x => x.Id == gameId).Key;
        }
    }
}