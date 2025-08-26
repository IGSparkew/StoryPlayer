public class LayoutConfigResolver
{
    public List<string> Values { get; set; }

    public LayoutConfigResolver()
    {
        Values = new List<string>();
    }

    public bool HasOneElement() {
        return Values.Count == 1;
    }

    public string getValue()
    {
        if (!this.HasOneElement())
        {
            throw new Exception("the id of element can't be on to data board!");
        }

        return Values[0];
    }

    public static LayoutConfigResolver resolver(LayoutConfig config, Board board)
    {
        LayoutConfigResolver layoutConfigResolver = new LayoutConfigResolver();

        switch (config.Id)
        {
            case "Title":
                layoutConfigResolver.Values.Add(board.Name);
                break;
            case "Description":
                layoutConfigResolver.Values.Add(board.Description);
                break;

        }

        return layoutConfigResolver;
    } 



}