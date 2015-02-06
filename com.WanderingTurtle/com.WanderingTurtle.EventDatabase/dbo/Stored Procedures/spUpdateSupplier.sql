CREATE PROCEDURE spUpdateSupplier
	(
	@CompanyName 			varchar(255),
	@FirstName 				varchar(50), 
	@LastName 				varchar(50), 
	@Address1 				varchar(255),
	@Address2 				varchar(255), 
	@Zip 					varchar(10), 
	@PhoneNumber 			varchar(15), 
	@EmailAddress 			varchar(100), 
	@SupplierTypeID 		int, 
	@ApplicationID 			int, 
	@UserID 				int, 
	@SupplierID 			int, 
	@originalCompanyName 	varchar(255),
	@originalFirstName 		varchar(50), 
	@originalLastName 		varchar(50), 
	@originalAddress1 		varchar(255), 
	@originalAddress2 		varchar(255), 
	@originalZip 			varchar(10), 
	@originalPhoneNumber 	varchar(15), 
	@originalEmailAddress 	varchar(100), 
	@originalSupplierTypeID int, 
	@originalApplicationID 	int, 
	@originalUserID 		int
	)
AS
	UPDATE Supplier SET
		CompanyName = @CompanyName, 
		FirstName = @FirstName, 
		LastName = @LastName, 
		Address1 = @Address1, 
		Address2 = @Address2, 
		Zip = @Zip, 
		PhoneNumber = @PhoneNumber, 
		EmailAddress = @EmailAddress, 
		SupplierTypeID = @SupplierTypeID, 
		ApplicationID = @ApplicationID, 
		UserID = @UserID
	WHERE 
		SupplierID = @SupplierID
		AND CompanyName = @originalCompanyName
		AND FirstName = @originalFirstName
		AND LastName = @originalLastName
		AND Address1 = @originalAddress1
		AND Address2 = @originalAddress2
		AND Zip = @originalZip
		AND PhoneNumber = @originalPhoneNumber
		AND EmailAddress = @originalEmailAddress
		AND SupplierTypeID = @originalSupplierTypeID
		AND ApplicationID = @originalApplicationID
		AND UserID = @originalUserID
		AND Active = 1
	RETURN @@ROWCOUNT