--STORED PROCEDURES
CREATE PROCEDURE [dbo].[spSelectSupplierApplication] 
	@ApplicationID int
AS
	SELECT ApplicationID, CompanyName, CompanyDescription, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate 
	FROM SupplierApplication
	WHERE ApplicationID = @ApplicationID