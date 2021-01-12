using GameStore.Bll.Dto.Enums;
using GameStore.Bll.Interfaces;
using System;

namespace GameStore.Bll.Services
{
    public class UserService : IUserService
    {
        public DateTime BanTimeEnd = DateTime.Now;
        public int UserId;

        public void BanUser(int userId, BanDurationEnum banDuration)
        {
            this.UserId = userId;
            BanTimeEnd = banDuration switch
            {
                BanDurationEnum.OneHour => DateTime.Now.AddHours(1),
                BanDurationEnum.OneDay => DateTime.Now.AddDays(1),
                BanDurationEnum.OneWeek => DateTime.Now.AddDays(7),
                BanDurationEnum.OneMonth => DateTime.Now.AddMonths(1),
                BanDurationEnum.Permanent => DateTime.MaxValue,
                _ => BanTimeEnd
            };
        }
    }
}
