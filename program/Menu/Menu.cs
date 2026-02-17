namespace Menu;

public class Menu
{
    public static string[] menuItems =
    {
        "Add Delivery", "Update delivery", "Show All deliveries",
        "Show all undelivered", "Send delivery", "Sort by priority",
        "Day result", "Next day", "Show all days result", "Exit"
    };

    public static void PrintMenu()
    {
        Console.WriteLine("----------------------");
        for (int i = 0; i < menuItems.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {menuItems[i]}.");
        }
        Console.WriteLine("----------------------");
    }
}