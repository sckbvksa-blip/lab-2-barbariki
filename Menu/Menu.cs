namespace Menu;

public class Menu
{
    public string[] menuItems =
    {
        "Add Delivery", "Update delivery", "Show All deliveries",
        "Show all undelivered", "Send delivery", "Sort by priority", 
        "Day result", "Next day", "Exit"
    };

    public void printMenu()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            Console.WriteLine($"{i+1}. {menuItems[i]}.");
        }
    }
    
}