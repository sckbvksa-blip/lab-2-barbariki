namespace Menu;

public class MenuReader
{
    public static int GetStatusFromUser()
    {
        while (true)
        {
            Console.WriteLine("0 - Exit.");
            Console.WriteLine("Choose status:");
            Console.WriteLine("1 - Packing.\n2 - Departue.\n3 - Delivered\n4 - Deleting");
            Console.Write("Enter key: ");

            string text = Console.ReadLine();

            if (text == "1" || text == "2" || text == "3" || text == "4")
            {
                return int.Parse(text);
            }
            else if (text == "0") { return 0; }

            Console.WriteLine("Wrong input!\nTry again.");
        }
    }

    public static string GetTitleFromUser()
    {
        while (true)
        {
            Console.WriteLine("0 - Exit.");
            Console.Write("Enter title: ");

            string title = Console.ReadLine();

            if (title == null || title == string.Empty)
            {
                Console.WriteLine("Title can't be null or empty!\nTry again.");
                continue;
            }
            else if (title == "0") return string.Empty;

            return title;
        }
    }

    public static int GetIntPriorityKeyFromUser()
    {
        while (true)
        {
            Console.WriteLine("\nChoose priority:");
            Console.WriteLine("1 - Important.\n2 - Not much.\n3 - Poop.");
            Console.Write("Enter key: ");

            string text = Console.ReadLine();

            if (text == "1" || text == "2" || text == "3")
            {
                return int.Parse(text);
            }
            else if (text == "0") { return 0; }

            Console.WriteLine("Wrong input!\nTry again.");
        }
    }
}