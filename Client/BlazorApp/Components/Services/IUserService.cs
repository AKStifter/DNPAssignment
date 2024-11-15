using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Components.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task<ActionResult<UserDto>>UpdateUser(int id, UpdateUserDto request);
    public  Task<ActionResult<User>> GetSingleUser(int id);
    public Task<IResult> GetMany(string? nameContains);
    public Task<ActionResult> DeleteUser(int id);
}