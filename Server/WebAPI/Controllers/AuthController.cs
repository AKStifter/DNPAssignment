using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public AuthController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> CheckUser(
        [FromBody] CreateUserDto request)
    {
        try
        {  
            User? user = userRepo.GetMany().SingleOrDefault(u => u.Name == request.UserName);

            if (user == null)
            {
                return Unauthorized();
            }
            if (!user.Password.Equals(request.Password))
            {
                return Unauthorized();
            }
            UserDto dto = new()
            {
                Id = user.Id,
                UserName = user.Name,
            };
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }
}