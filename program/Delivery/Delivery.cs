namespace Delivery;

using Enums;
using Interfaces;
using Task;

public class Delivery : TaskBase
{
    public PriorityKey priority { get; set; }
    public DeliveryStatus status { get; set; }

    public Delivery(string title, int intPriority) : base(title)
    {
        status = DeliveryStatus.Packing;
        priority = this.GetPriorityKey(intPriority);
    }
    public Delivery() { }

    public override string ToString()
    {
        return $"{title} - {priority}";
    }
}