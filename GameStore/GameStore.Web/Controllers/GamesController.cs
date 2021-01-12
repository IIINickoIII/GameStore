using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IGenreService genreService, IPublisherService publisherService, IPlatformTypeService platformTypeService, IMapper mapper)
        {
            _gameService = gameService;
            _genreService = genreService;
            _publisherService = publisherService;
            _platformTypeService = platformTypeService;
            _mapper = mapper;
        }

        [Route("")]
        public IActionResult GetReadOnlyGames()
        {
            var games = _gameService.GetAllGames() ?? new List<GameDto>();
            return View("GameReadOnlyList", games);
        }

        [Route("/games/edit")]
        public IActionResult GetForEditGames()
        {
            var games = _gameService.GetAllGames() ?? new List<GameDto>();
            return View("GameEditList", games);
        }

        [Route("/games/{gameId}/details")]
        public IActionResult GameDetails(int gameId)
        {
            var game = _gameService.GetGameById(gameId) ?? new GameDto();
            return View("GameDetails", game);
        }

        [Route("/games/new")]
        public IActionResult CreateGame()
        {
            var gameViewModel = new GameViewModel
            {
                AllGenresList = _genreService.GetGenres() ?? new List<GenreDto>(),
                AllPlatformTypesList = _platformTypeService.GetAllPlatformTypes() ?? new List<PlatformTypeDto>(),
                AllPublishersList = _publisherService.GetPublishers() ?? new List<PublisherDto>()
            };
            return View("GameForm", gameViewModel);
        }

        [Route("/games/{gameId}/edit")]
        public IActionResult EditGame(int gameId)
        {
            var game = _gameService.GetGameById(gameId);
            var gameViewModel = _mapper.Map<GameViewModel>(game);
            gameViewModel.AllGenresList = _genreService.GetGenres() ?? new List<GenreDto>();
            gameViewModel.AllPlatformTypesList = _platformTypeService.GetAllPlatformTypes() ?? new List<PlatformTypeDto>();
            gameViewModel.AllPublishersList = _publisherService.GetPublishers() ?? new List<PublisherDto>();
            return View("GameForm", gameViewModel);
        }

        [Route("/games/{gameId}/delete")]
        public IActionResult DeleteGame(int gameId)
        {
            _gameService.DeleteGame(gameId);
            return RedirectToAction("GetForEditGames");
        }

        [HttpPost]
        public IActionResult Save([FromForm] GameViewModel gameViewModel)
        {
            var newGameKeyIsAvailable = _gameService.NewGameKeyIsAvailable(gameViewModel.Key, gameViewModel.Id);

            if (!ModelState.IsValid || !newGameKeyIsAvailable)
            {
                if (!newGameKeyIsAvailable)
                {
                    var alternativeGameKey = GenerateAlternativeGameKey(gameViewModel.Id, gameViewModel.Key);
                    var gameKeyErrorMessage = $"{gameViewModel.Key} is not available. Try {alternativeGameKey}";
                    ModelState.AddModelError(nameof(gameViewModel.Key), gameKeyErrorMessage);
                }

                gameViewModel.AllGenresList = _genreService.GetGenres() ?? new List<GenreDto>();
                gameViewModel.AllPlatformTypesList =
                    _platformTypeService.GetAllPlatformTypes() ?? new List<PlatformTypeDto>();
                gameViewModel.AllPublishersList = _publisherService.GetPublishers() ?? new List<PublisherDto>();

                return View("GameForm", gameViewModel);
            }

            var gameCreate = _mapper.Map<GameCreate>(gameViewModel);

            if (gameViewModel.Id == 0)
            {
                _gameService.AddGame(gameCreate);
            }
            else
            {
                _gameService.EditGame(gameCreate, gameViewModel.Id);
            }

            return RedirectToAction("GetForEditGames");
        }

        [Route("/games/{gameId}/download")]
        public IActionResult DownloadGame(int gameId)
        {
            var game = _gameService.GetGameById(gameId) ?? new GameDto();
            return View("GameDownload", game);
        }

        public FileContentResult GetGameFile(string gameKey)
        {
            var game = _gameService.GetGameByKey(gameKey) ?? new GameDto();
            var textView = game.ToString();

            return File(Encoding.UTF8.GetBytes(textView), "text/plain", $"Game Store - {game.Name}.txt");
        }

        [HttpGet]
        [Route("/games/numberofgames")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 60)]
        public JsonResult NumberOfGames()
        {
            return new JsonResult(_gameService.GetNumberOfGames());
        }

        private string GenerateAlternativeGameKey(int gameId, string gameKey)
        {
            while (true)
            {
                var alternativeGameKey = gameKey + Guid.NewGuid();
                if (_gameService.NewGameKeyIsAvailable(alternativeGameKey, gameId))
                {
                    return alternativeGameKey;
                }
            }
        }
    }
}
