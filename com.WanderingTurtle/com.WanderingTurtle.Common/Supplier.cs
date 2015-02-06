using System;

public class Supplier
{
    int SupplierID      { get; set; }
    string CompanyName  { get; set; }
    string FirstName    { get; set; }
    string LastName     { get; set; }
    string Address1     { get; set; }
    string Address2     { get; set; }
    string Zip          { get; set; }
    string PhoneNumber  { get; set; }
    string EmailAddress { get; set; }
    int ApplicationID   { get; set; }
    int UserID          { get; set; }
    bool Active         { get; set; }

    public Supplier(int supplierID, string companyName, string firstName, string lastName, string address1,
        string address2, string zip, string phoneNumber, string emailAddress, int applicationID, int userID, bool active)
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
        UserID = userID;
        Active = active;
    }

    public Supplier()
    {

    }
}
