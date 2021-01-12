using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GameStore.Web.Controllers.Api
{
    [ApiController]
    [Route("api/")]
    public class GamesController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService, ICommentService commentService)
        {
            _commentService = commentService;
            _gameService = gameService;
        }

        // GET url: /games

        [HttpGet]
        [Route("games")]
        public JsonResult GetAllGames()
        {
            return Json(_gameService.GetAllGames());
        }

        // GET url: /games/{key}

        [HttpGet]
        [Route("games/{gameKey}")]
        public JsonResult GetGameByKey(string gameKey)
        {
            return Json(_gameService.GetGameByKey(gameKey));
        }

        // POST url: /games/new

        [HttpPost]
        [Route("games/new")]
        public ActionResult AddGame([FromBody] GameCreate gameDto)
        {
            _gameService.AddGame(gameDto);
            return Ok();
        }

        // POST url: /games/update

        [HttpPost]
        [Route("games/update")]
        public ActionResult UpdateGame([FromBody] GameCreate gameDto, int gameDtoId)
        {
            _gameService.EditGame(gameDto, gameDtoId);
            return Ok();
        }

        //// DELETE url: /games/remove/{id}

        [HttpDelete]
        [Route("games/remove")]
        public ActionResult RemoveGameById(int id)
        {
            _gameService.DeleteGame(id);
            return Ok();
        }

        // GET url: /game/{gamekey}/comments

        [HttpGet]
        [Route("game/{gameKey}/comments")]
        public JsonResult GetAllCommentsByGameKey(string gameKey)
        {
            return Json(_commentService.GetAllCommentsByGameKey(gameKey));
        }


        // POST url: /game/{gamekey}/newcomment

        [HttpPost]
        [Route("game/{gameKey}/newcomment")]
        public ActionResult AddCommentByGameKey(string gameKey, [FromBody] CommentCreate commentDto)
        {
            _commentService.AddCommentToGame(commentDto);
            return Ok();
        }

        // GET url: /game/{gamekey}/download

        [HttpGet]
        [Route("/api/game/{gameKey}/download")]
        public FileContentResult File(string gameKey)
        {
            var game = _gameService.GetGameByKey(gameKey);
            var textView = game.ToString();

            return File(Encoding.UTF8.GetBytes(textView), "text/plain", $"Game Store - {game.Name}.txt");
        }
    }
}