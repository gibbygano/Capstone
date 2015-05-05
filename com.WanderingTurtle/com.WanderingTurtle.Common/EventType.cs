namespace com.WanderingTurtle.Common
{
    public class EventType
    {
        /// <summary>
        /// Bryan Hurst
        /// Created: 2015/02/19
        /// 
        /// Class for the creation of EventType Objects with set data fields
        /// </summary>

        public EventType()
        {
        }

        public EventType(int eventTypeID, string eventName)
        {
            EventTypeID = eventTypeID;
            EventName = eventName;
        }

        public string EventName { get; set; }


        public int EventTypeID { get; set; }
    }
}