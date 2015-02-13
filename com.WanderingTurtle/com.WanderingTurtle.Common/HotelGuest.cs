namespace com.WanderingTurtle.Common
{
    public class HotelGuest : NewHotelGuest
    {
        /// <summary>
        /// Create a HotelGuest object. To create a HotelGuestObject without an id, use NewHotelGuest
        /// </summary>
        public int HotelGuestID { get; set; }

        public HotelGuest(int HotelGuestID, string FirstName, string LastName, string Address1, string Address2, string Zip, string PhoneNumber, string EmailAddress, int HotelGuestPIN)
            : base(FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, HotelGuestPIN)
        {
            this.HotelGuestID = HotelGuestID;
        }
    }

    public class NewHotelGuest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Zip { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public int HotelGuestPIN { get; set; }

        public NewHotelGuest(string FirstName, string LastName, string Address1, string Address2, string Zip, string PhoneNumber, string EmailAddress, int HotelGuestPIN)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.Zip = Zip;
            this.PhoneNumber = PhoneNumber;
            this.EmailAddress = EmailAddress;
            this.HotelGuestPIN = HotelGuestPIN;
        }
    }
}