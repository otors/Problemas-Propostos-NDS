namespace ProposedProblems.Fundamentos;

public static class MediaAritmetica
{
    public static void Run(int n = 2)
    {
        while (true)
        {
            var nums = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Digite o {i+1}° número: ");
                if (!int.TryParse(Console.ReadLine(), out nums[i]))
                {
                    Console.WriteLine("Digite um número válido!");
                    i--;
                }

                Console.Write("A média entre ");
                var sum = 0;
                foreach (var num in nums)
                {
                    sum += num;
                    Console.Write($"{num}, ");
                }
                Console.Write($"é {sum/n}");
            }
            
            
            
        }
    }
}