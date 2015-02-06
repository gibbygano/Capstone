using System;

public class Event
{
    public int EventItemID         { get; set; }
    public string EventItemName    { get; set; }
    public DateTime EventStartDate { get; set; }
    public DateTime EventEndDate   { get; set; }
    public int MaxNumGuests        { get; set; }
    public int MinNumGuests        { get; set; }
    public int CurrentNumGuests    { get; set; }
    public bool Transportation     { get; set; }
    public int EventTypeID         { get; set; }
    public bool OnSite             { get; set; }
    public int ProductID           { get; set; }
    public decimal PricePerPerson  { get; set; }
    public String Description      { get; set; }
    public bool Active             { get; set; }

	public Event(int eventItemID, string eventItemName, DateTime eventStartDate, DateTime eventEndDate, int maxNumGuests, int minNumGuests,
        int currentNumGuests, bool transportation, int eventTypeID, bool onSite, int productID, decimal pricePerPerson, String description, bool active)
	{
        EventItemID = eventItemID;
        EventItemName = eventItemName;
        EventStartDate = eventStartDate;
        EventEndDate = eventEndDate;
        MaxNumGuests = maxNumGuests;
        MinNumGuests = minNumGuests;
        CurrentNumGuests = currentNumGuests;
        Transportation = transportation;
        EventTypeID = eventTypeID;
        OnSite = onSite;
        ProductID = productID;
        PricePerPerson = pricePerPerson;
        Description = description;
        Active = active;
	}

    public Event()
    {

    }

    void changeDate()
    {

    }

    void modifyEventInfo()
    {

    }
}
