using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";
    
         public PostFileRepository()
         {
         if (!File.Exists(filePath))
             {
             File.WriteAllText(filePath, "[]");
             }
         }

         public async Task<Post> AddAsync(Post post)
         {
             string postsAsJson = await File.ReadAllTextAsync(filePath);

             List<Post> posts =
                 JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

             int maxId = posts.Count > 0 ? posts.Max(p => p.Id) : 0;

             post.Id = maxId + 1;

             posts.Add(post);

             postsAsJson = JsonSerializer.Serialize(posts);

             await File.WriteAllTextAsync(filePath, postsAsJson);

             return post;
         }

         public async Task UpdateAsync(Post post)
    {
        
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        
        postsAsJson = JsonSerializer.Serialize(posts);

        await File.WriteAllTextAsync(filePath, postsAsJson);
        
        return ;
    }

    public async Task DeleteAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!; 
        
        Post? PostToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (PostToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        posts.Remove(PostToRemove);
        
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
        return ;
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        
        Post? PostToReturn = posts.SingleOrDefault(p => p.Id == id);
        if (PostToReturn is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        return PostToReturn;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        
        return posts.AsQueryable();
        
    }
}