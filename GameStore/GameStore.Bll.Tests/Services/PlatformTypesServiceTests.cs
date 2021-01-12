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
using Xunit;

namespace GameStore.Bll.Tests.Services
{
    public class PlatformTypesServiceTests
    {
        public PlatformTypesServiceTests()
        {
            _platformTypesInDb = new List<PlatformType>()
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
                    Type = "PS4"
                },
                new PlatformType
                {
                    Id = 4,
                    Type = "Xbox360"
                },
                new PlatformType
                {
                    Id = 5,
                    Type = "XboxONE"
                }
            };
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.Setup(p => p.PlatformTypeRepository).Returns(new BaseRepositoryFake<PlatformType>());
            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MapperProfileBll()); }).CreateMapper();
        }

        private readonly List<PlatformType> _platformTypesInDb;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly IMapper _mapper;

        [Fact]
        public void PlatformTypeNameIsAvailable_PassAvailablePlatformTypeName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeName = "newPC";

            //Act
            var result = service.NewPlatformTypeNameIsAvailable(platformTypeName);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void PlatformTypeNameIsAvailable_PassNotAvailablePlatformTypeName_ReturnsFalse()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeName = "PC";

            //Act
            var result = service.NewPlatformTypeNameIsAvailable(platformTypeName);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void NewPlatformTypeNameIsAvailable_PassAvailablePlatformTypeName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeName = "newPC";
            var platformTypeId = 1;

            //Act
            var result = service.NewPlatformTypeNameIsAvailable(platformTypeName, platformTypeId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void NewPlatformTypeNameIsAvailable_PassNotAvailablePlatformTypeName_ReturnsFalse()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeName = "PC";
            var platformTypeId = 2;

            //Act
            var result = service.NewPlatformTypeNameIsAvailable(platformTypeName, platformTypeId);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void newPlatformTypeNameIsAvailable_PassPreviousPlatformTypeName_ReturnsTrue()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeName = "PC";
            var platformTypeId = 1;

            //Act
            var result = service.NewPlatformTypeNameIsAvailable(platformTypeName, platformTypeId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddPlatformType_ArgumentsFitAllRequirements_PlatformTypeAddedToDb()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeToAdd = new PlatformTypeDto()
            {
                Type = "NewPlatformType"
            };

            //Act
            service.AddPlatformType(platformTypeToAdd);

            //Assert
            var platformTypeInDb = service.GetAllPlatformTypes()
                .Single(p => p.Type == platformTypeToAdd.Type);
            Assert.Equal(platformTypeToAdd.Type, platformTypeInDb.Type);
        }

        [Fact]
        public void AddPlatformType_ArgumentTypeExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeToAdd = new PlatformTypeDto()
            {
                Type = "PC"
            };

            //Act
            Action addPlatformType = () => service.AddPlatformType(platformTypeToAdd);

            //Assert
            Assert.Throws<ArgumentException>(addPlatformType);
        }

        [Fact]
        public void EditPlatformType_ArgumentsFitAllRequirements_PlatformTypeUpdatedInDb()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeToEdit = new PlatformTypeDto()
            {
                Id = 1,
                Type = "PC-CP"
            };

            //Act
            service.EditPlatformType(platformTypeToEdit);

            //Asset
            var platformTypeInDb = service.GetPlatformType(platformTypeToEdit.Id);
            Assert.Equal(platformTypeToEdit.Type, platformTypeInDb.Type);
        }

        [Fact]
        public void EditPlatformType_ArgumentTypeExistInDb_ThrowsArgumentException()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeToEdit = new PlatformTypeDto()
            {
                Id = 1,
                Type = "PS3"
            };

            //Act
            Action editPlatformType = () => service.EditPlatformType(platformTypeToEdit);

            //Asset
            Assert.Throws<ArgumentException>(editPlatformType);
        }

        [Fact]
        public void GetPlatformTypeById_PlatformTypeWithThisIdExistsInDb_ReturnsPlatformType()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeToGetId = 1;

            //Act
            var platfomrTypeInDb = service.GetPlatformType(platformTypeToGetId);

            //Asset
            Assert.Equal(platfomrTypeInDb.Id, platformTypeToGetId);
        }

        [Fact]
        public void GetPlatformTypeById_PlatformTypeWithThisIdDoesNotExistsInDb_ThrowsInvalidOperationException()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var nonExistingPlatformTypeId = 100500;

            //Act
            Func<PlatformTypeDto> getPlatformTypeById = () => service.GetPlatformType(nonExistingPlatformTypeId);

            //Asset
            Assert.Throws<InvalidOperationException>(getPlatformTypeById);
        }

        [Fact]
        public void GetPlatformTypes_WithPlatformTypesInDb_ReturnsPlatformTypesIEnumerable()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);

            //Act
            var allPlatformTypes = service.GetAllPlatformTypes().ToList();

            //Asset
            Assert.Equal(allPlatformTypes.Count, _platformTypesInDb.Count);
            Assert.Equal(allPlatformTypes.Select(p => p.Id), _platformTypesInDb.Select(p => p.Id));
        }

        [Fact]
        public void GetPlatformTypes_WithNoPlatformTypeInDb_ThrowsArgumentException()
        {
            //Arrange
            var service = new PlatformTypesService(_mapper, _uowMock.Object);

            //Act
            Func<IEnumerable<PlatformTypeDto>> getAllPlatformTypes = () => service.GetAllPlatformTypes();

            //Asset
            Assert.Throws<ArgumentException>(getAllPlatformTypes);
        }

        [Fact]
        public void DeletePlatformTypeById_PlatformTypeWithThisIdExistsInDb_DeletePlatformType()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var platformTypeToDeleteId = 1;

            //Act
            service.SoftDelete(platformTypeToDeleteId);

            //Asset
            var deletedPlatformType = service.GetPlatformType(platformTypeToDeleteId);
            Assert.True(deletedPlatformType.IsDeleted);
        }

        [Fact]
        public void DeletePlatformTypeById_PlatformTypeWithThisIdDoesNotExistInDb_ThrowsInvalidOperationException()
        {
            //Arrange
            _uowMock.Object.PlatformTypeRepository.AddRange(_platformTypesInDb);
            var service = new PlatformTypesService(_mapper, _uowMock.Object);
            var nonExistingPlatformTypeId = 100500;

            //Act
            Action deletePlatformTypeById = () => service.SoftDelete(nonExistingPlatformTypeId);

            //Asset
            Assert.Throws<InvalidOperationException>(deletePlatformTypeById);
        }
    }
}
