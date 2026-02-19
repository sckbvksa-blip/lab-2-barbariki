using Collections;

namespace DeliveryRepository;

using Delivery;

public class DeliveryRepository
{
    public DeliveryCollection deliveries { get; set; } = new DeliveryCollection();
    public DeliveryCollection departured { get; set; } = new DeliveryCollection();
    public DeliveryCollection delivered { get; set; } = new DeliveryCollection();
    public DeliveryRepository() { }

}