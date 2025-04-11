class InputFileReader
{
    public static (int R, int N, int ID, int M, string[] Events) ReadInputFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            
            int r = int.Parse(lines[0]);
            int n = int.Parse(lines[1]);
            int id = int.Parse(lines[2]);
            int m = int.Parse(lines[3]);
            string[] events = lines[4].Split(' ');
    
            string validationError = InputHandler.ValidateInput(r, n, id, m);
            if (!string.IsNullOrEmpty(validationError))
            {
                Console.WriteLine($"Validation error:{validationError}");
                return (0, 0, 0, 0, null);
            }

            return (r, n, id, m, events);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return (0, 0, 0, 0, null);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Read from test file
        var (R, N, ID, M, Events) = InputFileReader.ReadInputFile("test1.txt");
        
        // Create hero instance
        Hero hero = new Hero(ID, M);

        // Process each event
        foreach (string eventCode in Events)
        {
            Console.WriteLine($"Processing event: {eventCode}");
            ProcessEvent(hero, eventCode);
            DisplayStatus(hero);
        }
    }

    static void ProcessEvent(Hero hero, string eventCode)
    {
        int eventType = int.Parse(eventCode[0].ToString());
        switch (eventType)
        {
            case 1: // Green meteor
                hero.GreenMeteors++;
                Console.WriteLine("Found a green meteor!");
                break;
            case 2: // Red meteor
                hero.RedMeteors++;
                Console.WriteLine("Found a red meteor!");
                break;
            case 3: // Asclepius temple
                hero.HP += 50;
                hero.Drachma -= 40;
                Console.WriteLine("Visited Asclepius temple: +50 HP, -40 Drachma");
                break;
            case 4: // Ares monster
                hero.HP -= 50;
                Console.WriteLine("Encountered Ares monster: -50 HP");
                break;
            case 5: // Athena
                hero.HP += 30;
                Console.WriteLine("Met Athena: +30 HP");
                break;
            case 6: // Zeus shield
                hero.HP += 20;
                Console.WriteLine("Found Zeus shield: +20 HP");
                break;
            case 7: // Titan
                hero.HP -= 100;
                hero.TitanDefeated++;
                Console.WriteLine($"Fought Titan: -100 HP, Titans defeated: {hero.TitanDefeated}");
                break;
            case 8: // Hera
                hero.Drachma += 50;
                Console.WriteLine("Met Hera: +50 Drachma");
                break;
            case 9: // Hermes
                hero.Drachma *= 2;
                Console.WriteLine($"Met Hermes: Drachma doubled to {hero.Drachma}");
                break;
        }
    }

    static void DisplayStatus(Hero hero)
    {
        Console.WriteLine($"HP: {hero.HP}");
        Console.WriteLine($"Drachma: {hero.Drachma}");
        Console.WriteLine($"Green Meteors: {hero.GreenMeteors}");
        Console.WriteLine("-------------------");
    }
}