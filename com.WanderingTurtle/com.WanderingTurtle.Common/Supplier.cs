namespace com.WanderingTurtle.Common
{
    public class Supplier
    {
        public Supplier(int supplierID, string companyName, string firstName, string lastName, string address1,
                    string address2, string zip, string phoneNumber, string emailAddress, int applicationID, bool active)
        {
            SupplierID = supplierID;
            CompanyName = companyName;
            FirstName = firstName;
            LastName = lastName;
            Address1 = address1;
            Address2 = address2;
            Zip = zip;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            ApplicationID = applicationID;
            Active = active;
        }

        public Supplier()
        {
        }

        public Supplier(string companyName, string firstName, string lastName, string address1,
                string address2, string zip, string phoneNumber, string emailAddress, int applicationID, decimal supplyCost, bool active)
        {
            CompanyName = companyName;
            FirstName = firstName;
            LastName = lastName;
            Address1 = address1;
            Address2 = address2;
            Zip = zip;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            ApplicationID = applicationID;
            SupplyCost = supplyCost;
            Active = active;
        }

        public bool Active { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public int ApplicationID { get; set; }

        public string CompanyName { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        //Bryan Hurst Feb.19
        //Object for the creation of Supplier objects with set data fields
        public int SupplierID { get; set; }

        public decimal? SupplyCost { get; set; }

        public string Zip { get; set; }

        public Supplier ShallowCopy()
        {
            return (Supplier)MemberwiseClone();
        }

        public string GetFullName { get { return FirstName + " " + LastName; } }
    }
}
