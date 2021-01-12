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
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PublisherService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void AddPublisher(PublisherDto publisherDto)
        {
            if (!NewPublisherCompanyNameIsAvailable(publisherDto.CompanyName))
            {
                throw new ArgumentException($"{publisherDto.CompanyName} publisher company name is already taken");
            }

            var publisher = _mapper.Map<Publisher>(publisherDto);
            _uow.PublisherRepository.Add(publisher);
            _uow.Save();
        }

        public void EditPublisher(PublisherDto publisherDto)
        {
            if (!NewPublisherCompanyNameIsAvailable(publisherDto.CompanyName, publisherDto.Id))
            {
                throw new ArgumentException($"{publisherDto.CompanyName} publisher company name is already taken");
            }

            var publisher = _mapper.Map<Publisher>(publisherDto);
            _uow.PublisherRepository.Update(publisher);
            _uow.Save();
        }

        public PublisherDto GetPublisher(int publisherId)
        {
            if (!_uow.PublisherRepository.Any(p => p.Id == publisherId))
            {
                throw new InvalidOperationException($"No PlatformType with Id = {publisherId} in the Database");
            }

            var publisherInDb = _uow.PublisherRepository.Single(p => p.Id == publisherId);
            var publisherDto = _mapper.Map<PublisherDto>(publisherInDb);
            return publisherDto;
        }

        public IEnumerable<PublisherDto> GetPublishers()
        {
            var publishersInDb = _uow.PublisherRepository
                .Find(p => p.IsDeleted == false);

            if (!publishersInDb.Any())
            {
                throw new ArgumentException("No Publishers found in the Database");
            }

            var publishersDto = _mapper.Map<IEnumerable<PublisherDto>>(publishersInDb);
            return publishersDto;
        }

        public void SoftDelete(int publisherId)
        {
            if (!_uow.PublisherRepository.Any(p => p.Id == publisherId))
            {
                throw new InvalidOperationException($"No Publisher with Id = {publisherId} in the Database");
            }

            var publisherInDb = _uow.PublisherRepository.Single(p => p.Id == publisherId);
            _uow.PublisherRepository.SoftDelete(publisherInDb);
            _uow.Save();
        }

        public bool NewPublisherCompanyNameIsAvailable(string newCompanyName, int publisherId = 0)
        {
            var publisherCompanyNameIsAvailable = !_uow.PublisherRepository.Any(p => p.CompanyName == newCompanyName);

            if (publisherId == 0)
            {
                return publisherCompanyNameIsAvailable;
            }

            if (!_uow.PublisherRepository.Any(p => p.Id == publisherId))
            {
                throw new ArgumentException($"No publisher with Id = {publisherId} in the Database");
            }

            var oldPublisherCompanyName = _uow.PublisherRepository.Single(p => p.Id == publisherId).CompanyName;
            return oldPublisherCompanyName.Equals(newCompanyName) || publisherCompanyNameIsAvailable;
        }
    }
}
