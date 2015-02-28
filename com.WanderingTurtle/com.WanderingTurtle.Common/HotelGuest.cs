namespace com.WanderingTurtle.Common
{
    public class HotelGuest 
    {
        /// <summary>
        /// Create a HotelGuest object.
        /// </summary>
        /// Miguel Santana 2/18/2015
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/02/27
        /// </remarks>
        public int? HotelGuestID { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Address1 { get; private set; }

        public string Address2 { get; private set; }

        public CityState CityState { get; private set; }

        public string PhoneNumber { get; private set; }

        public string EmailAddress { get; private set; }

        public bool Active { get; private set; }

        public int? Room { get; private set; }

        public HotelGuest(int HotelGuestID, string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, int? Room, bool Active = true)
        {
            SetValues(HotelGuestID, FirstName, LastName, Address1, Address2, CityState, PhoneNumber, EmailAddress, Room, Active);
        }
        public HotelGuest(string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, int? Room, bool Active = true)
        {
            SetValues(null, FirstName, LastName, Address1, Address2, CityState, PhoneNumber, EmailAddress, Room, Active);
        }

        private void SetValues(int? HotelGuestID, string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, int? Room, bool Active)
        {
            this.HotelGuestID = HotelGuestID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.CityState = CityState;
            this.PhoneNumber = PhoneNumber;
            this.EmailAddress = EmailAddress;
            this.Room = Room;
            this.Active = Active;
        }

        public string GetFullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
    }
}