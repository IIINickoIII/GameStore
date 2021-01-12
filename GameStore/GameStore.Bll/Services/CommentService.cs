using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Bll.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private const string DeletedCommentText = "This comment was deleted.";
        private const string SeparatorForTreatment = ", ";

        public CommentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void AddCommentToGame(CommentCreate commentDto)
        {
            if (commentDto == null)
            {
                throw new ArgumentException($"Argument {nameof(commentDto)} is null");
            }

            if (!_uow.GameRepository.Any(g => g.Id == commentDto.GameId))
            {
                throw new ArgumentException("You can't add comment to non existing game");
            }

            if (commentDto.ParentCommentId.HasValue)
            {
                var nameOfAuthorOfParentComment =
                    _uow.CommentRepository.Single(x => x.Id == commentDto.ParentCommentId).Name;
                var builderCapacity = nameOfAuthorOfParentComment.Length + SeparatorForTreatment.Length + commentDto.Body.Length;
                var commentBody = new StringBuilder(builderCapacity);
                commentBody.Append(nameOfAuthorOfParentComment);
                commentBody.Append(SeparatorForTreatment);
                commentBody.Append(commentDto.Body);
                commentDto.Body = commentBody.ToString();
            }

            var comment = _mapper.Map<Comment>(commentDto);
            comment.Time = DateTime.Now;
            _uow.CommentRepository.Add(comment);
            _uow.Save();
        }

        public IEnumerable<CommentDto> GetAllCommentsByGameKey(string gameKey)
        {
            if (!_uow.GameRepository.Any(g => g.Key == gameKey))
            {
                throw new ArgumentException($"No game with key = {gameKey} in the Database");
            }

            var gameInDb = _uow.GameRepository.Single(g => g.Key == gameKey);
            var comments = _uow.CommentRepository.Find(c => c.GameId == gameInDb.Id);
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            HideTextOfDeletedComments(commentsDto);
            return GetStructuredCommentsTree(commentsDto);
        }

        public void DeleteComment(int commentId)
        {
            if (!_uow.CommentRepository.Any(x => x.Id == commentId))
            {
                throw new ArgumentException($"No comment with Id = {commentId} in the Database");
            }

            var comment = _uow.CommentRepository.Single(x => x.Id == commentId);
            comment.IsDeleted = true;
            _uow.CommentRepository.Update(comment);
            _uow.Save();
        }

        private static IEnumerable<CommentDto> GetStructuredCommentsTree(IEnumerable<CommentDto> commentsDto)
        {
            var structuredCommentsTree = commentsDto.ToList();
            foreach (var commentDto in structuredCommentsTree)
            {
                var childrenCommentsDto = structuredCommentsTree.Where(c => c.ParentCommentId == commentDto.Id).ToList();

                if (childrenCommentsDto.Any())
                {
                    commentDto.ChildrenComments = childrenCommentsDto;
                    structuredCommentsTree = structuredCommentsTree.Except(childrenCommentsDto).ToList();

                    foreach (var childrenCommentDto in commentDto.ChildrenComments.ToList())
                    {
                        GetStructuredCommentsTree(childrenCommentDto.ChildrenComments);
                    }
                }
            }
            return structuredCommentsTree;
        }

        private static void HideTextOfDeletedComments(IEnumerable<CommentDto> comments)
        {
            comments.ToList().ForEach(x =>
            {
                if (x.IsDeleted)
                {
                    x.Body = DeletedCommentText;
                }
            });
        }
    }
}