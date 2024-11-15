using ApiContracts.CommentDtos;
using ApiContracts.PostDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Components.Services;

public interface ICommentService
{
    public Task<ActionResult<CommentDto>> UpdateComment(int id,
        UpdateCommentReq commentResponseInfo);

    public  Task<ActionResult<CommentDto>> GetSingleComment(int id);
    public Task<IEnumerable<CommentDto>> GetMany(int? postId, int? userId);
    public Task<ActionResult> DeleteComment(int id);
}