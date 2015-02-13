using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Obejct to hold information pertaining to a Supplier Application
    /// </summary>
    /// Created by Matt Lapka 2/8/15
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
        public bool Approved { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public SupplierApplication()
        {
            //default
        }
        public SupplierApplication(int applicationID, string companyName, string companyDescription, string firstName, string lastName, string address1, string address2, string zip, string phoneNumber, string emailAddress, DateTime applicationDate, bool approved, DateTime approvalDate)
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
            Approved = approved;
            ApprovalDate = approvalDate;

        }

    }
}
