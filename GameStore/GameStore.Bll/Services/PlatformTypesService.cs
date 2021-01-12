using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;

namespace GameStore.Bll.Services
{
    public class PlatformTypesService : IPlatformTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public PlatformTypesService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public void AddPlatformType(PlatformTypeDto platformTypeDto)
        {
            if (!NewPlatformTypeNameIsAvailable(platformTypeDto.Type))
            {
                throw new ArgumentException($"{platformTypeDto.Type} platform type name is already taken");
            }

            var platformType = _mapper.Map<PlatformType>(platformTypeDto);
            _uow.PlatformTypeRepository.Add(platformType);
            _uow.Save();
        }

        public void EditPlatformType(PlatformTypeDto platformTypeDto)
        {
            if (!NewPlatformTypeNameIsAvailable(platformTypeDto.Type, platformTypeDto.Id))
            {
                throw new ArgumentException($"{platformTypeDto.Type} platform type name is already taken");
            }

            var platformType = _mapper.Map<PlatformType>(platformTypeDto);
            _uow.PlatformTypeRepository.Update(platformType);
            _uow.Save();
        }

        public PlatformTypeDto GetPlatformType(int platformTypeId)
        {
            if (!_uow.PlatformTypeRepository.Any(p => p.Id == platformTypeId))
            {
                throw new InvalidOperationException($"No PlatformType with Id = {platformTypeId} in the Database");
            }

            var platformTypeInDb = _uow.PlatformTypeRepository.Single(p => p.Id == platformTypeId);
            var platformTypeDto = _mapper.Map<PlatformTypeDto>(platformTypeInDb);
            return platformTypeDto;
        }

        public IEnumerable<PlatformTypeDto> GetAllPlatformTypes()
        {
            var platformTypesInDb = _uow.PlatformTypeRepository
                .Find(p => p.IsDeleted == false);

            if (!platformTypesInDb.Any())
            {
                throw new ArgumentException("No PlatformTypes found in the Database");
            }

            var platformTypesDto = _mapper.Map<IEnumerable<PlatformTypeDto>>(platformTypesInDb);
            return platformTypesDto;
        }

        public void SoftDelete(int platformTypeId)
        {
            if (!_uow.PlatformTypeRepository.Any(p => p.Id == platformTypeId))
            {
                throw new InvalidOperationException($"No PlatformType with Id = {platformTypeId} in the Database");
            }

            var platformTypeInDb = _uow.PlatformTypeRepository.Single(p => p.Id == platformTypeId);
            _uow.PlatformTypeRepository.SoftDelete(platformTypeInDb);
            _uow.Save();
        }

        public bool NewPlatformTypeNameIsAvailable(string newTypeName, int platformTypeId = 0)
        {
            var platformTypeNameIsAvailable = !_uow.PlatformTypeRepository.Any(p => p.Type == newTypeName);

            if (platformTypeId == 0)
            {
                return platformTypeNameIsAvailable;
            }

            if (!_uow.PlatformTypeRepository.Any(p => p.Id == platformTypeId))
            {
                throw new ArgumentException($"No Platform Type with Id = {platformTypeId} in the Database");
            }

            var oldPlatformTypeTypeName = _uow.PlatformTypeRepository.Single(p => p.Id == platformTypeId).Type;
            return oldPlatformTypeTypeName.Equals(newTypeName) || platformTypeNameIsAvailable;
        }
    }
}
