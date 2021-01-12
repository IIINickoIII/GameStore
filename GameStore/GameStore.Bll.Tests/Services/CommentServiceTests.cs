using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Mapper;
using GameStore.Bll.Services;
using GameStore.Bll.Tests.Fake;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Bll.Tests.Services
{
    public class CommentServiceTests
    {
        public CommentServiceTests()
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
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.Setup(x => x.GameRepository).Returns(new BaseRepositoryFake<Game>());
            _uowMock.Setup(x => x.CommentRepository).Returns(new BaseRepositoryFake<Comment>());
            _mapper = new MapperConfiguration(mc => { mc.AddProfile<MapperProfileBll>(); }).CreateMapper();
        }

        private readonly List<Game> _gamesInDb;
        private readonly List<Comment> _commentsInDb;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly IMapper _mapper;

        [Fact]
        public void AddComment_ArgumentFitsAllRequirements_CommentAddedToDb()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.CommentRepository.AddRange(_commentsInDb);
            var service = new CommentService(_uowMock.Object, _mapper);
            var gameId = 1;
            var gameKey = "GTA V";
            var commentToAdd = new CommentCreate
            {
                Body = "Soo cool!",
                GameId = gameId,
                Name = "Peter"
            };

            //Act
            service.AddCommentToGame(commentToAdd);

            //Assert
            var numberOfCommentsInDb = service.GetAllCommentsByGameKey(gameKey).Count();
            Assert.True(numberOfCommentsInDb == 1);
        }

        [Fact]
        public void AddComment_CommentCreateArgumentIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var service = new CommentService(_uowMock.Object, _mapper);
            CommentCreate commentCreate = null;

            //Act
            Action addComment = () => service.AddCommentToGame(commentCreate);

            //Assert
            Assert.Throws<ArgumentException>(addComment);
        }

        [Fact]
        public void AddComment_TryToAddCommentToNotExistingGame_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.CommentRepository.AddRange(_commentsInDb);
            var service = new CommentService(_uowMock.Object, _mapper);
            var notExistingGameId = 100500;
            var commentToAdd = new CommentCreate
            {
                Body = "Soo cool!",
                GameId = notExistingGameId,
                Name = "Peter"
            };

            //Act
            Action addComment = () => service.AddCommentToGame(commentToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addComment);
        }

        [Fact]
        public void GetAllCommentsByGameKey_GameWithThisKeyDoesNotExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.CommentRepository.AddRange(_commentsInDb);
            var service = new CommentService(_uowMock.Object, _mapper);
            var notExistGameKey = "Not exist";

            //Act
            Action getAllCommentsByGameKey = () => service.GetAllCommentsByGameKey(notExistGameKey);

            //Assert
            Assert.Throws<ArgumentException>(getAllCommentsByGameKey);
        }

        [Fact]
        public void GetAllCommentsByGameKey_GameWithThisKeyHasComments_ReturnsComments()
        {
            //Arrange
            var name = "Roman";
            var newComment = new Comment
            {
                Name = name,
                GameId = 1,
                Body = "Ha ha ha"
            };
            _uowMock.Object.GameRepository.AddRange(_gamesInDb);
            _uowMock.Object.CommentRepository.AddRange(_commentsInDb);
            _uowMock.Object.CommentRepository.Add(newComment);
            var service = new CommentService(_uowMock.Object, _mapper);
            var gameKey = "GTA V";

            //Act
            var getAllCommentsByGameKey = service.GetAllCommentsByGameKey(gameKey);

            //Assert
            var commentInDb = getAllCommentsByGameKey.Single(x => x.Name == name);
            Assert.Equal(name, commentInDb.Name);
        }
    }
}