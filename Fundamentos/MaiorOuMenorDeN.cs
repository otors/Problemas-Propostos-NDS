namespace ProposedProblems.Fundamentos;

public static class MaiorOuMenorDeN
{
    public static void Run()
    {
        while (true)
        {
            
            Console.Write("Digite a quantidade de elementos a comparar: ");
            if (!int.TryParse(Console.ReadLine(), out var n))
            {
                Console.WriteLine("Digite um número válido!");
                continue;
            }
            
            var nums = new double[n];

            for (var i = 0; i < n; i++)
            {
                Console.Write($"Digite o {i+1}° número: ");
                if (double.TryParse(Console.ReadLine(), out nums[i])) continue;
                Console.WriteLine("Digite um número válido!");
                i--;
            }
            var maior = nums[0];
            var menor = nums[0];

            for (var i = 0; i < n; i++)
            {
                maior = maior > nums[i] ? maior : nums[i];
                menor = menor < nums[i] ? menor : nums[i];
            }

            Console.WriteLine($"O maior número é {maior}");
            Console.WriteLine($"O menor número é {menor}");
            break;
        }
    }
}