using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameStore.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;

        public CommentsController(ICommentService commentService, IGameService gameService)
        {
            _commentService = commentService;
            _gameService = gameService;
        }

        [Route("/game/{gameKey}/comments")]
        public IActionResult GetCommentsByGameKey(string gameKey)
        {
            var gameCommentViewModel = new GameCommentViewModel()
            {
                Comments = _commentService.GetAllCommentsByGameKey(gameKey) ?? new List<CommentDto>(),
                Game = _gameService.GetGameByKey(gameKey) ?? new GameDto()
            };
            return View("GameCommentsList", gameCommentViewModel);
        }

        [HttpPost]
        public IActionResult AddCommentToGame([FromForm] CommentCreate commentCreate, string gameKey)
        {
            if (!ModelState.IsValid)
            {
                var gameCommentViewModel = new GameCommentViewModel()
                {
                    Comments = _commentService.GetAllCommentsByGameKey(gameKey) ?? new List<CommentDto>(),
                    Game = _gameService.GetGameByKey(gameKey) ?? new GameDto()
                };
                return View("GameCommentsList", gameCommentViewModel);
            }
            _commentService.AddCommentToGame(commentCreate);
            return RedirectToAction("GetCommentsByGameKey", "Comments", routeValues: new { gameKey });
        }

        [Route("/comments/delete/{commentId}")]
        public IActionResult DeleteComment([FromRoute] int commentId)
        {
            _commentService.DeleteComment(commentId);
            return RedirectToAction("GetCommentsByGameKey", routeValues: new
            {
                gameKey = _gameService.GetGameKeyByCommentId(commentId)
            });
        }
    }
}
