using GameStore.Bll.Dto.Enums;
using GameStore.Bll.Services;
using Xunit;

namespace GameStore.Bll.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void BanUser_ArgumentsNotNull_TestStub()
        {
            //Arrange
            var service = new UserService();
            var userId = 10;
            var banDuration = BanDurationEnum.OneHour;

            //Act
            service.BanUser(userId, banDuration);

            //Assert
            Assert.Equal(userId, service.UserId);
        }
    }
}
