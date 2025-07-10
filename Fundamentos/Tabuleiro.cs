namespace ProposedProblems.Fundamentos;

public static class Tabuleiro
{
    public static void Run()
    {
        int n;
        int m;
        while (true)
        {
            Console.WriteLine("Informe o comprimento do Tabuleiro: ");
            if (int.TryParse(Console.ReadLine(), out n)) break;
            Console.WriteLine("Valor inválido. Tente novamente.");
        }
        
        while (true)
        {
            Console.WriteLine("Informe a altura do Tabuleiro: ");
            if (int.TryParse(Console.ReadLine(), out m)) break;
            Console.WriteLine("Valor inválido. Tente novamente.");
        }

        for (int i = 0; i < m+2; i++)
        {
            for (int j = 0; j < n+2; j++)
            {
                Console.Write($"{(i==0 || j==0 || i == m+1 || j == n+1 ? "# " : "  ")}");
                if(j == n+1) Console.Write("\n");
            }
        }
        
        Thread.Sleep(2000);
    }
}