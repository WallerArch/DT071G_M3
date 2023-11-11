// See https://aka.ms/new-console-template for more information


class Program
{
    static List<string> guestbook = new List<string>(); //Skapar en variabel gästboken som är en lista som innehåller textsträngar

    static void Main()
    {
        bool programRuns = true; //Skapar variabel för att kunna skapa ett alternativ som stänger ned applikationen

        while (programRuns)
        {
            Console.Clear(); //Tömmer konsolen så den inte bara fylls på eftersom, så det då blir lättare att läsa konsolen eftersom
            Console.WriteLine("Välkommen till Björns gästbok!");
            Console.WriteLine("1. Läs alla inlägg");
            Console.WriteLine("2. Skriv ett nytt inlägg");
            Console.WriteLine("3. Radera ett inlägg");
            Console.WriteLine("4. Avsluta");

            int choice; //Skapar en variabel det val av nästkommande alternativ användaren får

            int.TryParse(Console.ReadLine(), out choice); //Läser in det användaren anger och konverterar den strängen (som kan vara en siffra) till en integer

            switch (choice) //Alternativ för användaren som kallar på de olika funktionerna
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
                    programRuns = false; //Alternativ 4 stänger ned applikationen
                    break;
                default:
                    break;
            }
        }
    }

    //Funktioner för gästboken

    static void ReadGuestbook() //Funktion för att läsa alla inlägg
    {
        Console.Clear(); //Tömmer konsolen så den inte bara fylls på eftersom, så det då blir lättare att läsa konsolen eftersom
        Console.WriteLine("Inlägg i gästboken:");
        if (guestbook.Count == 0) //Om det inte finns några inlägg skrivs bara denna text ut
        {
            Console.WriteLine("\nGästboken är tom.");
        }
        else
        {
            for (int i = 0; i < guestbook.Count; i++) //Loopar igenom alla inlägg
            {
                Console.WriteLine($"{i + 1}. {guestbook[i]}"); //Skriver ut alla inlägg med dess index
            }
        }
        Console.WriteLine("\nTryck på valfri tangent för att återgå.");
        Console.ReadKey();
    }

    static void WriteToGuestbook() //Funktion för att lägga till nytt inlägg i gästboken
    {
        Console.Clear(); //Tömmer konsolen så den inte bara fylls på eftersom, så det då blir lättare att läsa konsolen eftersom
        do
        {
            Console.WriteLine("Skriv ett nytt inlägg:");
            string post = Console.ReadLine();
            Console.WriteLine("Skriv ditt namn:");
            string postAuthor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(post) && !string.IsNullOrWhiteSpace(postAuthor))  // Kontrollera så inte em tom strängs skickas in
            {
                guestbook.Add(post); //Add-funktionen lägger in det användaren skrev som nytt inlägg i listan med alla inlägg
                guestbook.Add(postAuthor); //Add-funktionen lägger in det användaren skrev som nytt inlägg i listan med alla inlägg
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
    static void DeleteGuestbookPost() //Funktion för att ta bort specifika inlägg
    {

        if (guestbook.Count == 0)
        {
            Console.Clear(); //Tömmer konsolen så den inte bara fylls på eftersom, så det då blir lättare att läsa konsolen eftersom
            Console.WriteLine("Gästboken är tom.\n");
            Console.WriteLine("Tryck på valfri tangent för att återgå.");
            Console.ReadKey();
        }
        else do
            {
                Console.Clear(); //Tömmer konsolen så den inte bara fylls på eftersom, så det då blir lättare att läsa konsolen eftersom
                Console.WriteLine("Radera ett inlägg från gästboken:");

                {
                    //Återanvänder funktionaliteten från ReadGuestBook-funktionen, att bara kalla på funktionen istället skulle skrivit ut text som förvirrat
                    for (int i = 0; i < guestbook.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {guestbook[i]}");
                    }
                    Console.Write("Ange numret på inlägget du vill radera:\n");
                }
                //If-sats som kollar om det användaren skriver in och kollar att det är större än 1 och mindre än högsta siffran av antalet inlägg i boken
                if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber >= 1 && postNumber <= guestbook.Count)
                {
                    guestbook.RemoveAt(postNumber - 1); //Använder RemoveAt funktionen och eftersom indexeringen i bakgrunden börjar på 0 blir det siffran användaren angav minus 1 som är indexet vi ska ta bort
                    Console.WriteLine("\nInlägget har raderats.\n");
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
