using ApiContracts;
using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }


    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser(
        [FromBody] CreateUserDto request)
    {
        try
        {
            User user = new(request.UserName, request.Password);
            User created = await userRepo.AddAsync(user);
            UserDto dto = new()
            {
                Id = created.Id,
                UserName = created.Name
            };
            return Created("", dto);
            //return Created($"/Users/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id,
        [FromBody] UpdateUserDto userInfo)
    {
        try
        {
            User user = new User(id, userInfo.UserName, userInfo.Password);
            
            await userRepo.UpdateAsync(user);
            User updated = await userRepo.GetSingleAsync(user.Id);
            UpdateUserDto dto = new()
            {
                UserName = updated.Name,
                Password = updated.Password
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
    public async Task<ActionResult<User>> GetSingleUser([FromRoute] int id)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(id);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
             return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IResult> GetMany(
        [FromQuery] string? nameContains = null)
    {
        try
        {
            IList<User> users = await userRepo.GetMany().Where(u =>
                    nameContains == null ||
                    u.Name.ToLower().Contains(nameContains.ToLower()))
                .ToListAsync();
           /* IQueryable<User> users = userRepo.GetMany(); 
        
            if (!string.IsNullOrEmpty(nameContains))
            {
                users = users.Where(u => u.Name.ToLower().Contains(nameContains.ToLower()));
            }*/
            return Results.Ok(users);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            await userRepo.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}