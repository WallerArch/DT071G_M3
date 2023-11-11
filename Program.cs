// See https://aka.ms/new-console-template for more information


class Program
{
    static List<string> guestbook = new List<string>();

    static void Main()
    {
        bool programGoes = true;

        while (programGoes)
        {
            Console.Clear();
            Console.WriteLine("Välkommen till gästboken!");
            Console.WriteLine("1. Läs alla inlägg");
            Console.WriteLine("2. Skriv ett nytt inlägg");
            Console.WriteLine("3. Radera ett inlägg");
            Console.WriteLine("4. Avsluta");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
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
                        programGoes = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
        }
    }

    static void ReadGuestbook()
    {
        Console.Clear();
        Console.WriteLine("Inlägg i gästboken:");
        if (guestbook.Count == 0)
        {
            Console.WriteLine("Gästboken är tom.");
        }
        else
        {
            for (int i = 0; i < guestbook.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {guestbook[i]}");
            }
        }
        Console.WriteLine("Tryck på valfri tangent för att återgå.");
        Console.ReadKey();
    }

    static void WriteToGuestbook()
    {
        Console.Clear();
        Console.WriteLine("Skriv ett nytt inlägg:");
        string post = Console.ReadLine();
        guestbook.Add(post);
        Console.WriteLine("Inlägget har lagts till i gästboken.");
        Console.WriteLine("Tryck på valfri tangent för att återgå.");
        Console.ReadKey();
    }

    static void DeleteGuestbookPost()
    {
        Console.Clear();
        Console.WriteLine("Radera ett inlägg från gästboken:");
        ReadGuestbook();
        Console.Write("Ange numret på inlägget du vill radera: ");
        if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber >= 1 && postNumber <= guestbook.Count)
        {
            guestbook.RemoveAt(postNumber - 1);
            Console.WriteLine("Inlägget har raderats.");
        }
        else
        {
            Console.WriteLine("Ogiltigt val. Ange ett giltigt nummer.");
        }
        Console.WriteLine("Tryck på valfri tangent för att återgå.");
        Console.ReadKey();
    }
}
