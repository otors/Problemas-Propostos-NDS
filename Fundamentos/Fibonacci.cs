namespace ProposedProblems.Fundamentos;

public static class Fibonacci
{
    public static void Run()
    {
        while (true)
        {
            Console.Write("Digite a quantidade de números de Fibonacci desejados: ");
            if (!int.TryParse(Console.ReadLine(), out var n))
            {
                Console.WriteLine("Digite um número válido!");
                continue;
            }

            var fibo = new List<int>();
            for (var i = 0; i < n; i++) fibo.Add(GetNthFib(i));
            Console.WriteLine(string.Join(", ", fibo));
            break;
        }
    }

    private static int GetNthFib(int n)
    {
        return n switch
        {
            0 => 0,
            1 => 1,
            _ => GetNthFib(n - 1) + GetNthFib(n - 2)
        };
    }
}