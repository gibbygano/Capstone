namespace com.WanderingTurtle.Common
{
    public class EventType
    {
        public EventType()
        {
        }

        public EventType(int eventTypeID, string eventName)
        {
            EventTypeID = eventTypeID;
            EventName = eventName;
        }

        public string EventName { get; set; }

        /// <summary>
        /// Bryan Hurst Feb.19
        /// Object for the creation of EventType objects with set data fields
        /// </summary>
        public int EventTypeID { get; set; }
    }
}