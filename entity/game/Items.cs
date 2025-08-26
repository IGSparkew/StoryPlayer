public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Usable { get; set; }

    public Item(string name, string description, bool usable)
    {
        Name = name;
        Description = description;
        Usable = usable;
    }

    public Item() : this("", "", false)
    {
    }

}