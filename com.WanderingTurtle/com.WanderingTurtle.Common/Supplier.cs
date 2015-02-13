using System;

public class Supplier
{
    public int SupplierID      { get; set; }
    public string CompanyName  { get; set; }
    public string FirstName    { get; set; }
    public string LastName     { get; set; }
    public string Address1     { get; set; }
    public string Address2     { get; set; }
    public string Zip          { get; set; }
    public string PhoneNumber  { get; set; }
    public string EmailAddress { get; set; }
    public int ApplicationID   { get; set; }
    public int UserID          { get; set; }
    public bool Active         { get; set; }

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
