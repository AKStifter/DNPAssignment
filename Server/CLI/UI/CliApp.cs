using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    private DummyData dummyData;
    
    private CreateUserView createUserView { get; set; }
    private CreatePostView createPostView { get; set; }
    private PostsOverviewView postsOverviewView { get; set; }
    private ViewPostView viewPostView { get; set; }
    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        createUserView = new CreateUserView(this.userRepository);
        createPostView = new CreatePostView(this.postRepository);
        postsOverviewView = new PostsOverviewView(this.postRepository);
        viewPostView = new ViewPostView(this.postRepository, this.commentRepository);
        dummyData = new DummyData(userRepository, postRepository, commentRepository);
    }
    
   public async Task StartAsync()
   {
       dummyData.createDummyData();
       bool exit = false;
        Console.WriteLine("Welcome to our forum app");

        while (!exit)
        {
            Console.WriteLine("Please enter the number corresponding to the following actions:");
            Console.WriteLine("1. Create a new user");
            Console.WriteLine("2. Create a new post");
            Console.WriteLine("3. Get an overview of all posts");
            Console.WriteLine("4. View specific post");
            Console.WriteLine("5. Exit the program");

            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    await createUserView.createUser();
                    break;
                case "2":
                    await createPostView.CreatePost();
                    break;
                case "3":
                    await postsOverviewView.listPosts();
                    break;
                case "4":
                    await viewPostView.ViewPost();
                    break;
                case "5":
                    exit = true;
                    break;
            }
        }
   }
}