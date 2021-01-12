using GameStore.Bll.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult SelectBanDuration(int userId)
        {
            return View("BanDuration", new UserBanViewModel() { UserId = userId });
        }

        [HttpPost]
        public IActionResult BanUser(UserBanViewModel userBanViewModel)
        {
            _userService.BanUser(userBanViewModel.UserId, userBanViewModel.BanDurationEnum);
            return RedirectToAction("GetReadOnlyGames", "Games");
        }
    }
}
