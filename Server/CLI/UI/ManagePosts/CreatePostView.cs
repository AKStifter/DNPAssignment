using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private IPostRepository postRepo;

    public CreatePostView(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    public async Task<Post> CreatePost()
    {
        Console.WriteLine("Welcome to the post creation menu");
        Console.WriteLine("Write the desired post title: ");
        string title = Console.ReadLine();

        while (title.Length < 1)
        {
            Console.WriteLine("The title is required! Try again... ");
            title = Console.ReadLine();
        }

        Console.WriteLine("Write your desired message: ");
        string message = Console.ReadLine();

        while (message.Length < 1)
        {
            Console.WriteLine("The message is required! Try again... ");
            message = Console.ReadLine();
        }

        Console.WriteLine("Write your user id");

        string userId = Console.ReadLine();

        Post createdPost =
            await postRepo.AddAsync(new Post(title, message,
                Int32.Parse(userId)));
        Console.WriteLine("Post created successfully!");
        Console.WriteLine($"Your post id is {createdPost.Id}");
        return createdPost;
    }
}