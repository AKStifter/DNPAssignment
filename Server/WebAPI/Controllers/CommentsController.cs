using ApiContracts;
using ApiContracts.CommentDtos;
using ApiContracts.PostDtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{

    private  readonly IUserRepository userRepo;

    private readonly ICommentRepository commentRepo;
    
    public CommentsController(IUserRepository userRepo, ICommentRepository commentRepo)
    {
        this.userRepo = userRepo;
        this.commentRepo = commentRepo;
    }
    

    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> UpdateComment([FromRoute] int id,
        [FromBody] UpdateCommentReq commentResponseInfo)
    {
        try
        {
            Comment comment = new Comment(id, commentResponseInfo.UserId, commentResponseInfo.PostId, commentResponseInfo.Body);
            
            await commentRepo.UpdateAsync(comment);
            Comment updated = await commentRepo.GetSingleAsync(comment.Id);
            UpdateCommentResponseDto dto = new()
            {
                Id = updated.Id,
                Body = updated.Body,
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingleComment([FromRoute] int id)
    {
        try
        {
            IQueryable<Comment> queryForComment = commentRepo.GetMany()
                .Where(c => c.Id == id)
                .Include(c => c.User).AsQueryable();

            CommentDto? dto = await queryForComment.Select(comment =>
                new CommentDto()
                {
                    Id = comment.Id,
                    PostId = comment.PostId,
                    Body = comment.Body,
                    UserId = comment.UserId,
                    UserName = comment.User.Name
                }).FirstOrDefaultAsync();
            /*Comment comment = await commentRepo.GetSingleAsync(id);
            User user = await userRepo.GetSingleAsync(comment.UserId);
            CommentDto dto = new()
            {
                Id = comment.Id,
                PostId = comment.PostId,
                Body = comment.Body,
                UserId = comment.UserId,
                UserName = user.Name,
            };*/
            return dto == null ? NotFound("Comment not found") : Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
             return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IResult> GetMany(
        [FromQuery] int? postId = null, [FromQuery] int? userId = null)
    {
        try
        {
            IList<Comment> comments = await commentRepo.GetMany().Where(c =>
                    postId == null ||
                    c.PostId.Equals(postId) && userId == null || c.UserId.Equals(userId))
                .ToListAsync();
            /*IQueryable<Comment> comments = commentRepo.GetMany(); 
        
            if (!string.IsNullOrEmpty(postId.ToString()))
            {
                comments = comments.Where(c => c.PostId.Equals(postId));
            }
            if (!string.IsNullOrEmpty(userId.ToString()))
            {
                comments = comments.Where(c => c.UserId.Equals(userId));
            }*/
            return Results.Ok(comments);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment([FromRoute] int id)
    {
        try
        {
            await commentRepo.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }   
}