namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Matthew Lapka
    /// Created: 2015/04/14
    /// 
    /// Class for the creation of BookingNumbers objects with set data fields.
    /// </summary>
    public class BookingNumbers
    {
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/04/14
        /// </summary>
        public BookingNumbers()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Quantity { get; set; }

        public string Room { get; set; }
    }
}