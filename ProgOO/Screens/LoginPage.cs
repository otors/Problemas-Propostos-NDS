using ProposedProblems.ProgOO.App.Controllers;
using ProposedProblems.ProgOO.Models;

namespace ProposedProblems.ProgOO.Screens;

public static class LoginPage
{
    public static void Build()
    {
        var accountController = new AccountController();
        var exit = false;
        while (!exit)
        {
            if(accountController.User != null) {HomePage.Build(accountController); continue;}
            Console.Clear();
            Console.WriteLine("--=== Bem vindo ao SysCaixaEletronico! ===--");
            Console.WriteLine("> Efetue seu login ou cadastre-se! <");
            const string menu = "1. Login\n2. Signin\n0. Exit";
            var options = new List<Action>
                { () => { exit = true; }, accountController.Login, accountController.Register };
            Console.WriteLine(menu);
            Console.Write(">>> ");

            if (!int.TryParse(Console.ReadLine(), out var userChoice) || userChoice < 0 || userChoice > 2)
            {
                Console.WriteLine("Opção inválida! Tente novamente.");
                continue;
            }
            
            options[userChoice]();
        }
        Console.Clear();
    }

   
}