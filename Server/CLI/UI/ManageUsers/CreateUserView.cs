using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<User> createUser()
    {
        Console.WriteLine("Welcome to user creation menu...");
        Console.WriteLine("Write your desired name: ");
        string username = Console.ReadLine();
        
        while (username.Length < 1)
        {
            Console.WriteLine("Username is required! Try again... ");
            username = Console.ReadLine();
        }
        Console.WriteLine("Write your desired password: ");
        string password = Console.ReadLine();

        while (password.Length < 5)
        {
            Console.WriteLine("Password cannot be shorter than 5 characters! Try again... ");
            password = Console.ReadLine();
        }
        User created =  await userRepository.AddAsync(new User(username, password));
        Console.WriteLine("User created successfully!");
        Console.WriteLine($"Your user id is : {created.Id}");
        return created;
    }
}