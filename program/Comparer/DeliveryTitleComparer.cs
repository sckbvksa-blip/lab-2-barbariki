namespace Comparer;

using System.Collections;
using Delivery;

public class DeliveryTitleComparer : IComparer
{
    public int Compare(object x, object y)
    {
        Delivery d1 = x as Delivery;
        Delivery d2 = y as Delivery;

        if (d1 == null || d2 == null) return 0;

        return d1.title.CompareTo(d2.title);
    }
}