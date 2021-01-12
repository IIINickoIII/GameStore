using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameStore.Web.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublishersController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [Route("/Publishers")]
        public IActionResult GetReadOnlyPublishers()
        {
            var publishers = _publisherService.GetPublishers() ?? new List<PublisherDto>();
            return View("PublisherReadOnlyList", publishers);
        }

        [Route("/Publishers/Edit")]
        public IActionResult GetForEditPublishers()
        {
            var publishers = _publisherService.GetPublishers() ?? new List<PublisherDto>();
            return View("PublisherEditList", publishers);
        }

        [Route("/Publishers/{publisherId}/Details")]
        public IActionResult PublisherDetails(int publisherId)
        {
            var publisher = _publisherService.GetPublisher(publisherId) ?? new PublisherDto();
            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);
            return View("PublisherDetails", publisherViewModel);
        }

        [Route("/Publishers/New")]
        public IActionResult CreatePublisher()
        {
            var publisherViewModel = new PublisherViewModel();
            return View("PublisherForm", publisherViewModel);
        }

        [Route("/Publishers/{publisherId}/Edit")]
        public IActionResult EditPublisher(int publisherId)
        {
            var publisher = _publisherService.GetPublisher(publisherId) ?? new PublisherDto();
            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);
            return View("PublisherForm", publisherViewModel);
        }

        [Route("/Publishers/{publisherId}/Delete")]
        public IActionResult DeletePublisher(int publisherId)
        {
            _publisherService.SoftDelete(publisherId);
            return RedirectToAction("GetForEditPublishers");
        }

        [HttpPost]
        public IActionResult Save(PublisherViewModel publisherViewModel)
        {
            var newPublisherCompanyNameIsAvailable =
                _publisherService.NewPublisherCompanyNameIsAvailable(publisherViewModel.CompanyName, publisherViewModel.Id);

            if (!ModelState.IsValid || !newPublisherCompanyNameIsAvailable)
            {
                if (!newPublisherCompanyNameIsAvailable)
                {
                    var alternativeCompanyName =
                        GenerateAlternativeCompanyName(publisherViewModel.Id, publisherViewModel.CompanyName);
                    var companyNameErrorMessage =
                        $"{publisherViewModel.CompanyName} is not available. Try {alternativeCompanyName}";
                    ModelState.AddModelError(nameof(publisherViewModel.CompanyName), companyNameErrorMessage);
                }

                return View("PublisherForm", publisherViewModel);
            }

            var publisherDto = _mapper.Map<PublisherDto>(publisherViewModel);

            if (publisherDto.Id == 0)
            {
                _publisherService.AddPublisher(publisherDto);
            }
            else
            {
                _publisherService.EditPublisher(publisherDto);
            }
            return RedirectToAction("GetForEditPublishers");
        }

        private string GenerateAlternativeCompanyName(int publisherId, string publisherCompanyName)
        {
            while (true)
            {
                var alternativeCompanyName = publisherCompanyName + Guid.NewGuid();
                if (_publisherService.NewPublisherCompanyNameIsAvailable(alternativeCompanyName, publisherId))
                {
                    return alternativeCompanyName;
                }
            }
        }
    }
}
