namespace Delivery;

using Enums;
using Interfaces;
using Task;

public class Delivery : TaskBase
{
    public PriorityKey Priority { get; set; }
    public DeliveryStatus Status { get; set; }

    public Delivery(string title, int intPriority)
    {
        Title = title;
        Status = DeliveryStatus.Packing;
        Priority = this.getPriorityKey(intPriority);
    }
}