namespace Task;

using Enums;
using Interfaces;

public abstract class TaskBase : IRoutable
{
    public Guid id = Guid.NewGuid();
    public string title { get; set; }
    public PriorityKey GetPriorityKey(int intKey)
    {
        return (PriorityKey)intKey;
    }
}