namespace Interfaces;

using Enums;

interface IRoutable
{
    PriorityKey GetPriorityKey(int intKey);
}