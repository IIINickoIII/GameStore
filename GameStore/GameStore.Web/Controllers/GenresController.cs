using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameStore.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [Route("/Genres")]
        public IActionResult GetReadOnlyGenres()
        {
            var genres = _genreService.GetStructuredGenres() ?? new List<GenreDto>();
            return View("GenreReadOnlyList", genres);
        }

        [Route("/Genres/Edit")]
        public IActionResult GetForEditGenres()
        {
            var genres = _genreService.GetGenres() ?? new List<GenreDto>();
            return View("GenreEditList", genres);
        }

        [Route("/Genres/{genreId}/Details")]
        public IActionResult GenreDetails(int genreId)
        {
            var genre = _genreService.GetGenre(genreId) ?? new GenreDto();
            var genreViewModel = _mapper.Map<GenreViewModel>(genre);
            return View("GenreDetails", genreViewModel);
        }

        [Route("/Genres/New")]
        public IActionResult CreateGenre()
        {
            var genreViewModel = new GenreViewModel()
            {
                AllGenresList = _genreService.GetGenres() ?? new List<GenreDto>()
            };
            return View("GenreForm", genreViewModel);
        }

        [Route("/Genres/{genreId}/Edit")]
        public IActionResult EditGenre(int genreId)
        {
            var genre = _genreService.GetGenre(genreId) ?? new GenreDto();
            var genreViewModel = _mapper.Map<GenreViewModel>(genre);
            genreViewModel.AllGenresList = _genreService.GetGenres();
            return View("GenreForm", genreViewModel);
        }

        [Route("/Genres/{genreId}/Delete")]
        public IActionResult DeleteGenre(int genreId)
        {
            _genreService.SoftDelete(genreId);
            return RedirectToAction("GetForEditGenres");
        }

        [HttpPost]
        public IActionResult Save(GenreViewModel genreViewModel)
        {
            var newGenreNameIsAvailable = _genreService.NewGenreNameIsAvailable(genreViewModel.Name, genreViewModel.Id);

            if (!ModelState.IsValid || !newGenreNameIsAvailable)
            {
                if (!newGenreNameIsAvailable)
                {
                    var alternativeGenreName = GenerateAlternativeGenreName(genreViewModel.Id, genreViewModel.Name);
                    var genreNameErrorMessage = $"{genreViewModel.Name} is not available. Try {alternativeGenreName}";
                    ModelState.AddModelError(nameof(genreViewModel.Name), genreNameErrorMessage);
                }

                genreViewModel.AllGenresList = _genreService.GetGenres() ?? new List<GenreDto>();
                return View("GenreForm", genreViewModel);
            }

            var genreDto = _mapper.Map<GenreDto>(genreViewModel);

            if (genreDto.Id == 0)
            {
                _genreService.AddGenre(genreDto);
            }
            else
            {
                _genreService.EditGenre(genreDto);
            }
            return RedirectToAction("GetForEditGenres");
        }

        private string GenerateAlternativeGenreName(int genreId, string genreName)
        {
            while (true)
            {
                var alternativeGenreName = genreName + Guid.NewGuid();
                if (_genreService.NewGenreNameIsAvailable(alternativeGenreName, genreId))
                {
                    return alternativeGenreName;
                }
            }
        }
    }
}
