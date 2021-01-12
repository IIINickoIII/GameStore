using GameStore.Bll.Dto;
using System.Collections.Generic;

namespace GameStore.Bll.Interfaces
{
    public interface IGameService
    {
        void AddGame(GameCreate gameDto);

        void EditGame(GameCreate gameDto, int gameId);

        GameDto GetGameById(int gameId);

        GameDto GetGameByKey(string gameKey);

        IEnumerable<GameDto> GetGamesByGenre(int genreId);

        IEnumerable<GameDto> GetGamesByPlatformTypes(List<int> platformTypeIds);

        IEnumerable<GameDto> GetAllGames();

        void DeleteGame(int gameId);

        bool NewGameKeyIsAvailable(string newGameKey, int gameId = 0);

        int GetNumberOfGames();

        string GetGameKeyByCommentId(int commentId);
    }
}