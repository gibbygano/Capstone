using System;

public class EventType
{
    public int EventTypeID     { get; set; }
    public string EventName    { get; set; }

    public EventType(int eventTypeID, string eventName)
	{
        EventTypeID = eventTypeID;
        EventName = eventName;
	}
}
