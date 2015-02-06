using System;

public class Event : Product
{
    int EventItemID         { get; set; }
    string EventItemName    { get; set; }
    DateTime EventStartDate { get; set; }
    DateTime EventEndDate   { get; set; }
    int MaxNumGuests        { get; set; }
    int MinNumGuests        { get; set; }
    int CurrentNumGuests    { get; set; }
    bool Transportation     { get; set; }
    int EventTypeID         { get; set; }
    bool OnSite             { get; set; }
    int ProductID           { get; set; }
    decimal PricePerPerson  { get; set; }
    String Description      { get; set; }
    bool Active             { get; set; }

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
