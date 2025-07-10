using System.Text.Json;
using ProposedProblems.ProgOO.Models;

namespace ProposedProblems.ProgOO.App.Services;

public static class UserService
{
    private static readonly JsonSerializerOptions JsonWriteOptions = new() { WriteIndented = true  };
    private const string FileName = "appUsers.json";
    public static List<AppUser> GetAllUsers()
    {
        if (File.Exists(Directory.GetCurrentDirectory() + FileName))
        {
            var jsonString = File.ReadAllText(Directory.GetCurrentDirectory() + FileName);
            var appUsers = JsonSerializer.Deserialize<List<AppUser>>(jsonString, JsonWriteOptions);
            return appUsers ?? [];
        }
        SaveUsers([]);
        return [];
    }

    public static AppUser? GetUserById(Guid id)
    {
        var appUsers = GetAllUsers();
        return appUsers.Find((u) => u.Id == id);
    }

    public static void AddUser(AppUser user)
    {
        var appUsers = GetAllUsers();
        if (appUsers.Any(u => u.Name == user.Name)) throw new Exception("Username already exists");
        
        appUsers.Add(user);
        SaveUsers(appUsers);
    }

    public static void DepositToUser(Guid id, decimal amount)
    {
        var appUsers = GetAllUsers();
        var user = GetUserById(id);
        if (user == null) throw new Exception("User not found");
        if(amount < 0) throw new Exception("Invalid amount");
        var updatedUser = new AppUser(id: user.Id, name: user.Name, password: user.Password, saldo:  user.Saldo + amount);
        UpdateUser(updatedUser);
    }

    public static void WithdrawFromUser(Guid id, decimal amount)
    {
        var appUsers = GetAllUsers();
        var user = GetUserById(id);
        if (user == null) throw new Exception("User not found");
        if(amount < 0 || amount > user.Saldo) throw new Exception("Invalid amount");
        var updatedUser = new AppUser(id: user.Id, name: user.Name, password: user.Password, saldo:  user.Saldo - amount);
        UpdateUser(updatedUser);
    }
    
    private static void UpdateUser(AppUser user)
    {
        var appUsers = GetAllUsers();
        appUsers.RemoveAll((u) => u.Id == user.Id);
        appUsers.Add(user);
        SaveUsers(appUsers);
    }

    private static void SaveUsers(List<AppUser> users)
    {
        var jsonString = JsonSerializer.Serialize(users, JsonWriteOptions);
        File.WriteAllText(Directory.GetCurrentDirectory()+FileName, jsonString);
    }
    
    public static void DeleteUser(AppUser user)
    {
        var appUsers = GetAllUsers();
        appUsers.Remove(user);
        SaveUsers(appUsers);
    }
    
    public static void DeleteAllUsers()
    {
        SaveUsers([]);
    }
    
    
    
}