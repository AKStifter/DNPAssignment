namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    //public List<Comment> Comments { get; set; }

    public Post()
    {
    }

    public Post(string title, string body, int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
       // Comments = new List<Comment>();
    }
    
    public Post(int id, string title, string body, int userId)
    {
        Id = id;
        Title = title;
        Body = body;
        UserId = userId;
    }
    
   /* public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }
    public void AddComment(int userId, string body, int postId)
    {
        Comments.Add(new Comment(userId, postId, body));
    }
    */
    public string ToString()
    {
        return $"Id: {Id}, \nTitle:{Title}, \nBody: {Body}, \nUserId: {UserId}";
    }
}