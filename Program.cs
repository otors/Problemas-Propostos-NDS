using System.Runtime.InteropServices.JavaScript;
using ProposedProblems.Fundamentos;
using ProposedProblems.ProgOO.App;

namespace ProposedProblems 
{
    class Program
    
    
    {
        static void Main(string[] args)
        {
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("---===== Proposed Problems =====---");
                Console.WriteLine("Escolha uma Aba: ");
                const string menu = "1. Fundamentos\n2. POO\n0. Sair";
                var options = new List<Action>
                    { () => { exit = true; }, Fundamentos, ProgOO };
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
            
            
            // ParOuImpar.Run();
            // MaiorOuMenor.Run();
            // MaiorOuMenorDeN.Run();
            // ManipulacaoString.Run();
            // Tabuleiro.Run();
            // Fibonacci.Run();
            // WumpusGame.Run();
            // MainApp.Run();
        }

        static void Fundamentos()
        {
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Bem vindo à aba de Fundamentos");
                Console.WriteLine("Projetos disponíveis: ");
                const string menu = """
                                    1. Par ou Ímpar
                                    2. Maior e menor valor
                                    3. Maior e menor valor (Para n valores)
                                    4. Média Aritmética
                                    5. Média Aritmética (Para n valores)
                                    6. Média Ponderada
                                    7. Calculadora simples
                                    8. Manipulação de String
                                    9. Tabuleiro
                                    10. Fibonacci
                                    11. Struct
                                    12. Wumpus Game
                                    13. Projeto RPG
                                    0. Voltar
                                    """;
                var options = new List<Action>
                {
                    () => { exit = true; }, 
                    () => ParOuImpar.Run(),
                    () => MaiorOuMenor.Run(), 
                    () => MaiorOuMenor.Run(), 
                    () => MediaAritmetica.Run(), 
                    MediaAritmeticaDeN, 
                    () => Console.WriteLine("Projeto indisponivel no momento"), 
                    () => Console.WriteLine("Projeto indisponivel no momento"), 
                    () => ManipulacaoString.Run(), 
                    () => Tabuleiro.Run(), 
                    () => Fibonacci.Run(), 
                    () => Console.WriteLine("Projeto indisponivel no momento"), 
                    () => WumpusGame.Run(), 
                    () => Console.WriteLine("Projeto indisponivel no momento")
                };
                Console.WriteLine(menu);
                Console.Write(">>> ");

                if (!int.TryParse(Console.ReadLine(), out var userChoice) || userChoice < 0 || userChoice > options.Count-1)
                {
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    continue;
                }
            
                options[userChoice]();
            }
            Console.Clear();
        }

        private static void MediaAritmeticaDeN()
        {
            while (true)
            {
                Console.Write("Digite a quantidade de elementos a inserir: ");
                if (!int.TryParse(Console.ReadLine(), out var n))
                {
                    Console.WriteLine("Digite um número válido!");
                    continue;
                }

                MediaAritmetica.Run(n);
            }
        }

        static void ProgOO()
        {
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Bem vindo à aba de POO");
                Console.WriteLine("Projetos disponíveis: ");
                const string menu = "1. Página de Login com SysCaixaEletronico\n0. Voltar";
                var options = new List<Action>
                    { () => { exit = true; }, MainApp.Run };
                Console.WriteLine(menu);
                Console.Write(">>> ");

                if (!int.TryParse(Console.ReadLine(), out var userChoice) || userChoice < 0 || userChoice > options.Count-1)
                {
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    continue;
                }
            
                options[userChoice]();
            }
            Console.Clear();
        }
        
    }
    
}