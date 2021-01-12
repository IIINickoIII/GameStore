using GameStore.Bll.Dto.Enums;

namespace GameStore.Bll.Interfaces
{
    public interface IUserService
    {
        void BanUser(int userId, BanDurationEnum banDuration);
    }
}
