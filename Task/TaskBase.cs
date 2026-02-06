namespace Task;

using Enums;
using Interfaces;

public abstract class TaskBase : IRoutable
{
    public Guid id = Guid.NewGuid();
    public string Title;
    public PriorityKey getPriorityKey(int intKey)
    {
        return (PriorityKey)intKey;
    }
}