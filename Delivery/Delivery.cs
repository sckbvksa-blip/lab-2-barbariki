namespace Delivery;

using Enums;
using Interfaces;
using Task;
public class Delivery : TaskBase
{
    public PriorityKey Priority { get; set; }
    public DeliveryStatus Status { get; set; }

    public Delivery(string title)
    {
        Title = title;
        Status = DeliveryStatus.Packing;
        Priority = this.GetPriorityKey();
    }
}