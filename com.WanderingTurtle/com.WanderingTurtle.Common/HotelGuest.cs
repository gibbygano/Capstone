namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Miguel Santana
    /// Created: 2015/02/18
    /// 
    /// Create a HotelGuest object.
    /// </summary>
    /// <remarks>
    /// Rose Steffensmeier
    /// Updated: 2015/02/27
    /// </remarks>
    public class HotelGuest 
    {
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

        public int? GuestPIN { get; private set; }

        /// <summary>
        /// Rose Steffensmeier
        /// Created: 2015/02/27
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/04/03
        /// Added GuestPIN
        /// </remarks>
        /// <param name="HotelGuestID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Address1"></param>
        /// <param name="Address2"></param>
        /// <param name="CityState"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="EmailAddress"></param>
        /// <param name="Room"></param>
        /// <param name="Active"></param>
        /// <param name="GuestPIN"></param>
        public HotelGuest(int HotelGuestID, string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, int? Room, int? GuestPIN, bool Active = true)
        {
            SetValues(HotelGuestID, FirstName, LastName, Address1, Address2, CityState, PhoneNumber, EmailAddress, Room, GuestPIN, Active);
        }

        /// <summary>
        /// Rose Steffensmeier
        /// Created: 2015/02/27
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/04/03
        /// Added GuestPIN
        /// </remarks>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Address1"></param>
        /// <param name="Address2"></param>
        /// <param name="CityState"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="EmailAddress"></param>
        /// <param name="Room"></param>
        /// <param name="Active"></param>
        /// <param name="GuestPIN"></param>
        public HotelGuest(string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, int? Room, int? GuestPIN, bool Active = true)
        {
            SetValues(null, FirstName, LastName, Address1, Address2, CityState, PhoneNumber, EmailAddress, Room, GuestPIN, Active);
        }

        /// <summary>
        /// Rose Steffensmeier
        /// Created: 2015/02/27
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/04/03
        /// Added GuestPIN
        /// </remarks>
        /// <param name="HotelGuestID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Address1"></param>
        /// <param name="Address2"></param>
        /// <param name="CityState"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="EmailAddress"></param>
        /// <param name="Room"></param>
        /// <param name="Active"></param>
        /// <param name="GuestPIN"></param>
        private void SetValues(int? HotelGuestID, string FirstName, string LastName, string Address1, string Address2, CityState CityState, string PhoneNumber, string EmailAddress, int? Room, int? GuestPIN, bool Active = true)
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
            this.GuestPIN = GuestPIN;
        }


        public string GetFullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
    }
}