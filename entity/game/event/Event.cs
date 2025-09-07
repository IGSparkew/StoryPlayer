public class Event
{
    public EventType Type { get; set; } = EventType.NONE;
    public EventTypeOutput TypeOutput { get; set; } = EventTypeOutput.NONE;

    public string Output { get; set; } = String.Empty;

    public string Flag { get; set; } = String.Empty;

    public string Script { get; set; } = String.Empty;

    public string Next { get; set; } = String.Empty;

    public string Default { get; set; } = String.Empty;
}