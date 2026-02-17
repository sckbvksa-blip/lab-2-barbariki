namespace Menu;

using Runner;
using Delivery;
using DayData;

public class MenuWritter
{
    public static void ShowAllDeliveries(AppState state)
    {
        ShowDeliveries(state.repository.deliveries, "packing");
        ShowDeliveries(state.repository.departured, "departure");
        ShowDeliveries(state.repository.delivered, "delivered");
    }

    public static void ShowDeliveries(List<Delivery> deliveries, string title)
    {
        if (deliveries.Count == 0)
        {
            Console.WriteLine($"There are no {title} deliviries");
            return;
        }

        Console.WriteLine($"{char.ToUpper(title[0]) + title.Substring(1)}:");

        for (int i = 0; i < deliveries.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {deliveries[i]}.");
        }
    }

    public static void ShowDayResult(DayData day)
    {
        Console.WriteLine($"Day {day.DayCounter}: {day.AmountOfDepartured} departured deliveries.");
    }

    public static void ShowAllDaysResult(AppState state)
    {
        Console.WriteLine("Results:");
        for (int i = 0; i < state.daysStorage.Count; i++)
        {
            ShowDayResult(state.daysStorage[i]);
        }
    }
}