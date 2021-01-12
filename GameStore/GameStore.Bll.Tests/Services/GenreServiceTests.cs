using System;
using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Mapper;
using GameStore.Bll.Services;
using GameStore.Bll.Tests.Fake;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameStore.Bll.Tests.Services
{
    public class GenreServiceTests
    {
        public GenreServiceTests()
        {
            _genresInDb = new List<Genre>()
            {
                new Genre
                {
                    Id = 1,
                    Name = "Strategy",
                    ParentGenreId = null
                },
                new Genre
                {
                    Id = 2,
                    Name = "RTS",
                    ParentGenreId = 1
                },
                new Genre
                {
                    Id = 3,
                    Name = "TBS",
                    ParentGenreId = 1
                },
                new Genre
                {
                    Id = 4,
                    Name = "RPG",
                    ParentGenreId = null
                },
                new Genre
                {
                    Id = 5,
                    Name = "Sports",
                    ParentGenreId = null
                },
                new Genre
                {
                    Id = 6,
                    Name = "Races",
                    ParentGenreId = null
                },
                new Genre
                {
                    Id = 7,
                    Name = "Rally",
                    ParentGenreId = 6
                }
            };
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.Setup(x => x.GenreRepository).Returns(new BaseRepositoryFake<Genre>());
            _mapper = new MapperConfiguration(mc => { mc.AddProfile<MapperProfileBll>(); }).CreateMapper();
        }

        private readonly List<Genre> _genresInDb;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly IMapper _mapper;

        [Fact]
        public void GenreNameIsAvailable_PassAvailableGenreName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreName = "newStrategy";

            //Act
            var result = service.NewGenreNameIsAvailable(genreName);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GenreNameIsAvailable_PassNotAvailableGenreName_ReturnsFalse()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreName = "Strategy";

            //Act
            var result = service.NewGenreNameIsAvailable(genreName);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void NewGenreNameIsAvailable_PassAvailableGenreName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreName = "newStrategy";
            var genreId = 1;

            //Act
            var result = service.NewGenreNameIsAvailable(genreName, genreId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void NewGenreNameIsAvailable_PassNotAvailableGenreName_ReturnsFalse()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreName = "RTS";
            var genreId = 1;

            //Act
            var result = service.NewGenreNameIsAvailable(genreName, genreId);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void NewGenreNameIsAvailable_PassPreviousGenreName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreName = "Strategy";
            var genreId = 1;

            //Act
            var result = service.NewGenreNameIsAvailable(genreName, genreId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddGenre_ArgumentFitsAllRequirements_GameAddedToDb()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreToAdd = new GenreDto
            {
                Name = "Arcade",
                ParentGenreId = 6
            };

            //Act
            service.AddGenre(genreToAdd);

            //Assert
            var genreInDb = service.GetGenres()
                .Single(x => x.Name == genreToAdd.Name & x.ParentGenreId == genreToAdd.ParentGenreId);
            Assert.Equal(genreToAdd.Name, genreInDb.Name);
            Assert.Equal(genreToAdd.ParentGenreId, genreInDb.ParentGenreId);
        }

        [Fact]
        public void AddGenre_ArgumentNameExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreToAdd = new GenreDto
            {
                Name = "RTS",
                ParentGenreId = 6
            };

            //Act
            Action addGenre = () => service.AddGenre(genreToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addGenre);
        }

        [Fact]
        public void EditGenre_ArgumentFitsAllRequirements_GenreUpdatedInDb()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreToEdit = new GenreDto
            {
                Id = 7,
                Name = "Rally2",
                ParentGenreId = 1
            };

            //Act
            service.EditGenre(genreToEdit);

            //Assert
            var genreInDb = service.GetGenre(genreToEdit.Id);
            Assert.Equal(genreToEdit.Name, genreInDb.Name);
            Assert.Equal(genreToEdit.ParentGenreId, genreInDb.ParentGenreId);
        }

        [Fact]
        public void EditGenre_ArgumentNameExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreToEdit = new GenreDto
            {
                Id = 4,
                Name = "Strategy",
                ParentGenreId = 1
            };

            //Act
            Action editGenre = () => service.EditGenre(genreToEdit);

            //Assert
            Assert.Throws<ArgumentException>(editGenre);
        }

        [Fact]
        public void GetGenreById_GenreWithThisIdDoesNotExistsInDb_ThrowsInvalidOperationException()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var nonExistingGenreId = 100;

            //Act
            Func<GenreDto> getGenreById = () => service.GetGenre(nonExistingGenreId);

            //Assert
            Assert.Throws<InvalidOperationException>(getGenreById);
        }

        [Fact]
        public void GetGenreById_GenreWithThisIdExistsInDb_ReturnsGenre()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreToGetId = 1;

            //Act
            var genreById = service.GetGenre(genreToGetId);

            //Assert
            Assert.Equal(genreToGetId, genreById.Id);
        }

        [Fact]
        public void GetGenres_WithGenresInDataBase_ReturnsGenresIEnumerable()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);

            //Act
            var getAllGenres = service.GetGenres().ToList();

            //Assert
            Assert.Equal(_genresInDb.Count, getAllGenres.Count);
            Assert.Equal(_genresInDb.Select(x=>x.Id), getAllGenres.Select(x => x.Id));
        }

        [Fact]
        public void GetGenres_WithoutGenresInTheDatabase_ThrowsArgumentException()
        {
            //Arrange
            var service = new GenreService(_uowMock.Object, _mapper);

            //Act
            Func<IEnumerable<GenreDto>> getAllGenres = () => service.GetGenres();

            //Assert
            Assert.Throws<ArgumentException>(getAllGenres);
        }

        [Fact]
        public void DeleteGenreById_GenreWithThisIdExistsInDb_DeletedGenre()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreToDeleteId = 1;

            //Act
            service.SoftDelete(genreToDeleteId);

            //Assert
            var deletedGenre = service.GetGenre(genreToDeleteId);
            Assert.True(deletedGenre.IsDeleted);
        }

        [Fact]
        public void DeleteGenreById_GenreWithThisIdDoesNotExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.GenreRepository.AddRange(_genresInDb);
            var service = new GenreService(_uowMock.Object, _mapper);
            var genreToDeleteId = 100500;

            //Act
            Action deleteGenre = () => service.SoftDelete(genreToDeleteId);

            //Assert
            Assert.Throws<ArgumentException>(deleteGenre);
        }
    }
}
