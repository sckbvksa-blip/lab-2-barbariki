namespace DeliveryRepository;

using Delivery;

public class DeliveryRepository
{
    public List<Delivery> Deliveries = new List<Delivery>();
    public List<Delivery> Departured = new List<Delivery>();
    public List<Delivery> Delivered = new List<Delivery>();
    public Delivery Find(string title)
    {
        try
        {
            return Deliveries.Find(Delivery => Delivery.Title == title);
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Empty list.");
            return null;
        }
    }
    public void Sort()
    {
        
    }
}