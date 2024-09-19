using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ViewPostView
{
    private IPostRepository postRepository;
    private ICommentRepository commentRepository;

    private CreateCommentView createCommentView;
    
    public ViewPostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        createCommentView = new CreateCommentView(this.commentRepository);
    }

    public async Task ViewPost()
    {
        Console.WriteLine("Write the id of the post you wish to view");
        bool exit = false;
        while (!exit)
        {
            try
            {
                string id = Console.ReadLine();
                
                Post post = await postRepository.GetSingleAsync(Int32.Parse(id));
                Console.WriteLine(post.ToString());
                
                Console.WriteLine("\n Post comments: \n");

                foreach (Comment comment in commentRepository.GetMany())
                {
                    if (comment.PostId == post.Id)
                    {
                        Console.WriteLine("User " + comment.UserId + " wrote: \n" + comment.Body);
                    }
                }
                
                Console.WriteLine("Do you want to comment? (y/n) ");
                string response = Console.ReadLine();
                if (response.ToLower() == "y")
                {
                   await createCommentView.CreateComment(post.Id);
                }
                exit = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try again? (y/n) ");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "n") { exit = true; }
            }
        }
    }
}