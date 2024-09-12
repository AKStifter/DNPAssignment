namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public User(string name, string password)
    {
        Name = name;
        Password = password;
    }
    
    public string ToString()
    {
        return $"Id: {Id}, Name:{Name}, Password: {Password}";
    }
}