namespace Runner;

using Enums;
using DeliveryRepository;
using Delivery;
using DayData;
using System.Text.Json;

public class AppState
{
    public DeliveryRepository repository;
    public List<DayData> daysStorage;
    public DayData currentDay;

    public string dataDirectory { get; set; } = Path.Combine(
        AppContext.BaseDirectory,
        "Data"
    );
    public string dayFileName { get; set; } = Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "DayData.json"
    );
    public string repositoryFileName { get; set; } = Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "RepositoryData.json"
    );

    public void CreateDelivery()
    {
        string title = GetTitleFromUser();
        int keyPriority = GetIntPriorityKeyFromUser();
        repository.deliveries.Add(new Delivery(title, keyPriority));
        Console.WriteLine("Successfully added.");
    }

    public void ShowAllDeliviries()
    {
        ShowDeliveries(repository.deliveries, "packing");
        ShowDeliveries(repository.departured, "departure");
        ShowDeliveries(repository.delivered, "delivered");
    }

    public void ShowDeliveries(List<Delivery> deliveries, string title)
    {
        if (deliveries.Count != 0)
        {
            Console.WriteLine($"{char.ToUpper(title[0]) + title.Substring(1)}:");

            for (int i = 0; i < deliveries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {deliveries[i]}.");
            }
        }
        else
        {
            Console.WriteLine($"There are no {title} deliviries");
        }
    }

    public AppState()
    {
        repository = GetRepositoryData();
        daysStorage = GetDayData();
        if (daysStorage.Count != 0) currentDay = daysStorage[daysStorage.Count - 1];
        else
        {
            currentDay = new DayData(daysStorage.Count + 1);
            daysStorage.Add(currentDay);
        }
    }

    public void UpdateDelivery()
    {
        if (repository.deliveries.Count == 0 && repository.delivered.Count == 0 && repository.departured.Count == 0)
        {
            Console.WriteLine("There are no deliviries");
        }
        ShowAllDeliviries();
        Console.WriteLine();

        string title = GetTitleFromUser();

        Delivery delivery = FindDelivery(title);

        if (delivery == null)
        {
            Console.WriteLine("Delivery not found!");
            return;
        }
        Console.WriteLine("Delivery found.\n");
        Console.WriteLine("----------------------");

        string answer = string.Empty;
        while (true)
        {
            Console.WriteLine("Delete(1) or change status(2)?");
            answer = Console.ReadLine();
            if (answer != "1" && answer != "2")
            {
                Console.WriteLine("Wrong input! Try again.");
                continue;
            }
            else break;
        }

        AddOrDeleteDelivery(delivery, DeliveryAction.Delete);

        if (answer == "2")
        {
            AddOrDeleteDelivery(delivery, DeliveryAction.Add);
            Console.WriteLine("Successfully updated.");
        }

    }

    public int GetStatusFromUser()
    {
        while (true)
        {
            Console.WriteLine("1 - Packing.\n2 - Departue.\n3 - Delivered.");
            Console.Write("Enter key: ");

            string text = Console.ReadLine();

            if (text == "1" || text == "2" || text == "3")
            {
                return int.Parse(text);
            }

            Console.WriteLine("Wrong input!\nTry again.");
        }
    }

    public string GetTitleFromUser()
    {
        while (true)
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            if (title == null || title == string.Empty)
            {
                Console.WriteLine("Title can't be null or empty!\nTry again.");
                continue;
            }

            return title;
        }
    }

    public int GetIntPriorityKeyFromUser()
    {
        while (true)
        {
            Console.WriteLine("1 - Important.\n2 - Not much.\n3 - Poop.");
            Console.Write("Enter key: ");

            string text = Console.ReadLine();

            if (text == "1" || text == "2" || text == "3")
            {
                return int.Parse(text);
            }

            Console.WriteLine("Wrong input!\nTry again.");
        }
    }

    public void SendDelivery()
    {
        if (repository.deliveries.Count == 0)
        {
            Console.WriteLine("There are no deliviries");
            return;
        }
        ShowDeliveries(repository.deliveries, "packing");
        string text = GetTitleFromUser();

        Delivery delivery = repository.deliveries.Find(del => del.title == text);

        if (delivery != null)
        {
            Console.WriteLine("Delivery not found.");
            return;
        }

        repository.deliveries.Remove(delivery);
        delivery.status = DeliveryStatus.Departure;
        repository.departured.Add(delivery);
        currentDay.AmountOfDepartured++;
        Console.WriteLine("Successfully sent.");
    }

    public Delivery FindDelivery(string title)
    {
        Delivery delivery = repository.deliveries.Find(del => del.title == title);
        if (delivery == null) delivery = repository.departured.Find(del => del.title == title);
        if (delivery == null) delivery = repository.delivered.Find(del => del.title == title);

        return delivery;
    }

    public void AddOrDeleteDelivery(Delivery delivery, DeliveryAction action)
    {
        if (delivery == null)
        {
            Console.WriteLine("Delivery not found.");
            return;
        }

        if (action == DeliveryAction.Add)
        {
            DeliveryStatus oldStatus = delivery.status;
            delivery.status = (DeliveryStatus)GetStatusFromUser();

            if (oldStatus == DeliveryStatus.Delivered)
            {
                repository.delivered.Remove(delivery);
            }
            else if (oldStatus == DeliveryStatus.Departure)
            {
                repository.departured.Remove(delivery);
                currentDay.AmountOfDepartured--;
            }
            else if (oldStatus == DeliveryStatus.Packing)
            {
                repository.deliveries.Remove(delivery);
            }

            if (delivery.status == DeliveryStatus.Packing) repository.deliveries.Add(delivery);
            else if (delivery.status == DeliveryStatus.Departure)
            {
                repository.departured.Add(delivery);
                currentDay.AmountOfDepartured++;
            }
            else if (delivery.status == DeliveryStatus.Delivered)
            {
                repository.delivered.Add(delivery);
                currentDay.AmountOfDepartured++;
            }

        }
        else if (action == DeliveryAction.Delete)
        {

            if (delivery.status == DeliveryStatus.Packing) repository.deliveries.Remove(delivery);
            else if (delivery.status == DeliveryStatus.Departure) repository.departured.Remove(delivery);
            else if (delivery.status == DeliveryStatus.Delivered) repository.delivered.Remove(delivery);
        }
    }

    public void ShowDayResult(DayData day)
    {
        Console.WriteLine($"Day {day.DayCounter}: {day.AmountOfDepartured} departured deliveries.");
    }

    public void ShowAllDaysResult()
    {
        Console.WriteLine("Results:");
        for (int i = 0; i < daysStorage.Count; i++)
        {
            ShowDayResult(daysStorage[i]);
        }
    }

    public void NextDay()
    {
        SaveDayData();
        currentDay = new DayData(daysStorage[daysStorage.Count - 1].DayCounter + 1);
        daysStorage.Add(currentDay);
        Console.WriteLine("The next day came.");
    }

    public void SaveDayData()
    {
        EnsureDataDirectoryExists();
        string json = JsonSerializer.Serialize(daysStorage);
        File.WriteAllText(dayFileName, json);
    }

    public List<DayData> GetDayData()
    {
        if (!File.Exists(dayFileName))
        {
            return new List<DayData>();
        }

        string json = File.ReadAllText(dayFileName);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<DayData>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<DayData>>(json);
        }
        catch (Exception)
        {
            return new List<DayData>();
        }

    }

    public void SaveRepositoryData()
    {
        EnsureDataDirectoryExists();
        string json = JsonSerializer.Serialize(repository);
        File.WriteAllText(repositoryFileName, json);
    }

    public DeliveryRepository GetRepositoryData()
    {
        if (!File.Exists(repositoryFileName))
        {
            return new DeliveryRepository();
        }

        string json = File.ReadAllText(repositoryFileName);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new DeliveryRepository();
        }

        try
        {
            return JsonSerializer.Deserialize<DeliveryRepository>(json);
        }
        catch (Exception)
        {
            return new DeliveryRepository();
        }
    }

    public void EnsureDataDirectoryExists()
    {
        if (!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }
    }

    public void QuickSortByPriority(List<Delivery> deliveries, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(deliveries, left, right);

            QuickSortByPriority(deliveries, left, pivotIndex);
            QuickSortByPriority(deliveries, pivotIndex + 1, right);
        }
    }
    private int Partition(List<Delivery> deliveries, int left, int right)
    {
        int pivotValue = (int)deliveries[(left + right) / 2].priority;
        int i = left - 1;
        int j = right + 1;

        while (true)
        {
            do
            {
                i++;
            } while ((int)deliveries[i].priority < pivotValue);

            do
            {
                j--;
            } while ((int)deliveries[j].priority > pivotValue);

            if (i >= j) return j;

            var temp = deliveries[i];
            deliveries[i] = deliveries[j];
            deliveries[j] = temp;
        }
    }

}