using System.Collections;
using ApiContracts.CommentDtos;
using ApiContracts.PostDtos;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Components.Services;

public interface IPostService
{
    public Task<PostDto> AddPost(CreatePostDto request);
    public  Task<ActionResult<PostDto>> UpdatePost([FromRoute] int id, UpdatePostDto postInfo);
    public Task<GetPostDto> GetSinglePost(int id);
    public Task<IEnumerable<ManyPostDto>> GetMany(string? nameContains, int? userId);
    public Task<ActionResult> DeletePost(int id);
    public Task<CommentDto> AddComment(int postId, CreateCommentDto request);
}