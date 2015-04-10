CREATE PROCEDURE [dbo].[spSelectAllSupplierApplication] 
AS
SELECT ApplicationID, CompanyName, CompanyDescription, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate
FROM SupplierApplication
WHERE Approved = 0;