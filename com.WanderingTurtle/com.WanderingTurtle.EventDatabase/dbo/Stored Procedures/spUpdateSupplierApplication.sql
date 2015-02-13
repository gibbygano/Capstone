CREATE PROCEDURE spUpdateSupplierApplication
	(
	@CompanyName 			varchar(255),
	@FirstName 				varchar(50), 
	@LastName 				varchar(50), 
	@Address1 				varchar(255),
	@Address2 				varchar(255)=NULL, 
	@Zip 					varchar(10), 
	@PhoneNumber 			varchar(15), 
	@EmailAddress 			varchar(100), 
	@ApplicationDate		[date],
	@Approved				bit,
	@ApprovalDate			[date],
	@ApplicationID 			int, 

	@originalCompanyName 	varchar(255),
	@originalFirstName 		varchar(50), 
	@originalLastName 		varchar(50), 
	@originalAddress1 		varchar(255), 
	@originalAddress2 		varchar(255)=NULL, 
	@originalZip 			varchar(10), 
	@originalPhoneNumber 	varchar(15), 
	@originalEmailAddress 	varchar(100), 
	@originalApplicationDate [date],
	@originalApproved		bit,
	@originalApprovalDate	[date],
	@originalApplicationID 	int	
	
	)
AS
	UPDATE SupplierApplication SET
		CompanyName = @CompanyName, 
		FirstName = @FirstName, 
		LastName = @LastName, 
		Address1 = @Address1, 
		Address2 = @Address2, 
		Zip = @Zip, 
		PhoneNumber = @PhoneNumber, 
		EmailAddress = @EmailAddress, 
		ApplicationDate = @ApplicationDate,
		Approved = @Approved,
		ApprovalDate = @ApprovalDate
	WHERE 
		ApplicationID = @ApplicationID
		AND CompanyName = @originalCompanyName
		AND FirstName = @originalFirstName
		AND LastName = @originalLastName
		AND Address1 = @originalAddress1
		AND Address2 = @originalAddress2
		AND Zip = @originalZip
		AND PhoneNumber = @originalPhoneNumber
		AND EmailAddress = @originalEmailAddress
		AND ApplicationDate = @originalApplicationDate
		AND Approved = @originalApproved
		AND ApprovalDate = @originalApprovalDate
		AND ApplicationID = @originalApplicationID
	RETURN @@ROWCOUNT