using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameStore.Web.Controllers
{
    public class PlatformTypesController : Controller
    {
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IMapper _mapper;

        public PlatformTypesController(IPlatformTypeService platformTypeService, IMapper mapper)
        {
            _platformTypeService = platformTypeService;
            _mapper = mapper;
        }

        [Route("/PlatformTypes")]
        public IActionResult GetReadOnlyPlatformTypes()
        {
            var platformTypes = _platformTypeService.GetAllPlatformTypes() ?? new List<PlatformTypeDto>();
            return View("PlatformTypeReadOnlyList", platformTypes);
        }

        [Route("/PlatformTypes/Edit")]
        public IActionResult GetForEditPlatformTypes()
        {
            var platformTypes = _platformTypeService.GetAllPlatformTypes() ?? new List<PlatformTypeDto>();
            return View("PlatformTypesEditList", platformTypes);
        }

        [Route("/PlatformTypes/{platformTypeId}/Details")]
        public IActionResult PlatformTypeDetails(int platformTypeId)
        {
            var platformType = _platformTypeService.GetPlatformType(platformTypeId) ?? new PlatformTypeDto();
            var platformTypeViewModel = _mapper.Map<PlatformTypeViewModel>(platformType);
            return View("PlatformTypeDetails", platformTypeViewModel);
        }

        [Route("/PlatformTypes/New")]
        public IActionResult CreatePlatformType()
        {
            var platformTypeViewModel = new PlatformTypeViewModel();
            return View("PlatformTypeForm", platformTypeViewModel);
        }

        [Route("/PlatformTypes/{platformTypeId}/Edit")]
        public IActionResult EditPlatformType(int platformTypeId)
        {
            var platformType = _platformTypeService.GetPlatformType(platformTypeId) ?? new PlatformTypeDto();
            var platformTypeViewModel = _mapper.Map<PlatformTypeViewModel>(platformType);
            return View("PlatformTypeForm", platformTypeViewModel);
        }

        [Route("/PlatformTypes/{platformTypeId}/Delete")]
        public IActionResult DeletePlatformType(int platformTypeId)
        {
            _platformTypeService.SoftDelete(platformTypeId);
            return RedirectToAction("GetForEditPlatformTypes");
        }

        [HttpPost]
        public IActionResult Save(PlatformTypeViewModel platformTypeViewModel)
        {
            var newPlatformTypeNameIsAvailable =
                _platformTypeService.NewPlatformTypeNameIsAvailable(platformTypeViewModel.Type, platformTypeViewModel.Id);

            if (!ModelState.IsValid || !newPlatformTypeNameIsAvailable)
            {
                if (!newPlatformTypeNameIsAvailable)
                {
                    var alternativePLatformTypeName =
                        GenerateAlternativePlatformTypeName(platformTypeViewModel.Id, platformTypeViewModel.Type);
                    var typeNameErrorMessage =
                        $"{platformTypeViewModel.Type} is not available. Try {alternativePLatformTypeName}";
                    ModelState.AddModelError(nameof(platformTypeViewModel.Type), typeNameErrorMessage);
                }

                return View("PlatformTypeForm", platformTypeViewModel);
            }

            var platformTypeDto = _mapper.Map<PlatformTypeDto>(platformTypeViewModel);

            if (platformTypeViewModel.Id == 0)
            {
                _platformTypeService.AddPlatformType(platformTypeDto);
            }
            else
            {
                _platformTypeService.EditPlatformType(platformTypeDto);
            }
            return RedirectToAction("GetForEditPlatformTypes");
        }

        private string GenerateAlternativePlatformTypeName(int platformTypeId, string platformTypeName)
        {
            while (true)
            {
                var alternativePlatformTypeName = platformTypeName + Guid.NewGuid();
                if (_platformTypeService.NewPlatformTypeNameIsAvailable(alternativePlatformTypeName, platformTypeId))
                {
                    return alternativePlatformTypeName;
                }
            }
        }
    }
}
