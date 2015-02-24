using System;

public class Event
{
    //Bryan Hurst Feb.19
    //Object for the creation of Event objects with set data fields
    public int EventItemID         { get; set; }
    public string EventItemName    { get; set; }
    public bool Transportation     { get; set; }
    public int EventTypeID         { get; set; }
    public bool OnSite             { get; set; }
    public int ProductID           { get; set; }
    public String Description      { get; set; }
    public bool Active             { get; set; }

	public Event(int eventItemID, string eventItemName, 
    bool transportation, int eventTypeID, bool onSite, int productID, String description, bool active)
	{
        EventItemID = eventItemID;
        EventItemName = eventItemName;
        Transportation = transportation;
        EventTypeID = eventTypeID;
        OnSite = onSite;
        ProductID = productID;
        Description = description;
        Active = active;
	}

    public Event()
    {

    }
}
