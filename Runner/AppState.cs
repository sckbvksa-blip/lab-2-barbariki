namespace Runner;

using Enums;
using DeliveryRepository;
using Delivery;

public class AppState
{
    public DeliveryRepository Repository = new DeliveryRepository();
    public void createDelivery()
    {
        string title = getTitleFromUser();
        int keyPriority = getIntPriorityKeyFromUser();
        Repository.Deliveries.Add(new Delivery(title, keyPriority));
        Console.WriteLine("Successfully added.");
    }
    public void ShowAllDeliviries()
    {
        showDeliveries(Repository.Deliveries, "packing");
        showDeliveries(Repository.Departured, "departure");
        showDeliveries(Repository.Delivered, "delivered");
    }
    public void showDeliveries(List<Delivery> deliveries, string title)
    {
        if (deliveries.Count != 0)
        {
            Console.WriteLine("Packing:");
            
            for (int i = 0; i < deliveries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {deliveries[i].Title}.");
            }
        }
        else
        {
            Console.WriteLine($"There are no {title} deliviries");
        }
    }
    
    public void updateDelivery()
    {
        if (Repository.Deliveries.Count != 0 || Repository.Delivered.Count != 0 || Repository.Departured.Count != 0)
        {
            ShowAllDeliviries();
            Console.WriteLine();
            string title = getTitleFromUser();

            Delivery delivery = Repository.Deliveries.Find(del => del.Title == title);
            if (delivery == null) delivery = Repository.Departured.Find(del => del.Title == title);
            if (delivery == null) delivery = Repository.Delivered.Find(del => del.Title == title);

            if (delivery != null)
            {
                Console.WriteLine("Delivery found.\n");
                Console.WriteLine("----------------------");
                string answer = null;
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
                if (delivery.Status == DeliveryStatus.Packing) Repository.Deliveries.Remove(delivery);
                else if (delivery.Status == DeliveryStatus.Departure) Repository.Departured.Remove(delivery);
                else if (delivery.Status == DeliveryStatus.Delivered) Repository.Delivered.Remove(delivery);
                if (answer == "2")
                {
                    delivery.Status = (DeliveryStatus)getStatusFromUser();

                    if (delivery.Status == DeliveryStatus.Packing) Repository.Deliveries.Add(delivery);
                    else if (delivery.Status == DeliveryStatus.Departure) Repository.Departured.Add(delivery);
                    else if (delivery.Status == DeliveryStatus.Delivered) Repository.Delivered.Add(delivery);
                    
                    Console.WriteLine("Successfully updated.");
                }
            }
            else
            {
                Console.WriteLine("Delivery not found!");
            }
        }
        else { Console.WriteLine("There are no deliviries"); }

    }
    public int getStatusFromUser()
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
    public string getTitleFromUser()
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
    public int getIntPriorityKeyFromUser()
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
    public void sendDelivery()
    {
        if (Repository.Deliveries.Count != 0)
        {
            showDeliveries(Repository.Deliveries, "packing");
            string text = getTitleFromUser();
            Delivery delivery = Repository.Deliveries.Find(del => del.Title == text);

            if (delivery != null)
            {
                Repository.Deliveries.Remove(delivery);
                delivery.Status = DeliveryStatus.Departure;
                Repository.Departured.Add(delivery);
                Console.WriteLine("Successfully sent.");
            }
            else { Console.WriteLine("Delivery not found."); }
        }
        else { Console.WriteLine("There are no deliviries"); }
    }
}