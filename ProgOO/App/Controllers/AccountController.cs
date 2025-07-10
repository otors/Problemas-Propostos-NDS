using ProposedProblems.ProgOO.App.Services;
using ProposedProblems.ProgOO.Models;

namespace ProposedProblems.ProgOO.App.Controllers;

public class AccountController
{
    public AppUser? User = null;

    public void Login()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---==== Login ====---");
            
            Console.Write("Nome de Usuário: ");
            var username = Console.ReadLine();
            
            Console.Write("Senha: ");
            var password = Console.ReadLine();
            
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("<<< Preencha todos os campos! >>>");
                continue;
            }

            try
            {
                var success = AuthService.Login(username.Trim(), password.Trim(), out var loggedUser);

                if (!success) continue;
                User = loggedUser;
                Console.WriteLine(">> Logado com sucesso! <<");
                Thread.Sleep(1000);
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine("<<< Nome de usuário ou senha incorretos >>>");
                Console.WriteLine("<<< Tente Novamente >>>");
                Thread.Sleep(1500);
            }
        }

    }

    public void Register()
    {
        var exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("---==== Signin ====---");

            Console.Write("Nome de usuário: ");
            var username = Console.ReadLine();

            Console.Write("Senha: ");
            var password = Console.ReadLine();

            Console.Write("Confirmar senha: ");
            var confirmPassword = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                Console.WriteLine("<<< Preencha todos os campos! >>>");
                continue;
            }
            

            if (confirmPassword != password)
            {
                Console.WriteLine("<<< Campos de senha e confirmar senha precisam ser iguais >>>");
                Console.WriteLine("<<< Tente novamente >>>");
                Thread.Sleep(2000);
                continue;
            }

            Console.WriteLine("Criar usuário? (y/n)");
            Console.WriteLine(">> ");
            var confirmation = Console.ReadLine();
            if (confirmation is "y" or "Y" or "yes")
            {
                try
                {
                    AuthService.Register(username, password);
                    exit = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n"+e.Message);
                    Console.WriteLine("Tente novamente");
                    Thread.Sleep(2000);
                }
            } else
                exit = true;
            
        }
    }

    public void Logout()
    {
        User = null;
        Console.WriteLine("Logging out...");
        Thread.Sleep(2000);
        Console.Clear();
    }

    public void Deposit()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---==== Depositar ====---");
            Console.WriteLine($">> Saldo: R${User!.Saldo:F2} <<");
            
            Console.Write("Valor: ");
            var value = Console.ReadLine();

            Console.Write("Confirmar valor: ");
            var confirmValue = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(confirmValue))
            {
                Console.WriteLine("<<< Preencha todos os campos! >>>");
                continue;
            }

            if (!decimal.TryParse(value, out var amount) || !decimal.TryParse(confirmValue, out var confirmAmount))
            {
                Console.WriteLine("<<< Valor fornecido inválido >>>");
                Console.WriteLine("<<< Tente novamente >>>");
                Thread.Sleep(1500);
                continue;
            }
            
            if (value != confirmValue)
            {
                Console.WriteLine("<<< Valores informados diferentes >>>");
                Console.WriteLine("<<< Tente novamente >>>");
                Thread.Sleep(2000);
                continue;
            }
            
            try
            { 
                UserService.DepositToUser(User.Id, amount);
                Console.WriteLine(">> Deposito efetuado com sucesso! <<");
                Thread.Sleep(1000);
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine("<<< Valor inválido >>>");
                Console.WriteLine("<<< Tente novamente >>>");
                Thread.Sleep(1500);
            }
        }
        RefreshUser();
    }

    public void Withdraw()
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("---==== Sacar ====---");
            Console.WriteLine($">> Saldo: R${User!.Saldo:F2} <<");

            Console.Write("Valor: ");
            var value = Console.ReadLine();

            Console.Write("Confirmar valor: ");
            var confirmValue = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(confirmValue))
            {
                Console.WriteLine("<<< Preencha todos os campos! >>>");
                continue;
            }

            if (!decimal.TryParse(value, out var amount) || !decimal.TryParse(confirmValue, out var confirmAmount))
            {
                Console.WriteLine("<<< Valor fornecido inválido >>>");
                Console.WriteLine("<<< Tente novamente >>>");
                Thread.Sleep(1500);
                continue;
            }
            
            if (value != confirmValue)
            {
                Console.WriteLine("<<< Valores informados diferentes >>>");
                Console.WriteLine("<<< Tente novamente >>>");
                Thread.Sleep(2000);
                continue;
            }

            try
            {
                UserService.WithdrawFromUser(User.Id, amount);
                Console.WriteLine(">> Saque efetuado com sucesso! <<");
                Thread.Sleep(1000);
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine("<<< Valor inválido >>>");
                Console.WriteLine("<<< Tente novamente >>>");
                Thread.Sleep(1500);
            }
        }
        RefreshUser();
    }

    public void RefreshUser()
    {
        User = UserService.GetUserById(User!.Id);
    }
}