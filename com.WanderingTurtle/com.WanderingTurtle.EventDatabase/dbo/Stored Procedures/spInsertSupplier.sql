/* stored procedures */

CREATE PROCEDURE spInsertSupplier
	(
	@CompanyName 	varchar(255),
	@FirstName		varchar(50), 
	@LastName 		varchar(50), 
	@Address1 		varchar(255), 
	@Address2 		varchar(255) , 
	@Zip 			varchar(10), 
	@PhoneNumber 	varchar(15), 
	@EmailAddress 	varchar(100), 
	@SupplierTypeID int, 
	@ApplicationID 	int, 
	@UserID 		int, 
	@SupplierID 	int OUTPUT
	)
AS
INSERT INTO Supplier
	(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, SupplierTypeID, ApplicationID, UserID, Active) 
VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @SupplierTypeID, @ApplicationID, @UserID, 1)
SET @SupplierID = SCOPE_IDENTITY()
RETURN @@ERROR