using ApiContracts;
using ApiContracts.CommentDtos;
using ApiContracts.PostDtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class PostsController : ControllerBase
{
 
    private  readonly IPostRepository postRepo;

    private  readonly IUserRepository userRepo;

    private readonly ICommentRepository commentRepo;
    
    public PostsController(IPostRepository postRepo, IUserRepository userRepo, ICommentRepository commentRepo)
    {
        this.postRepo = postRepo;
        this.userRepo = userRepo;
        this.commentRepo = commentRepo;
    }
    
    [HttpPost]
    public async Task<ActionResult<PostDto>> AddPost(
        [FromBody] CreatePostDto request)
    {
        try
        {
            Post post = new(request.Title, request.Body, request.UserId);
            Post created = await postRepo.AddAsync(post);
            PostDto dto = new()
            {
                Id = created.Id,
                Title = created.Title,
                Body = created.Body,
                UserId = created.UserId
                
            };
            return Created($"/Posts/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> UpdatePost([FromRoute] int id,
        [FromBody] UpdatePostDto postInfo)
    {
        try
        {
            Post post = new Post(id, postInfo.Title, postInfo.Body, postInfo.UserId);
            
            await postRepo.UpdateAsync(post);
            Post updated = await postRepo.GetSingleAsync(post.Id);
            UpdatePostDto dto = new()
            {
                Title = updated.Title,
                Body = updated.Body,
                UserId = updated.UserId
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
    public async Task<ActionResult<GetPostDto>> GetSinglePost([FromRoute] int id)
    {
        try
        {
            Post post = await postRepo.GetSingleAsync(id);
            User user = await userRepo.GetSingleAsync(post.UserId);
            IQueryable<Comment> comments =  commentRepo.GetMany();
            comments = comments.Where(c => c.UserId== user.Id);
            GetPostDto dto = new()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId,
                UserName = user.Name,
                Comments = comments
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
             return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IResult> GetMany(
        [FromQuery] string? nameContains, [FromQuery] int? userId)
    {
        try
        {
            IQueryable<Post> posts = postRepo.GetMany(); 
        
            if (!string.IsNullOrEmpty(nameContains))
            {
                posts = posts.Where(u => u.Title.ToLower().Contains(nameContains.ToLower()));
            }
            if (!string.IsNullOrEmpty(userId.ToString()))
            {
                posts = posts.Where(u => u.UserId.Equals(userId));
            }
            return Results.Ok(posts);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost([FromRoute] int id)
    {
        try
        {
            await postRepo.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("{postId}/comments")]
    public async Task<ActionResult<CommentDto>> AddComment([FromQuery] int postId,
        [FromBody] CreateCommentDto request)
    {
        try
        {
            Comment comment = new(request.UserId, postId, request.Body);
            Comment created = await commentRepo.AddAsync(comment);
            User user = await userRepo.GetSingleAsync(created.UserId);
            CommentDto dto = new()
            {
                Id = created.Id,
                UserId = created.UserId,
                PostId = created.PostId,                
                Body = created.Body,
                UserName = user.Name
                
            };
            return Created($"/Posts/{dto.PostId}/Comments", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}