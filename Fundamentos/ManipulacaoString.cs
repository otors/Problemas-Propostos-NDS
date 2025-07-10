namespace ProposedProblems.Fundamentos;

public static class ManipulacaoString
{
    public static void Run()
    {
        while(true)
        {
            Console.Write("Digite seu nome completo: ");
            var name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Digite um nome válido!");
                continue;
            }

            Console.WriteLine(name.Trim());
            Console.WriteLine(name.Replace(" ", ""));
            Console.WriteLine(name.ToLower());
            Console.WriteLine(name.ToUpper());
            Console.WriteLine(name.Insert(name.Length, "NewWord"));
            Console.WriteLine(name.Replace("era", ""));
            Console.WriteLine(name.Contains("era"));
            Console.WriteLine(name.Replace(" ", "").Length);

        }
        
    }


}