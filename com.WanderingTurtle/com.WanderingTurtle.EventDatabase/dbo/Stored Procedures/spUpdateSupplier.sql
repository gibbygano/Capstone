CREATE PROCEDURE spUpdateSupplier
	(
	@CompanyName 			varchar(255),
	@FirstName 				varchar(50), 
	@LastName 				varchar(50), 
	@Address1 				varchar(255),
	@Address2 				varchar(255), 
	@Zip 					char(5), 
	@PhoneNumber 			varchar(15), 
	@EmailAddress 			varchar(100), 
	@ApplicationID 			int, 
	@UserID 				int, 
	@SupplierID 			int,
	@SupplyCost				decimal(3,2), 
	@originalCompanyName 	varchar(255),
	@originalFirstName 		varchar(50), 
	@originalLastName 		varchar(50), 
	@originalAddress1 		varchar(255), 
	@originalAddress2 		varchar(255), 
	@originalZip 			char(5), 
	@originalPhoneNumber 	varchar(15), 
	@originalEmailAddress 	varchar(100), 
	@originalApplicationID 	int, 
	@originalUserID 		int,
	@originalSupplyCost		decimal(3,2)
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
		ApplicationID = @ApplicationID, 
		UserID = @UserID,
		SupplyCost = @SupplyCost
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
		AND ApplicationID = @originalApplicationID
		AND UserID = @originalUserID
		AND SupplyCost = @originalSupplyCost
		AND Active = 1
	RETURN @@ROWCOUNT