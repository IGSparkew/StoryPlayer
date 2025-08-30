public class Event
{

    public EventType Type { get; set; }
    public EventTypeOutput TypeOutput { get; set; }

    public string Output { get; set; }

    public string Flag { get; set; }

    public string Script { get; set; }

    public string Next { get; set; }

    public string Default;

    public Event(EventType type, string output, EventTypeOutput typeOutput, string flag, string script, string next, string defaultValue)
    {
        Type = type;
        Output = output;
        TypeOutput = typeOutput;
        Flag = flag;
        Script = script;
        Next = next;
        Default = defaultValue;
    }

    public Event() : this(EventType.NONE, "", EventTypeOutput.NONE, "", "", "", "") { }
}