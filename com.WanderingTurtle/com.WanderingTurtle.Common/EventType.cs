namespace com.WanderingTurtle.Common
{
    public class EventType
    {
        //Bryan Hurst Feb.19
        //Object for the creation of EventType objects with set data fields
        public int EventTypeID { get; set; }

        public string EventName { get; set; }

        public EventType()
        {
        }

        public EventType(int eventTypeID, string eventName)
        {
            EventTypeID = eventTypeID;
            EventName = eventName;
        }
    }
}