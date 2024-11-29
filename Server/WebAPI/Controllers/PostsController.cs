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
            //When trying to get the data with a single query I get the following error
            //Translating this query requires the SQL APPLY operation, which is not supported on SQLite.
            /*IQueryable<Post> queryForPost =
                postRepo.GetMany().Where(p => p.Id == id).AsQueryable();
            queryForPost = queryForPost.Include(p => p.User);
            queryForPost = queryForPost.Include(p => p.Comments);*/
            Post post = await postRepo.GetSingleAsync(id);
            User user = await userRepo.GetSingleAsync(post.UserId);
            IQueryable<Comment> comments =  commentRepo.GetMany();
            List<Comment> lComments = new List<Comment>();
            List<CommentDto> commentDtos = new List<CommentDto>();
            comments = comments.Where(c => c.PostId == post.Id);
            
            foreach (Comment c in comments)
            {
                User commenter = await userRepo.GetSingleAsync(c.UserId);
                CommentDto commentDto = new()
                {
                    
                    Id = c.Id,
                    UserId = c.UserId,
                    Body = c.Body,
                    PostId = c.PostId,
                    UserName = commenter.Name
                };   
                commentDtos.Add(commentDto);
            }
            GetPostDto dto = new()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId,
                UserName = user.Name,
                Comments = commentDtos
            };
           /*GetPostDto? dto = await queryForPost.Select(post => new GetPostDto()
           {
               Id = post.Id,
               Title = post.Title,
               Body = post.Body,
               UserId = post.UserId,
               UserName = post.User.Name,
               Comments = post.Comments.Select(c => new CommentDto
               {
                   Id = c.Id,
                   UserId = c.UserId,
                   Body = c.Body,
                   PostId = post.Id,
                   UserName = c.User.Name,
               }).ToList() 
           }) .FirstOrDefaultAsync();*/
           
           return dto == null ? NotFound() : Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
             return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IResult> GetMany(
        [FromQuery] string? nameContains = null, [FromQuery] int? userId = null)
    {
        try
        {
            IQueryable<Post> queryForPosts = postRepo.GetMany().Where(p =>
                nameContains == null ||
                p.Title.ToLower().Contains(nameContains.ToLower()) &&
                userId == null || p.UserId.Equals(userId)).AsQueryable();
            
            queryForPosts.Include(p => p.User);
            
            List<ManyPostDto>? dtos = await queryForPosts.Select(post =>
                new ManyPostDto()
                {
                    Id = post.Id,
                    Title = post.Title,
                    UserName = post.User.Name,
                }).ToListAsync();
            /*IList<Post> posts = await postRepo.GetMany().Where(p =>
                    nameContains == null ||
                    p.Title.ToLower().Contains(nameContains.ToLower()) && userId == null || p.UserId.Equals(userId))
                .ToListAsync();
            //IQueryable<Post> posts = postRepo.GetMany();
            List<ManyPostDto> dtos = new List<ManyPostDto>();

        
           /* if (!string.IsNullOrEmpty(nameContains))
            {
                posts = posts.Where(u => u.Title.ToLower().Contains(nameContains.ToLower()));
            }
            if (!string.IsNullOrEmpty(userId.ToString()))
            {
                posts = posts.Where(u => u.UserId.Equals(userId));
            }*/

            /*Console.WriteLine(posts);
            foreach (Post post in posts)
            {
                ManyPostDto dto = new()
                {
                    Id = post.Id,
                    Title = post.Title,
                    UserName = userRepo.GetSingleAsync(post.UserId).Result.Name,
                };
                dtos.Add(dto);
            }*/
            return Results.Ok(dtos);

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
    public async Task<ActionResult<CommentDto>> AddComment([FromRoute] int postId,
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
            return Created($"/Posts/{dto.PostId}/Comments", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}