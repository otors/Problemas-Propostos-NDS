namespace ProposedProblems.Fundamentos;

public static class ParOuImpar
{
    public static void Run()
    {
        
        while(true)
        {
            Console.Clear();
            Console.WriteLine("--=== Par Ou Impar ===--");
            Console.Write("Digite um número: ");
            
            if (!int.TryParse(Console.ReadLine(), out int num))
            {
                Console.WriteLine("Digite um número inteiro válido!");
                continue;
            }

            Console.WriteLine($"O número é {(num % 2 == 0 ? "par":"ímpar")}!");
            Thread.Sleep(1000);
            break;
        }
        Console.Clear();
    }
}