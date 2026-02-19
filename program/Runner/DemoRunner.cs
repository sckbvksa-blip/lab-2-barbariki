using Comparer;

namespace Runner;

using Menu;

public class DemoRunner
{
    public static void Run()
    {
        Console.Clear();
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
                    data.CreateDelivery(MenuReader.GetTitleFromUser(), MenuReader.GetIntPriorityKeyFromUser());
                    break;

                case ("2"):
                    Console.Clear();
                    data.UpdateHelper();
                    break;

                case ("3"):
                    Console.Clear();
                    MenuWritter.ShowAllDeliveries(data);
                    break;

                case ("4"):
                    Console.Clear();
                    MenuWritter.ShowDeliveries(data.repository.deliveries, "packing"); break;

                case ("5"):
                    Console.Clear();
                    data.SendHelper();
                    break;

                case ("6"):
                    Console.Clear();
                    Array.Sort(data.repository.deliveries.items);
                    Console.WriteLine("Successfully sorted by priority.");
                    break;
                
                case ("7"):
                    Console.Clear();
                    Array.Sort(data.repository.deliveries.items, new DeliveryTitleComparer());
                    Console.WriteLine("Successfully sorted by priority.");
                    break;

                case ("8"):
                    Console.Clear();
                    MenuWritter.ShowDayResult(data.currentDay);
                    break;

                case ("9"):
                    Console.Clear();
                    data.NextDay();
                    break;

                case ("10"):
                    Console.Clear();
                    MenuWritter.ShowAllDaysResult(data);
                    break;

                case ("11"):
                    Console.Clear();
                    data.SaveDayData();
                    data.SaveRepositoryData();
                    return;
                
            }
        }
    }
}