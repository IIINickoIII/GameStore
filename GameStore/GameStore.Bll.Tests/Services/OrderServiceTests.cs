using AutoMapper;
using GameStore.Bll.Mapper;
using GameStore.Bll.Payment.Interfaces;
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
    public class OrderServiceTests
    {
        public OrderServiceTests()
        {
            _ordersInDb = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Date = DateTime.Now,
                    TotalSum = 100,
                    CustomerId = 10,
                    IsClosedForEdit = false,
                    IsPaid = false
                }
            };
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.Setup(x => x.OrderRepository).Returns(new BaseRepositoryFake<Order>());
            _paymentContextMock = new Mock<IPaymentContext>();
            _mapper = new MapperConfiguration(mc => { mc.AddProfile<MapperProfileBll>(); }).CreateMapper();
        }

        private readonly List<Order> _ordersInDb;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IPaymentContext> _paymentContextMock;
        private readonly IMapper _mapper;

        [Fact]
        public void GetAllOrders_WithOrdersInTheDatabase_ReturnsOrdersIEnumerable()
        {
            //Arrange
            _uowMock.Object.OrderRepository.AddRange(_ordersInDb);
            var service = new OrderService(_uowMock.Object, _mapper, _paymentContextMock.Object);

            //Act
            var allOrders = service.GetAllOrders().ToList();

            //Assert
            Assert.Equal(_ordersInDb.Count, allOrders.Count());
            Assert.Equal(_ordersInDb.Select(x => x.Id), allOrders.Select(x => x.Id));
        }

        [Fact]
        public void GetAllOrders_WithOutOrdersInTheDatabase_ReturnsOrdersEmptyIEnumerable()
        {
            //Arrange
            var countOfOrdersInDb = 0;
            var service = new OrderService(_uowMock.Object, _mapper, _paymentContextMock.Object);

            //Act
            var allOrders = service.GetAllOrders().ToList();

            //Assert
            Assert.Equal(countOfOrdersInDb, allOrders.Count());
        }
    }
}
