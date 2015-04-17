using System;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Matt Lapka
    /// Created: 2015/02/08
    /// Object to hold information pertaining to a Supplier Application
    /// </summary>
    public class SupplierApplication
    {
        public int ApplicationID { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Zip { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }

        public string Remarks { get; set; }

        public SupplierApplication()
        {
            //default
        }

        public SupplierApplication(int applicationID, string companyName, string companyDescription, string firstName, string lastName, string address1, string address2, string zip, string phoneNumber, string emailAddress, DateTime applicationDate, string applicationStatus, DateTime lastStatusDate, String remarks)
        {
            ApplicationID = applicationID;
            ApplicationDate = applicationDate;
            CompanyName = companyName;
            CompanyDescription = companyDescription;
            FirstName = firstName;
            LastName = lastName;
            Address1 = address1;
            Address2 = address2;
            Zip = zip;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            Remarks = remarks;
        }

        //constructor for use with web application submittal
        public SupplierApplication(string companyName, string companyDescription, string firstName, string lastName, string address1, string address2, string zip, string phoneNumber, string emailAddress, DateTime applicationDate)
        {
            ApplicationDate = applicationDate;
            CompanyName = companyName;
            CompanyDescription = companyDescription;
            FirstName = firstName;
            LastName = lastName;
            Address1 = address1;
            Address2 = address2;
            Zip = zip;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            ApplicationStatus = "Pending";
            LastStatusDate = DateTime.Now;
            Remarks = "";
        }
    }
}