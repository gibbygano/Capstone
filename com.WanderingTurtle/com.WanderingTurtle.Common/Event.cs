namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Bryan Hurst
    /// Created: 2015/02/19
    /// 
    /// Class for the creation of Event Objects with set data fields
    /// </summary>
    public class Event
    {
        public Event(int eventItemID, string eventItemName,
                    bool transportation, int eventTypeID, bool onSite, int productID, string description, bool active)
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

        public bool Active { get; set; }

        public string Description { get; set; }

        public int EventItemID { get; set; }

        public string EventItemName { get; set; }

        public int EventTypeID { get; set; }

        public string EventTypeName { get; set; }

        public bool OnSite { get; set; }

        public string OnSiteString { get; set; }

        public int ProductID { get; set; }

        public bool Transportation { get; set; }

        public string TransportString { get; set; }

        public void setFields()
        {
            if (Transportation == true)
            {
                TransportString = "Provided";
            }
            else
            {
                TransportString = "Not Provided";
            }

            if (OnSite == true)
            {
                OnSiteString = "Yes";
            }
            else
            {
                OnSiteString = "No";
            }
        }
    }
}