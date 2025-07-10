namespace ProposedProblems.Fundamentos;

public static class MaiorOuMenor
{
    public static void Run(int n = 3)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"--=== Maior ou Menor{(n != 3 ? " de N": "")} ===--");
            if (n != 3)
            {
                Console.Write("Digite a quantidade de elementos a comparar: ");
                if (!int.TryParse(Console.ReadLine(), out n))
                {
                    Console.WriteLine("Digite um número válido!");
                    continue;
                }
            }
            var nums = new double[n];
            double maior;
            double menor;
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Digite o {i+1}° número: ");
                if (!double.TryParse(Console.ReadLine(), out nums[i]))
                {
                    Console.WriteLine("Digite um número válido!");
                    i--;
                }
            }
            maior = nums[0];
            menor = nums[0];

            for (int i = 0; i < 3; i++)
            {
                maior = maior > nums[i] ? maior : nums[i];
                menor = menor < nums[i] ? menor : nums[i];
            }

            Console.WriteLine($"O maior número é {maior}");
            Console.WriteLine($"O menor número é {menor}");
            Thread.Sleep(2000);
            break;
        }
    }
}