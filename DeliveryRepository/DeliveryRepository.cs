namespace DeliveryRepository;

using Delivery;

public class DeliveryRepository
{
    public List<Delivery> deliveries { get; set; } = new List<Delivery>();
    public List<Delivery> departured { get; set; } = new List<Delivery>();
    public List<Delivery> delivered { get; set; } = new List<Delivery>();
    public DeliveryRepository() { }
    public Delivery Find(string title)
    {
        try
        {
            return deliveries.Find(Delivery => Delivery.title == title);
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Empty list.");
            return null;
        }
    }
}