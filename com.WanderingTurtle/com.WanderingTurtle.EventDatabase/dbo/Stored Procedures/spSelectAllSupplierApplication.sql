CREATE PROCEDURE [dbo].[spSelectAllSupplierApplication] 
AS
SELECT ApplicationID, CompanyName, CompanyDescription, FirstName, LastName, 
Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, 
ApplicationStatus, LastStatusDate, Remarks
FROM SupplierApplication
WHERE ApplicationStatus = 'Pending';