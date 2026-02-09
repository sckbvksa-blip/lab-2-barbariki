namespace Runner;

using Menu;

public class DemoRunner
{
    public static void Run()
    {
        AppState data = new AppState();
        while (true)
        {
            Menu.PrintMenu();
            string answer = Console.ReadLine();
            Console.WriteLine("----------------------");
            switch (answer)
            {
                case ("1"):
                    Console.Clear();
                    data.CreateDelivery(); 
                    break;
                case ("2"):
                    Console.Clear();
                    data.UpdateDelivery(); 
                    break;
                case ("3"):
                    Console.Clear();
                    data.ShowAllDeliviries(); 
                    break;
                case ("4"):
                    Console.Clear();
                    data.ShowDeliveries(data.repository.deliveries, "packing"); break;
                case ("5"):
                    Console.Clear();
                    data.SendDelivery(); 
                    break;
                case ("6"):
                    Console.Clear();
                    data.QuickSortByPriority(data.repository.deliveries, 0, data.repository.deliveries.Count - 1);
                    Console.WriteLine("Successfully sorted by priority.");
                    break;
                case ("7"):
                    Console.Clear();
                    data.ShowDayResult(data.currentDay); 
                    break;
                case ("8"):
                    Console.Clear();
                    data.NextDay(); 
                    break;
                case ("9"):
                    Console.Clear();
                    data.ShowAllDaysResult(); 
                    break;
                case ("10"):
                    Console.Clear();
                    data.SaveDayData();
                    data.SaveRepositoryData(); 
                    return;
            }
        }
    }
}