using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Mapper;
using GameStore.Bll.Services;
using GameStore.Bll.Tests.Fake;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Dal.Repositories;
using Xunit;

namespace GameStore.Bll.Tests.Services
{
    public class GameServiceTests
    {
        public GameServiceTests()
        {
            _gamesInDb = new List<Game>
            {
                new Game
                {
                    Id = 1,
                    Name = "Grand Theft Auto 5",
                    Description = "Description",
                    Key = "GTA V"
                },
                new Game
                {
                    Id = 2,
                    Name = "WorldOfTanks",
                    Description = "Description",
                    Key = "WOT"
                },
                new Game
                {
                    Id = 3,
                    Name = "Grand Theft Auto San Andreas",
                    Description = "Description",
                    Key = "GTA San Andreas"
                }
            };
            _genresInDb = new List<Genre>
            {
                new Genre
                {
                    Id = 1,
                    Name = "Rally"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Sport"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Simulators"
                }
            };
            _commentsInDb = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    Body = "Nice Game.",
                    GameId = 3,
                    Name = "Adam",
                    Time = new DateTime()
                },
                new Comment
                {
                    Id = 2,
                    Body = "Wonderful!",
                    GameId = 2,
                    Name = "Bob",
                    Time = new DateTime()
                }
            };
            _gameGenresInDb = new List<GameGenre>
            {
                new GameGenre
                {
                    GameId = 1,
                    GenreId = 1
                },
                new GameGenre
                {
                    GameId = 2,
                    GenreId = 1
                },
                new GameGenre
                {
                    GameId = 3,
                    GenreId = 2
                }
            };
            _platformTypesInDb = new List<PlatformType>
            {
                new PlatformType
                {
                    Id = 1,
                    Type = "PC"
                },
                new PlatformType
                {
                    Id = 2,
                    Type = "PS3"
                },
                new PlatformType
                {
                    Id = 3,
                    Type = "Xbox One"
                }
            };
            _gamePlatformTypesInDb = new List<GamePlatformType>
            {
                new GamePlatformType
                {
                    GameId = 1,
                    PlatformTypeId = 1
                },
                new GamePlatformType
                {
                    GameId = 2,
                    PlatformTypeId = 2
                },
                new GamePlatformType
                {
                    GameId = 3,
                    PlatformTypeId = 3
                }
            };
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.Setup(x => x.GameRepository).Returns(new BaseRepositoryFake<Game>());
            _uowMock.Setup(x => x.GenreRepository).Returns(new BaseRepositoryFake<Genre>());
            _uowMock.Setup(x => x.CommentRepository).Returns(new BaseRepositoryFake<Comment>());
            _uowMock.Setup(x => x.GameGenreRepository).Returns(new BaseRepositoryFake<GameGenre>());
            _uowMock.Setup(x => x.PlatformTypeRepository).Returns(new BaseRepositoryFake<PlatformType>());
            _uowMock.Setup(x => x.GamePlatformTypeRepository).Returns(new BaseRepositoryFake<GamePlatformType>());
            _mapper = new MapperConfiguration(mc => { mc.AddProfile<MapperProfileBll>(); }).CreateMapper();
        }

        private readonly List<Game> _gamesInDb;
        private readonly List<Genre> _genresInDb;
        private readonly List<Comment> _commentsInDb;
        private readonly List<GameGenre> _gameGenresInDb;
        private readonly List<PlatformType> _platformTypesInDb;
        private readonly List<GamePlatformType> _gamePlatformTypesInDb;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly IMapper _mapper;

        [Fact]
        public void GetGameKeyByCommentId_CommentIdIsValid_ReturnsGameKey()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.CommentRepository.AddRange(_commentsInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var commentId = 1;
            var expectedGameKey = "GTA San Andreas";

            //Act
            var result = service.GetGameKeyByCommentId(commentId);

            //Assert
            Assert.Equal(expectedGameKey, result);
        }

        [Fact]
        public void AddGame_ArgumentFitsAllRequirements_GameAddedToDb()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var uniqueKey = "Unique Key";
            var gameToAdd = new GameCreate
            {
                Name = "New Game",
                Description = "Description",
                Key = uniqueKey,
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 }
            };

            //Act
            service.AddGame(gameToAdd);

            //Assert
            var gameInDb = service.GetGameByKey(uniqueKey);
            Assert.Equal(gameToAdd.Name, gameInDb.Name);
            Assert.Equal(gameToAdd.Description, gameInDb.Description);
        }

        [Fact]
        public void AddGame_GameCreateArgumentIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var service = new GameService(_uowMock.Object, _mapper);
            GameCreate gameToAdd = null;

            //Act
            Action addGame = () => service.AddGame(gameToAdd);

            //Assert
            Assert.Throws<ArgumentNullException>(addGame);
        }

        [Fact]
        public void AddGame_GameKeyIsAlreadyTaken_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToAdd = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 }
            };

            //Act
            Action addGame = () => service.AddGame(gameToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addGame);
        }

        [Fact]
        public void AddGame_GamePlatformTypesAreNotSet_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToAdd = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int>()
            };

            //Act
            Action addGame = () => service.AddGame(gameToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addGame);
        }

        [Fact]
        public void AddGame_GenresDoNotExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToAdd = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 100500, 100501, 100502 },
                PlatformTypeIds = new List<int> { 1 }
            };

            //Act
            Action addGame = () => service.AddGame(gameToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addGame);
        }

        [Fact]
        public void AddGame_PlatformTypesDoNotExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToAdd = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 100500, 100501, 100502 }
            };

            //Act
            Action addGame = () => service.AddGame(gameToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addGame);
        }

        [Fact]
        public void DeleteGame_GameWithThisIdDoesNotExistInDb_ThrowsInvalidOperationException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToDeleteId = 100500;

            //Act
            Action deleteGame = () => service.DeleteGame(gameToDeleteId);

            //Assert
            Assert.Throws<InvalidOperationException>(deleteGame);
        }

        [Fact]
        public void DeleteGame_GameWithThisIdExistsInDb_DeleteThisGame()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToDeleteId = 1;

            //Act
            service.DeleteGame(gameToDeleteId);

            //Assert
            var gameAfterDelete = service.GetAllGames().Single(x => x.Id == gameToDeleteId);
            Assert.True(gameAfterDelete.IsDeleted);
        }

        [Fact]
        public void EditGame_ArgumentFitsAllRequirements_GameAddedToDb()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var uniqueKey = "Unique Key";
            var gameToEditId = 1;
            var gameToEdit = new GameCreate
            {
                Name = "New Game",
                Description = "Description",
                Key = uniqueKey,
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 }
            };

            //Act
            service.EditGame(gameToEdit, gameToEditId);

            //Assert
            var gameInDb = service.GetGameByKey(uniqueKey);
            Assert.Equal(gameToEdit.Name, gameInDb.Name);
            Assert.Equal(gameToEdit.Description, gameInDb.Description);
        }

        [Fact]
        public void EditGame_GameCreateArgumentIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToEditId = 1;
            GameCreate gameToEdit = null;

            //Act
            Action editGame = () => service.EditGame(gameToEdit, gameToEditId);

            //Assert
            Assert.Throws<ArgumentNullException>(editGame);
        }

        [Fact]
        public void EditGame_GameIdDoesNotExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToEditId = 100500;
            var gameToEdit = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 }
            };

            //Act
            Action editGame = () => service.EditGame(gameToEdit, gameToEditId);

            //Assert
            Assert.Throws<ArgumentException>(editGame);
        }

        [Fact]
        public void EditGame_GameKeyIsAlreadyTaken_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToEditId = 1;
            var gameToEdit = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "WOT",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 }
            };

            //Act
            Action editGame = () => service.EditGame(gameToEdit, gameToEditId);

            //Assert
            Assert.Throws<ArgumentException>(editGame);
        }

        [Fact]
        public void EditGame_GamePlatformTypesAreNotSet_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToEditId = 1;
            var gameToedit = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int>()
            };

            //Act
            Action editGame = () => service.EditGame(gameToedit, gameToEditId);

            //Assert
            Assert.Throws<ArgumentException>(editGame);
        }

        [Fact]
        public void EditGame_GenresDoNotExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToEditId = 1;
            var gameToEdit = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 100500, 100501, 100502 },
                PlatformTypeIds = new List<int> { 1 }
            };

            //Act
            Action editGame = () => service.EditGame(gameToEdit, gameToEditId);

            //Assert
            Assert.Throws<ArgumentException>(editGame);
        }

        [Fact]
        public void EditGame_PlatformTypesDoNotExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameToEditId = 1;
            var gameToEdit = new GameCreate
            {
                Name = "Grand Theft Auto V",
                Description = "Description",
                Key = "GTA V",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 100500, 100501, 100502 }
            };

            //Act
            Action editGame = () => service.EditGame(gameToEdit, gameToEditId);

            //Assert
            Assert.Throws<ArgumentException>(editGame);
        }

        [Fact]
        public void GetAllGames_WithGamesInTheDatabase_ReturnsGamesIEnumerable()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);

            //Act
            var allGames = service.GetAllGames().ToList();

            //Assert
            Assert.Equal(_gamesInDb.Count, allGames.Count);
            Assert.Equal(_gamesInDb.Select(x => x.Id), allGames.Select(x => x.Id));
        }

        [Fact]
        public void GetAllGames_WithoutGamesInTheDatabase_ThrowsArgumentException()
        {
            //Arrange
            var service = new GameService(_uowMock.Object, _mapper);

            //Act
            Func<IEnumerable<GameDto>> getAllGames = () => service.GetAllGames();

            //Assert
            Assert.Throws<ArgumentException>(getAllGames);
        }

        [Fact]
        public void GetGameByKey_GameWithThisKeyDoesNotExistsInDb_ThrowsInvalidOperationException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);

            //Act
            var gameKey = "Mario";
            Func<GameDto> getGameByKey = () => service.GetGameByKey(gameKey);

            //Assert
            Assert.Throws<InvalidOperationException>(getGameByKey);
        }

        [Fact]
        public void GetGameByKey_GameWithThisKeyExistsInDb_ReturnsGame()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var gameKey = "GTA V";

            //Act
            var gameByKey = service.GetGameByKey(gameKey);

            //Assert
            Assert.Equal(gameKey, gameByKey.Key);
        }

        [Fact]
        public void GetGamesByGenre_WithGamesRelatedToThisGenreInDb_ReturnsGames()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GameGenreRepository.AddRange(_gameGenresInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var genreId = 1;

            //Act
            var gamesByGenre = service.GetGamesByGenre(genreId);

            //Assert
            var gamesWithThisGenreInDb = _gameGenresInDb
                .Where(x => x.GenreId == genreId)
                .Select(x => x.GameId)
                .ToList();
            var returnedGemeByGenreIds = gamesByGenre.Select(x => x.Id).ToList();
            Assert.Equal(gamesWithThisGenreInDb, returnedGemeByGenreIds);
        }

        [Fact]
        public void GetGamesByGenre_WithoutGamesRelatedToThisGenreInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GameGenreRepository.AddRange(_gameGenresInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var genreId = 100500;

            //Act
            Func<IEnumerable<GameDto>> getGamesByGenre = () => service.GetGamesByGenre(genreId);

            //Assert
            Assert.Throws<ArgumentException>(getGamesByGenre);
        }

        [Fact]
        public void GetGamesByPlatformTypes_GameWithTheseKeysDoNotExistInDb_ArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GamePlatformTypeRepository.AddRange(_gamePlatformTypesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var platformTypeIds = new List<int> { 5, 6, 7 };

            //Act
            Func<IEnumerable<GameDto>> gamesByPlatformTypes = () => service.GetGamesByPlatformTypes(platformTypeIds);

            //Assert
            Assert.Throws<ArgumentException>(gamesByPlatformTypes);
        }

        [Fact]
        public void GetGamesByPlatformTypes_GameWithTheseKeysExistInDb_ReturnsGames()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.GamePlatformTypeRepository.AddRange(_gamePlatformTypesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var platformTypeIds = new List<int> { 1, 2, 3 };

            //Act
            var games = service.GetGamesByPlatformTypes(platformTypeIds);

            //Assert
            var gameInDbIds = _gamesInDb
                .Where(x => platformTypeIds.Contains(x.Id))
                .Select(x => x.Id);
            var returnedGameIds = games.Select(x => x.Id);
            Assert.Equal(gameInDbIds, returnedGameIds);
        }

        [Fact]
        public void GetNumberOfGames()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            var service = new GameService(_uowMock.Object, _mapper);
            var numberOfGamesInDb = _gamesInDb.Count;

            //Assert
            var numberOfGames = service.GetNumberOfGames();

            //Assert
            Assert.Equal(numberOfGamesInDb, numberOfGames);
        }
    }
}