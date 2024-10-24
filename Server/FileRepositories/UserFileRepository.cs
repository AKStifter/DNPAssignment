using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";
    
    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;

        user.Id = maxId + 1;

        users.Add(user);

        usersAsJson = JsonSerializer.Serialize(users);

        await File.WriteAllTextAsync(filePath, usersAsJson);

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        
        usersAsJson = JsonSerializer.Serialize(users);

        await File.WriteAllTextAsync(filePath, usersAsJson);
        return;
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return;
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        User? userToReturn = users.SingleOrDefault(u => u.Id == id);
        if (userToReturn is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return userToReturn;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;

        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        return users.AsQueryable();
    }
}