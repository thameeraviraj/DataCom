namespace DataCom.WebAPI.Models;

public class Resource<T>
{
    public Resource(IEnumerable<T> items)
    {
        Items = items;
    }

    public IEnumerable<T> Items { get; }
}