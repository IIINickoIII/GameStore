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
    public class PublisherServiceTests
    {
        public PublisherServiceTests()
        {
            _publishersInDb = new List<Publisher>()
            {
                new Publisher
                {
                    Id = 1,
                    CompanyName = "Rockstar",
                    Description = "Rockstar Games, Inc. is an American video game publisher " +
                                  "based in New York City. The company was established in December" +
                                  " 1998 as a subsidiary of Take-Two Interactive, using the assets " +
                                  "Take-Two had previously acquired from BMG Interactive. Founding " +
                                  "members of the company were Sam and Dan Houser, Terry Donovan and " +
                                  "Jamie King, who worked for Take-Two at the time, and of which the " +
                                  "Houser brothers were previously executives at BMG Interactive. " +
                                  "Sam Houser heads the studio as president.",
                    HomePage = "rockstar.com"
                },
                new Publisher
                {
                    Id = 2,
                    CompanyName = "Blizzard",
                    Description = "Blizzard Entertainment is a PC, console, and mobile game developer " +
                                  "known for its epic multiplayer titles including the Warcraft, Diablo, " +
                                  "StarCraft, and Overwatch ...",
                    HomePage = "blizzard.com"
                },
                new Publisher
                {
                    Id = 3,
                    CompanyName = "EA Mobile",
                    Description = "EA Mobile Inc. is an American video game development studio of the " +
                                  "publisher Electronic Arts (EA) for mobile platforms. ",
                    HomePage = "ea.com"
                },
                new Publisher
                {
                    Id = 4,
                    CompanyName = "Wargaming",
                    Description = "Wargaming.net is a private company, offshore company, publisher and " +
                                  "developer of computer games, mainly free-to-play MMO genre and near-game " +
                                  "services for various platforms. The headquarters is located in Nicosia, " +
                                  "Republic of Cyprus, the development centers are in Minsk (main), Kiev, " +
                                  "St. Petersburg, Seattle, Chicago, Baltimore, Sydney, Helsinki, Austin and Prague.",
                    HomePage = "wargaming.com"
                }
            };
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.Setup(x => x.PublisherRepository).Returns(new BaseRepositoryFake<Publisher>());
            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MapperProfileBll()); }).CreateMapper();
        }

        private readonly List<Publisher> _publishersInDb;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly IMapper _mapper;

        [Fact]
        public void PublisherCompanyNameIsAvailable_PassAvailableCompanyName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var companyName = "availableCompanyName";

            //Act
            var result = service.NewPublisherCompanyNameIsAvailable(companyName);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void PublisherCompanyNameIsAvailable_PassNotAvailableCompanyName_ReturnsFalse()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var companyName = "Wargaming";

            //Act
            var result = service.NewPublisherCompanyNameIsAvailable(companyName);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void NewPublisherCompanyNameIsAvailable_PassAvailableCompanyName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var companyName = "availableCompanyName";
            var publisherId = 4;

            //Act
            var result = service.NewPublisherCompanyNameIsAvailable(companyName, publisherId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void NewPublisherCompanyNameIsAvailable_PassNotAvailableCompanyName_ReturnsFalse()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var companyName = "Wargaming";
            var publisherId = 3;

            //Act
            var result = service.NewPublisherCompanyNameIsAvailable(companyName, publisherId);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void NewPublisherCompanyNameIsAvailable_PassThePreviousCompanyName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var companyName = "Wargaming";
            var publisherId = 4;

            //Act
            var result = service.NewPublisherCompanyNameIsAvailable(companyName, publisherId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddPublisher_ArgumentFitsAllRequirements_PublisherAddedToDb()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var publisherToAdd = new PublisherDto()
            {
                CompanyName = "GameLoft",
                Description = "Gameloft SE is a French video game publisher based " +
                              "in Paris, founded in December 1999 by Ubisoft co-founder Michel Guillemot.",
                HomePage = "gameloft.com"
            };

            //Act
            service.AddPublisher(publisherToAdd);

            //Assert
            var publisherInDb = service.GetPublishers()
                .Single(p => p.CompanyName == publisherToAdd.CompanyName);
            Assert.Equal(publisherInDb.CompanyName, publisherInDb.CompanyName);
            Assert.Equal(publisherInDb.Description, publisherToAdd.Description);
            Assert.Equal(publisherInDb.HomePage, publisherToAdd.HomePage);
        }

        [Fact]
        public void AddPublisher_ArgumentCompanyNameAlreadyExistsInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var publisherToAdd = new PublisherDto()
            {
                CompanyName = "Rockstar",
                Description = "Rockstar Games, Inc. is an American video game publisher " +
                              "based in New York City. The company was established in December" +
                              " 1998 as a subsidiary of Take-Two Interactive, using the assets " +
                              "Take-Two had previously acquired from BMG Interactive. Founding " +
                              "members of the company were Sam and Dan Houser, Terry Donovan and " +
                              "Jamie King, who worked for Take-Two at the time, and of which the " +
                              "Houser brothers were previously executives at BMG Interactive. " +
                              "Sam Houser heads the studio as president.",
                HomePage = "rockstar.com"
            };

            //Act
            Action addPublisher = () => service.AddPublisher(publisherToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addPublisher);
        }

        [Fact]
        public void EditPublisher_ArgumentFitsAllRequirements_PublisherUpdatedInDb()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var publisherToEdit = new PublisherDto()
            {
                Id = 1,
                CompanyName = "RockstarEdited",
                Description = "NoDescription",  
                HomePage = "rockstar.edited.com"
            };

            //Act
            service.EditPublisher(publisherToEdit);

            //Assert
            var publisherInDb = service.GetPublisher(publisherToEdit.Id);
            Assert.Equal(publisherInDb.CompanyName, publisherToEdit.CompanyName);
            Assert.Equal(publisherInDb.Description, publisherToEdit.Description);
            Assert.Equal(publisherInDb.HomePage, publisherToEdit.HomePage);
            Assert.Equal(publisherInDb.IsDeleted, publisherToEdit.IsDeleted);
        }

        [Fact]
        public void EditPublisher_ArgumentCompanyNameAlreadyExistsInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var publisherToEdit = new PublisherDto()
            {
                Id = 1,
                CompanyName = "Blizzard",
                Description = "NoDescription",
                HomePage = "rockstar.edited.com"
            };

            //Act
            Action editPublisher = () => service.EditPublisher(publisherToEdit);

            //Assert
            Assert.Throws<ArgumentException>(editPublisher);
        }

        [Fact]
        public void GetPublisherById_PublisherWithThisIdExistsInDb_ReturnsPublisher()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var publisherToGetId = 1;

            //Act 
            var publisherInDb = service.GetPublisher(publisherToGetId);

            //Assert
            Assert.Equal(publisherInDb.Id, publisherToGetId);
        }

        [Fact]
        public void GetPublisherById_PublisherWithThisIdDoesNotExistInDb_ThrowsInvalidOperationException()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var publisherToGetId = 100500;

            //Act
            Func<PublisherDto> getPublisherById = () => service.GetPublisher(publisherToGetId);

            //Assert
            Assert.Throws<InvalidOperationException>(getPublisherById);
        }

        [Fact]
        public void GetPublishers_WithPublishersInDb_ReturnsPublishersIEnumerable()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);

            //Act
            var getAllPublishers = service.GetPublishers().ToList();

            //Assert
            Assert.Equal(getAllPublishers.Count, _publishersInDb.Count);
            Assert.Equal(getAllPublishers.Select(p => p.Id), _publishersInDb.Select(p => p.Id));
        }

        [Fact]
        public void GetPublishers_WithNoPublishersInDb_ThrowsArgumentException()
        {
            //Arrange
            var service = new PublisherService(_uowMock.Object, _mapper);

            //Act
            Func<IEnumerable<PublisherDto>> getAllPublishers = () => service.GetPublishers();

            //Assert
            Assert.Throws<ArgumentException>(getAllPublishers);
        }

        [Fact]
        public void DeletePublisherById_PublisherWithThisIdExistsInDb_DeletePublisher()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var publisherToDeleteId = 1;

            //Act
            service.SoftDelete(publisherToDeleteId);

            //Assert
            var deletedPublisher = service.GetPublisher(publisherToDeleteId);
            Assert.True(deletedPublisher.IsDeleted);
        }

        [Fact]
        public void DeletePublisherById_PublisherWithThisIdDoesNotExistInDb_ThrowsInvalidOperationException()
        {
            //Arrange
            _uowMock.Object.PublisherRepository.AddRange(_publishersInDb);
            var service = new PublisherService(_uowMock.Object, _mapper);
            var nonExistingPublisherId = 100500;

            //Act
            Action deletePublisherById = () => service.SoftDelete(nonExistingPublisherId);

            //Assert
            Assert.Throws<InvalidOperationException>(deletePublisherById);
        }
    }
}
