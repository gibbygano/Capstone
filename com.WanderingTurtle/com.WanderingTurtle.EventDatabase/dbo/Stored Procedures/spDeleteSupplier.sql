CREATE PROCEDURE spDeleteSupplier
	(
	@CompanyName 			varchar(255),
	@FirstName 				varchar(50), 
	@LastName 				varchar(50), 
	@Address1 				varchar(255),
	@Address2 				varchar(255) , 
	@Zip 					varchar(10), 
	@PhoneNumber 			varchar(15), 
	@EmailAddress 			varchar(100), 
	@SupplierTypeID 		int, 
	@ApplicationID 			int, 
	@UserID 				int, 
	@SupplierID 			int
	)
AS
	UPDATE Supplier SET
		Active = 0
	WHERE 
		SupplierID = @SupplierID
		AND CompanyName = @CompanyName
		AND FirstName = @FirstName
		AND LastName = @LastName
		AND Address1 = @Address1
		AND Address2 = @Address2
		AND Zip = @Zip
		AND PhoneNumber = @PhoneNumber
		AND EmailAddress = @EmailAddress
		AND SupplierTypeID = @SupplierTypeID
		AND ApplicationID = @ApplicationID
		AND UserID = @UserID
		AND Active = 1
	RETURN @@ROWCOUNT