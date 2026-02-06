using Enums;

namespace lab_1_barbariki.Runner;
using DeliveryRepository;
using Delivery;
using System.Threading.Tasks;

public class AppState
{
    public void createDelivery(DeliveryRepository repo)
    {
        string title = getTitleFromUser();
        
        repo.Deliveries.Add(new Delivery(title));
    }
    public string getTitleFromUser()
    {
        while (true)
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            if (title == null || title == string.Empty)
            {
                Console.WriteLine("Title can't be null or empty!\nTry again");
                continue;
            }

            return title;
        }
    }

    public async void sendDelivery(DeliveryRepository repo)
    {
        Delivery delivery = repo.Deliveries.Find(del => del.Status == DeliveryStatus.Packing);

        if (delivery != null)
        {
            await Task.Run(() =>
            {
                repo.Deliveries.Remove(delivery);
                
                delivery.Status = DeliveryStatus.Departure;
                
                repo.Departured.Add(delivery);
                
                Task.Delay(new Random().Next(5000, 20000));

                repo.Departured.Remove(delivery);
                
                delivery.Status = DeliveryStatus.Delivered;
                
                repo.Delivered.Add(delivery);
            });
        }
    }

}