using ProposedProblems.ProgOO.Models;

namespace ProposedProblems.ProgOO.App.Services;

public static class AuthService
{
    public static bool Login(string username, string password, out AppUser? loggedUser)
    {
        var userList = UserService.GetAllUsers();
        
        var foundUser = false;
        loggedUser = null;
        if (userList.Count != 0)
        {
            foundUser = userList.Any(u => u.Name == username && u.Password == password);
            if (foundUser)
            {
                loggedUser = userList.First(u => u.Name == username && u.Password == password);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
        else
        {
            throw new Exception("No users found");
        }
        return foundUser;

    }

    public static void Register(string username, string password)
    {
        var user = new AppUser(Guid.NewGuid(), username, password, 0);
        UserService.AddUser(user);
    }
}