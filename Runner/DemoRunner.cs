namespace Runner;

using Menu;

public class DemoRunner
{
    public static void Run()
    {
        AppState data = new AppState();
        while (true)
        {
            Menu.printMenu();
            string answer = Console.ReadLine();
            Console.WriteLine("----------------------");
            switch (answer)
            {
                case ("1"): data.createDelivery(); break;
                case ("2"): data.updateDelivery(); break;
                case ("3"): data.ShowAllDeliviries(); break;
                case ("4"): data.showAllPackingDeliveries();break;
                case ("5"): data.sendDelivery(); break;
                case ("6"): break;
                case ("7"): break;
                case ("8"): break;
                case ("9"): return;
            }
        }
    }
}