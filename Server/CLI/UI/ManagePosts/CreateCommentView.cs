using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreateCommentView
{
    private ICommentRepository commentRepo;
    
    public CreateCommentView(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }

    public async Task<Comment> CreateComment(int postId)
    {
        Console.WriteLine("Welcome to comment creation view");
        Console.WriteLine("Please enter the message of the comment");
        string commentBody = Console.ReadLine();
        Console.WriteLine("Please enter your user id");
        string userId = Console.ReadLine();
        Comment createdComment = await commentRepo.AddAsync(new Comment(Int32.Parse(userId), postId, commentBody));
        
        Console.WriteLine("Comment created");
        Console.WriteLine("Your comment id is: " + createdComment.Id);
        
        return createdComment;
    }
}