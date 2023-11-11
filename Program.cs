using System.Text.Json;

class Program
{
    static List<GuestbookPost> guestbook = new List<GuestbookPost>();
    static string jsonFilePath = "guestBook.json"; // Filnamnet för JSON-filen

    static void Main()
    {
        LoadGuestbook(); // Läs in tidigare inlägg från JSON-filen

        bool programRuns = true;

        while (programRuns)
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Björns gästbok!");
            Console.WriteLine("1. Läs alla inlägg");
            Console.WriteLine("2. Skriv ett nytt inlägg");
            Console.WriteLine("3. Radera ett inlägg");
            Console.WriteLine("4. Avsluta");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    ReadGuestbook();
                    break;
                case 2:
                    WriteToGuestbook();
                    break;
                case 3:
                    DeleteGuestbookPost();
                    break;
                case 4:
                    SaveGuestbook(); // Spara inläggen till JSON-filen innan du avslutar
                    programRuns = false;
                    break;
                default:
                    break;
            }
        }
    }

    static void LoadGuestbook()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);

            if (!string.IsNullOrWhiteSpace(json))
            {
                guestbook = JsonSerializer.Deserialize<List<GuestbookPost>>(json);
            }
            else
            {
                Console.WriteLine("JSON-filen är tom eller ogiltig.");
            }
        }
    }


    static void SaveGuestbook()
    {
        string json = JsonSerializer.Serialize(guestbook);
        File.WriteAllText(jsonFilePath, json);
    }

    // Funktioner för gästboken

    static void ReadGuestbook()
    {
        Console.Clear();
        Console.WriteLine("Inlägg i gästboken:");
        if (guestbook.Count == 0)
        {
            Console.WriteLine("\nGästboken är tom.");
        }
        else
        {
            for (int i = 0; i < guestbook.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {guestbook[i].Post} - Av: {guestbook[i].Author}");
            }
        }
        Console.WriteLine("\nTryck på valfri tangent för att återgå.");
        Console.ReadKey();
    }

    static void WriteToGuestbook()
    {
        Console.Clear();
        do
        {
            Console.WriteLine("Skriv ett nytt inlägg:");
            string post = Console.ReadLine();
            Console.WriteLine("Skriv ditt namn:");
            string postAuthor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(post) && !string.IsNullOrWhiteSpace(postAuthor))
            {
                guestbook.Add(new GuestbookPost { Post = post, Author = postAuthor });
                Console.WriteLine("Inlägget har lagts till i gästboken.");
                Console.WriteLine("\nTryck på valfri tangent för att börja skriva ett till inlägg eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            }
            else
            {
                Console.WriteLine("Varken inlägget eller fältet för ditt namn får vara tomt. Försök igen.");
                Console.WriteLine("\nTryck på valfri tangent för att börja skriva ett till inlägg eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Backspace);
    }

    static void DeleteGuestbookPost()
    {
        if (guestbook.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Gästboken är tom.\n");
            Console.WriteLine("Tryck på valfri tangent för att återgå.");
            Console.ReadKey();
        }
        else
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Radera ett inlägg från gästboken:");

                for (int i = 0; i < guestbook.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {guestbook[i].Post} - Av: {guestbook[i].Author}");
                }

                Console.Write("Ange numret på inlägget du vill radera:\n");

                if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber >= 1 && postNumber <= guestbook.Count)
                {
                    guestbook.RemoveAt(postNumber - 1);
                    Console.WriteLine("\nInlägget har raderats.\n");
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Ange ett giltigt nummer.\n");
                }
                Console.WriteLine("Tryck på valfri tangent för att ange numret på nytt eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            } while (Console.ReadKey(true).Key != ConsoleKey.Backspace);
        }
    }
}

class GuestbookPost
{
    public string Post { get; set; }
    public string Author { get; set; }
}
