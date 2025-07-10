using ProposedProblems.ProgOO.App.Controllers;

namespace ProposedProblems.ProgOO.Screens;

public static class HomePage
{
    public static void Build(AccountController accountController)
    {
        while (true)
        {
            if (accountController.User == null) return;
            Console.Clear();
            Console.WriteLine($"--=== Bem vindo, {accountController.User.Name}! ===--");
            const string menu = "1. Depositar\n2. Sacar\n0. Logout";
            var options = new List<Action>
                { accountController.Logout, accountController.Deposit, accountController.Withdraw };

            Console.WriteLine($">> Saldo: R${accountController.User.Saldo:F2} <<");
            Console.WriteLine(menu);
            Console.Write(">>> ");

            if (!int.TryParse(Console.ReadLine(), out int userChoice) || userChoice < 0 || userChoice > 2)
            {
                Console.WriteLine("Opção inválida! Tente novamente.");
                Thread.Sleep(1500);
                continue;
            }

            options[userChoice]();
        }
    }
    
}