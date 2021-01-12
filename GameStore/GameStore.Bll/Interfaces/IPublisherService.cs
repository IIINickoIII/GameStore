using GameStore.Bll.Dto;
using System.Collections.Generic;

namespace GameStore.Bll.Interfaces
{
    public interface IPublisherService
    {
        void AddPublisher(PublisherDto publisherDto);

        void EditPublisher(PublisherDto publisherDto);

        PublisherDto GetPublisher(int publisherId);

        IEnumerable<PublisherDto> GetPublishers();

        void SoftDelete(int publisherId);

        bool NewPublisherCompanyNameIsAvailable(string newCompanyName, int publisherId = 0);
    }
}
