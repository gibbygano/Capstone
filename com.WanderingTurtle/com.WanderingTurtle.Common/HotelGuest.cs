namespace com.WanderingTurtle.Common
{
    public class HotelGuest : NewHotelGuest
    {
        /// <summary>
        /// Create a HotelGuest object. To create a HotelGuestObject without an id, use NewHotelGuest
        /// </summary>
        /// Miguel Santana 2/18/2015
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 02/23/2015
        /// </remarks>
        public int HotelGuestID { get; private set; }

        public HotelGuest(int HotelGuestID, string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, bool Active = true)
            : base(FirstName, LastName, Address1, Address2, CityState, PhoneNumber, EmailAddress, Active)
        {
            this.HotelGuestID = HotelGuestID;
        }
    }

    /// <summary>
    /// Create a HotelGuest without an ID
    /// </summary>
    /// Miguel Santana 2/18/2015
    /// <remarks>
    /// Rose Steffensmeier
    /// Updated: 02/23/2015
    /// </remarks>
    public class NewHotelGuest
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Address1 { get; private set; }

        public string Address2 { get; private set; }

        public CityState CityState { get; private set; }

        public string PhoneNumber { get; private set; }

        public string EmailAddress { get; private set; }

        public bool Active { get; private set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public NewHotelGuest(string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, bool Active = true)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.CityState = CityState;
            this.PhoneNumber = PhoneNumber;
            this.EmailAddress = EmailAddress;
            this.Active = Active;
        }

        public string GetFullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
    }
}