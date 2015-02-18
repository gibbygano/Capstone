CREATE PROCEDURE spSelectAllSuppliers
AS
	SELECT SupplierID, CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, UserID
	FROM Supplier
	WHERE Active = 1
	RETURN @@ROWCOUNT