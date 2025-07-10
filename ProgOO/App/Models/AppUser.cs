namespace ProposedProblems.ProgOO.Models;

public class AppUser(Guid id, string name, string password, decimal saldo)
{
    public Guid Id { get; set; } = id;

    public string Name { get; set; } = name;

    // Not safe, use a hashing algorithm in real situations
    public string Password {get; set; } = password;

    public decimal Saldo { get; set; } = saldo;
}