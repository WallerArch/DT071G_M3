// See https://aka.ms/new-console-template for more information


class Program
{
    static List<string> guestbook = new List<string>();

    static void Main()
    {
        bool programRuns = true;

        while (programRuns)
        {
            Console.Clear(); //Tömmer konsolen så den inte bara fylls på eftersom, så det då blir lättare att läsa konsolen eftersom
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
                        programRuns = false;
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

    //Funktioner för gästboken

    static void ReadGuestbook() //Funktion för att läsa alla inlägg
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
                Console.WriteLine($"{i + 1}. {guestbook[i]}");
            }
        }
        Console.WriteLine("\nTryck på valfri tangent för att återgå.");
        Console.ReadKey();
    }

    static void WriteToGuestbook() //Funktion för att lägga till nytt inlägg i gästboken
    {
        Console.Clear();
        do
        {
            Console.WriteLine("Skriv ett nytt inlägg:");
            string post = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(post)) // Kontrollera om posten är tom eller bara innehåller vita mellanslag
            {
                guestbook.Add(post);
                Console.WriteLine("Inlägget har lagts till i gästboken.");
                Console.WriteLine("\nTryck på valfri tangent för att börja skriva ett till inlägg eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            }
            else
            {
                Console.WriteLine("Inlägget får inte vara tomt. Försök igen.");
                Console.WriteLine("\nTryck på valfri tangent för att börja skriva ett till inlägg eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Backspace);
    }
    static void DeleteGuestbookPost() //Funktion för att ta bort specifika inlägg
    {
        if (guestbook.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Gästboken är tom.");
            Console.WriteLine("Tryck på valfri tangent för att återgå.");
            Console.ReadKey();
        }
        else do
            {
                Console.Clear();
                Console.WriteLine("Radera ett inlägg från gästboken:");
                //Återanvänder funktionaliteten från ReadGuestBook-funktionen, att bara kalla på funktionen istället skulle skrivit ut text som förvirrat

                {
                    for (int i = 0; i < guestbook.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {guestbook[i]}");
                    }
                    Console.Write("Ange numret på inlägget du vill radera:\n");
                }
                if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber >= 1 && postNumber <= guestbook.Count)
                {
                    guestbook.RemoveAt(postNumber - 1);
                    Console.WriteLine("Inlägget har raderats.\n");
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Ange ett giltigt nummer.\n");
                }
                Console.WriteLine("Tryck på valfri tangent för att ange nummer på nytt eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Backspace);
    }
}
