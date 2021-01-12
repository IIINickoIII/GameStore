using System.Collections.Generic;
using GameStore.Bll.Dto;

namespace GameStore.Bll.Interfaces
{
    public interface ICommentService
    {
        void AddCommentToGame(CommentCreate commentDto);

        IEnumerable<CommentDto> GetAllCommentsByGameKey(string gameKey);

        void DeleteComment(int commentId);
    }
}