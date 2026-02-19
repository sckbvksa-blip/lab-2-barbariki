namespace Collections;
using System.Collections;
using Delivery;

public class DeliveryCollection : IEnumerable
{
    public Delivery[] items;
    public int count;
    public Delivery this[int index] { get { return items[index]; } }
    
    public void Add(object obj)
    {
        if (obj is not Delivery newDelivery)
        {
            throw new InvalidCastException();
        }
        
        Delivery[] result = new Delivery[count + 1];
        for (int i = 0; i < count; i++)
        {
            result[i] = items[i];
        }

        result[count] = newDelivery;
        count++;
        items = result;
    }

    public void Remove(object? obj)
    {
        if (obj is not Delivery newDelivery)
        {
            throw new InvalidCastException();
        }
        
        Delivery[] result = new Delivery[count - 1];
        
        for (int i = 0, j = 0; i < count; i++, j++)
        {
            if (items[i] == newDelivery) { j--; continue; }
            result[j] = items[i];
        }

        items = result;
        count--;
    }

    public Delivery GetAt(int index)
    {
        if (index < 0 || index >= count) return null;
        return items[index];
    }

    public void SetAt(int index, Delivery delivery)
    {
        if (index < 0 || index >= count) return;
        items[index] = delivery;
    }
    
    public IEnumerator GetEnumerator()
    {
        return new DeliveryEnumerator(items);
    }
    
    public Delivery Find(string title)
    {
        for(int i = 0; i < count; i++)
        {
            if (items[i].title == title)
            {
                return items[i];
            }
        }
        return null;
    }
    
}