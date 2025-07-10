namespace ProposedProblems.Fundamentos;

public static class WumpusGame
{
    private static int _points = 1000;
    private class Entity(string id, int coordX, int coordY)
    {
        public string Id { get; private set; } = id;
        public int CoordX { get; private set; } = coordX;
        public int CoordY { get; private set;} = coordY;

        public static Entity GetRandomPosEntity(string id, (int width, int height) boardSize)
        {
            var random = new Random();
            return new Entity(id,random.Next(boardSize.width), random.Next(boardSize.height));
        }
        
        public (int x, int y) Pos() => (CoordX, CoordY);

        public bool Move((int x, int y) newPos)
        {
            if (newPos.x == CoordX && newPos.y == CoordY) return false;
            
            CoordX = newPos.x;
            CoordY = newPos.y;
            return true;
        }
        
    }

    public static void Run()
    {
        while (true)
        {
            var boardSize = GetBoardSize();

            var cleanBoard = GetBoard(boardSize);
            Entity player;
            Entity monster;
            Entity gold;

            do
            {
                player = Entity.GetRandomPosEntity("P", boardSize);
                monster = Entity.GetRandomPosEntity("W", boardSize);
                gold = Entity.GetRandomPosEntity("G", boardSize);
            } while (HasDuplicatedPos([player, monster, gold]));

            List<Entity> entities = [player, monster, gold];

            Console.WriteLine(player.Pos().ToString());
            Console.WriteLine(monster.Pos().ToString());
            Console.WriteLine(gold.Pos().ToString());


            DrawEntities(cleanBoard, entities, out var board);
            PrintBoard(board);

            bool? status;
            do
            {
                board = UpdateBoard(cleanBoard, entities, out entities);
                PrintBoard(board);
                status = CheckWinOrLose(entities);
            } while (!status.HasValue);

            Console.WriteLine();
            Console.WriteLine((status.Value ? "YOU WIN!" : "Game Over"));

            Console.Write("Pressione Q para sair ou R para jogar denovo...");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Q:
                    return;
                case ConsoleKey.R:
                    Thread.Sleep(500);
                    continue;
            }

            break;
        }
    }


    private static (int width, int height) GetBoardSize()
    {
        var width = 0;
        var height = 0;

        while (true)
        {
            Console.Clear();
            for (var i = 0; i < 2; i++)
            {
                Console.Write($"Informe a {(i == 0 ? "largura" : "altura")} do Tabuleiro: ");
                if (!int.TryParse(Console.ReadLine(), out var size))
                {
                    Console.WriteLine("Digite um valor válido!");
                    i--;
                    continue;
                }
                if (size < 5)
                {
                    Console.WriteLine("Tamanho inválido. Mínimo de 5.");
                    i--;
                    continue;
                }

                if (i == 0)
                    width = size;
                else
                    height = size;
            }
            break;
        }
        
        return (width, height);
    }

    private static List<List<string>> GetBoard((int width, int height) boardSize)
    {
        var board = new List<List<string>>();
        for (var i = 0; i < boardSize.height+2; i++)
        {
            var line = new List<string>();
            for (var j = 0; j < boardSize.width+2; j++)
            {
                line.Add($"{(i==0 || j==0 || i == boardSize.height+1 || j == boardSize.width+1 ? "# " : "  ")}");
                if(j == boardSize.width+1) line[j] += ("\n");
            }
            board.Add(line);
        }

        return board;
    }
    
    private static void DrawEntities(List<List<string>> cleanBoard, List<Entity> entities, out List<List<string>> board)
    {
        board = Copy2DStringList(cleanBoard);
        
        for (var i = 0; i < cleanBoard.Count-2; i++)
        {
            for (var j = 0; j < cleanBoard[i].Count-2; j++)
            {
                foreach (var e in entities)
                {
                    if (e.Pos() == (j, i)) board[i + 1][j + 1] = e.Id+" ";
                }
            }
        }
    }

    private static List<List<string>> UpdateBoard(List<List<string>> cleanBoard, List<Entity> entities, out List<Entity> newEntities)
    {
        
        // Handle Player Movement
        var player =  entities.Find((e) => e.Id == "P");
        (int x, int y) newPlayerCoords;
        Console.WriteLine("-== Use as setas para se mover ==-");
        do
        {
            newPlayerCoords = GetPlayerMovement(entities);
        } while (newPlayerCoords.x < 0 || newPlayerCoords.y < 0 || newPlayerCoords.x >= cleanBoard[0].Count - 2 || newPlayerCoords.y >= cleanBoard.Count - 2);
        
        player.Move(newPlayerCoords);
        _points--;
        newEntities = entities.FindAll((e) => e.Id != "P");
        newEntities.Add(player);
        
        // Handle Monster Movement
        var monster =  entities.Find((e) => e.Id == "W");
        var goldPos = entities.Find((e) => e.Id == "G").Pos();
        (int x, int y) newMonsterCoords;
        do
        {
            newMonsterCoords = GetMonsterRandomMovement(newEntities);
        } while (newMonsterCoords.x < 0 || newMonsterCoords.y < 0 || newMonsterCoords.x >= cleanBoard[0].Count - 2 || newMonsterCoords.y >= cleanBoard.Count - 2 || newMonsterCoords == goldPos);
        
        monster.Move(newMonsterCoords);
        newEntities = newEntities.FindAll((e) => e.Id != "W");
        newEntities.Add(monster);
        
        entities = newEntities;
        
        DrawEntities(cleanBoard, entities, out var newBoard);
        return newBoard;
    }

    private static void PrintBoard(List<List<string>> board)
    {
        Console.Clear();
        Console.WriteLine($"Points: {_points}");
        // board.ForEach((s) => Console.Write(string.Join("", s)));
        foreach(var s in board)
            Console.Write(string.Join("", s));
    }

    private static (int, int) GetMonsterRandomMovement(List<Entity> entities)
    {
        var random = new Random();
        var chance = random.Next(10);
        var player =  entities.Find((e) => e.Id == "P");
        var monster =  entities.Find((e) => e.Id == "W");

        if (chance == 0 ) return (monster.CoordX, monster.CoordY);
        
        var priorityAxis = random.Next(2);
        var flag = Convert.ToBoolean(priorityAxis);

        while(true)
        {
            var (p, m) = flag ? (player.CoordX, monster.CoordX) : (player.CoordY, monster.CoordY);
            var adderX = Convert.ToInt32(flag);
            var adderY = Convert.ToInt32(!flag);
            
            (int x, int y) newMCoords;
            if (m < p)
            {
                newMCoords = (monster.CoordX + adderX, monster.CoordY + adderY);
                if(newMCoords is { x: >= 0, y: >= 0 }) return newMCoords;
            }
            if (m > p)
            {
                newMCoords = (monster.CoordX - adderX, monster.CoordY - adderY);
                if(newMCoords is { x: >= 0, y: >= 0 }) return newMCoords;
            }
            
            flag = !flag;
        }
    }

    private static (int, int) GetPlayerMovement(List<Entity> entities)
    {
        var player =  entities.Find((e) => e.Id == "P");
        
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    return (player.CoordX, player.CoordY - 1);
                case ConsoleKey.DownArrow:
                    return (player.CoordX, player.CoordY + 1);
                case ConsoleKey.LeftArrow:
                    return  (player.CoordX - 1, player.CoordY);
                case ConsoleKey.RightArrow:
                    return  (player.CoordX + 1, player.CoordY);
            }
        }
    }

    private static bool? CheckWinOrLose(List<Entity> entities)
    {
        var player = entities.Find((e) => e.Id == "P");
        var monster = entities.Find((e) => e.Id == "W");
        var gold = entities.Find((e) => e.Id == "G");

        if (player.Pos() == monster.Pos())
            return false;
        
        if (player.Pos() == gold.Pos())
            return true;
        
        return null;
    }
    
    
    
    
    #region Helpers

    private static bool HasDuplicatedPos(List<Entity> entities)
    {
        var pos = new HashSet<(int, int)>();
        foreach (var entity in entities)
            pos.Add(entity.Pos());
        return pos.Count != entities.Count;
    }

    private static List<List<string>> Copy2DStringList(List<List<string>> list)
    {
        var newList = new List<List<string>>(list.Count);

        foreach (var col in list)
        {
            var line = new List<string>();
            foreach (var e in col)
            {
                line.Add($"{e}");
            }
            newList.Add(line);
        }
        return newList;
    }
    
    #endregion

    
}