namespace Collections;
using System.Collections;
using Delivery;

public class DeliveryEnumerator : IEnumerator
{
    private Delivery[] items;
    private int position = -1;

    public DeliveryEnumerator(Delivery[] items)
    {
        this.items = items;
    }

    public object Current => items[position];


    public bool MoveNext()
    {
        position++;
        return position < items.Length;
    }

    public void Reset()
    {
        position = -1;
    }
}