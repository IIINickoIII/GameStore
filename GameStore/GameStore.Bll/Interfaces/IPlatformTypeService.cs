using GameStore.Bll.Dto;
using System.Collections.Generic;

namespace GameStore.Bll.Interfaces
{
    public interface IPlatformTypeService
    {
        void AddPlatformType(PlatformTypeDto platformTypeDto);

        void EditPlatformType(PlatformTypeDto platformTypeDto);

        PlatformTypeDto GetPlatformType(int platformTypeId);

        IEnumerable<PlatformTypeDto> GetAllPlatformTypes();

        void SoftDelete(int platformTypeId);

        bool NewPlatformTypeNameIsAvailable(string newTypeName, int platformTypeId = 0);
    }
}
