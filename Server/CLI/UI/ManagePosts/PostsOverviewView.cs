using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class PostsOverviewView
{
    private IPostRepository postRepo;

    public PostsOverviewView(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    public async Task listPosts()
    {
        Console.WriteLine("Writing the overview of the posts:");
        foreach (Post post in postRepo.GetMany())
        {
            Console.WriteLine("\n" + post.Title + ": ID-" + post.Id);
        }
        
    }
}