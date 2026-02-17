namespace Runner;

using Enums;
using DeliveryRepository;
using Delivery;
using DayData;
using Menu;
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

    public void CreateDelivery(string title, int keyPriority)
    {
        if (title == "" || keyPriority == 0)
        {
            return;
        }
        repository.deliveries.Add(new Delivery(title, keyPriority));
        Console.WriteLine("Successfully added.");
    }
    public void UpdateDelivery(Delivery delivery, int newStatusKey)
    {
        if (delivery == null || newStatusKey == 0) { return; }
        DeliveryStatus newStatus = (DeliveryStatus)newStatusKey;

        DeleteDelivery(delivery);

        if (newStatus != DeliveryStatus.Deleting)
        {
            AddDelivery(delivery, newStatus);
        }
    }

    public void UpdateHelper()
    {
        if (repository.deliveries.Count == 0 && repository.delivered.Count == 0 && repository.departured.Count == 0)
        {
            Console.WriteLine("No deliveries for departue");
            return;
        }
        MenuWritter.ShowAllDeliveries(this);
        Console.WriteLine();

        Delivery delivery = FindDelivery(MenuReader.GetTitleFromUser());
        Console.WriteLine();
        if (delivery == null) { return; }

        UpdateDelivery(delivery, MenuReader.GetStatusFromUser());
    }

    public void AddDelivery(Delivery delivery, DeliveryStatus newStatus)
    {
        if (delivery == null) { return; }

        DeliveryStatus oldStatus = delivery.status;
        delivery.status = newStatus;

        if (delivery.status == DeliveryStatus.Packing) repository.deliveries.Add(delivery);
        else if (delivery.status == DeliveryStatus.Departure)
        {
            repository.departured.Add(delivery);
            currentDay.AmountOfDepartured++;
        }
        else if (delivery.status == DeliveryStatus.Delivered)
        {
            repository.delivered.Add(delivery);
        }
        if (oldStatus == DeliveryStatus.Packing && newStatus == DeliveryStatus.Delivered)
        {
            currentDay.AmountOfDepartured++;
        }

        Console.WriteLine($"Delivery status successfully changed from {oldStatus} to {newStatus}");
    }

    public void DeleteDelivery(Delivery delivery)
    {
        if (delivery == null) { return; }

        if (delivery.status == DeliveryStatus.Packing) repository.deliveries.Remove(delivery);
        else if (delivery.status == DeliveryStatus.Departure) repository.departured.Remove(delivery);
        else if (delivery.status == DeliveryStatus.Delivered) repository.delivered.Remove(delivery);
    }

    public void SendDelivery(Delivery delivery)
    {
        if (delivery == null) { return; }

        repository.deliveries.Remove(delivery);
        delivery.status = DeliveryStatus.Departure;
        repository.departured.Add(delivery);
        currentDay.AmountOfDepartured++;
        Console.WriteLine("Successfully sent.");
    }

    public void SendHelper()
    {
        if (repository.deliveries.Count == 0)
        {
            Console.WriteLine("No deliveries for departue");
            return;
        }
        MenuWritter.ShowDeliveries(repository.deliveries, "Packing");
        Console.WriteLine();
        SendDelivery(FindDelivery(MenuReader.GetTitleFromUser()));
    }

    public Delivery FindDelivery(string title)
    {
        Delivery delivery = repository.deliveries.Find(del => del.title == title);
        if (delivery == null) delivery = repository.departured.Find(del => del.title == title);
        if (delivery == null) delivery = repository.delivered.Find(del => del.title == title);
        if (delivery == null && title != string.Empty) Console.WriteLine("Delivery not found.");
        return delivery;
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