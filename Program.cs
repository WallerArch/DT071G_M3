using System.Text.Json;


class GuestbookPost //Skapar en klass som kommer användas för att ange både inläggets innehåll samt för fattare
{
    public string Post { get; set; }
    public string Author { get; set; }
}


class Program
{
    static List<GuestbookPost> guestbook = new List<GuestbookPost>(); //Skapar en variabel som är en lista som innehåller klassen för gästbokens alla poster  
    static string jsonFilePath = "guestBook.json"; // Filnamnet för JSON-filen

    static void Main()
    {
        LoadGuestbook(); // Läs in tidigare inlägg från JSON-filen

        bool programRuns = true;  //Skapar variabel för att kunna skapa ett alternativ användaren kan välja som stänger ned applikationen

        while (programRuns)
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Björns gästbok!");
            Console.WriteLine("1. Läs alla inlägg");
            Console.WriteLine("2. Skriv ett nytt inlägg");
            Console.WriteLine("3. Radera ett inlägg");
            Console.WriteLine("4. Avsluta");

            int choice; //Variabel för integern som är valet användaren får göra
            int.TryParse(Console.ReadLine(), out choice); //Lyssnar efter den siffra användaren anger och parsar den till en integer från textsträng

            switch (choice) //Alternativen som skrivs ut för användaren att välja bland när applikationen startats
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
                    SaveGuestbook(); 
                    programRuns = false; //Applikationen stängs av
                    break;
                default:
                    break;
            }
        }
    }

    /* Funktioner för gästboken */

    static void LoadGuestbook() //Hämtar gästboken om den finns
    {
        if (File.Exists(jsonFilePath)) //Kontroll om filen existerar
        {
            string json = File.ReadAllText(jsonFilePath); //Lagrar allt från filen i en variabel

            if (!string.IsNullOrWhiteSpace(json)) //Kontroll om data finns
            {
                guestbook = JsonSerializer.Deserialize<List<GuestbookPost>>(json); //Om det finns data i filen skrivs denna till listan guestbook som senare skrivs ut till skärmen i funktionen ReadGuestbook
            }
            else
            {
                Console.WriteLine("JSON-filen är tom, saknas eller innehåller ogiltigt data."); //Felmeddelande
            }
        }
    }


    static void SaveGuestbook() //Funktionen som skriver in datat till JSON-filen
    {
        string json = JsonSerializer.Serialize(guestbook); //Använder funktionen för att serialisera listan "guestbook" till jsonfilen
        File.WriteAllText(jsonFilePath, json);
    }



    static void ReadGuestbook() //Funktion som läser ut data från JSON-filen
    {
        Console.Clear();
        Console.WriteLine("Inlägg i gästboken:");
        if (guestbook.Count == 0) //Kontroll som skriver ut meddelande om JSON-filen är tom
        {
            Console.WriteLine("\nGästboken är tom.");
        }
        else
        {
            for (int i = 0; i < guestbook.Count; i++) //Om filen inte är tom loopas datat igenom och skrivs ut 
            {
                Console.WriteLine($"{i + 1}. {guestbook[i].Post} - Av: {guestbook[i].Author}");
            }
        }
        Console.WriteLine("\nTryck på valfri tangent för att återgå.");
        Console.ReadKey();
    }

    static void WriteToGuestbook() //Funktion som skriver datat användaren skriver till JSON-filen
    {
        Console.Clear();
        do
        {
            Console.WriteLine("Skriv ett nytt inlägg:");
            string post = Console.ReadLine();
            Console.WriteLine("Skriv ditt namn:");
            string postAuthor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(post) && !string.IsNullOrWhiteSpace(postAuthor)) //Kontroll så inget fält lämnas tomt
            {
                guestbook.Add(new GuestbookPost { Post = post, Author = postAuthor }); //Lägger till det användaren skrev till deras motsvarigheter i klassen GuestbookPost
                Console.WriteLine("Inlägget har lagts till i gästboken.");
                Console.WriteLine("\nTryck på valfri tangent för att börja skriva ett till inlägg eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            }
            else
            {
                Console.WriteLine("Varken inlägget eller fältet för ditt namn får vara tomt. Försök igen."); //Felmeddelande
                Console.WriteLine("\nTryck på valfri tangent för att börja skriva ett till inlägg eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Backspace); //För att backa måste just bakstegs-tangenten tryckas på
    }

    static void DeleteGuestbookPost() //Funktion för att radera ett inlägg
    {
        if (guestbook.Count == 0) //Kontroll om gästboken är tom
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

                for (int i = 0; i < guestbook.Count; i++) //Loopar igenom och skriver ut alla inläggen från JSON-filen
                {
                    Console.WriteLine($"{i + 1}. {guestbook[i].Post} - Av: {guestbook[i].Author}");
                }

                Console.Write("Ange numret på inlägget du vill radera:\n");

                if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber >= 1 && postNumber <= guestbook.Count) //Kontroll att det angivna numnret är minst 1 och inte större än högsta index-siffran i JSON-filens array
                {
                    guestbook.RemoveAt(postNumber - 1); //Radera indexet som har siffran användaren angav minus 1, eftersom arrayens index i JSON-filen börjar på 0
                    Console.WriteLine("\nInlägget har raderats.\n");
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Ange ett giltigt nummer.\n"); //Felmeddelande om användaren anger ogiltig siffra
                }
                Console.WriteLine("Tryck på valfri tangent för att ange ett nummer på nytt eller tryck på tangenten backsteg för att återgå till startmenyn.\n");
            } while (Console.ReadKey(true).Key != ConsoleKey.Backspace);
        }
    }
}