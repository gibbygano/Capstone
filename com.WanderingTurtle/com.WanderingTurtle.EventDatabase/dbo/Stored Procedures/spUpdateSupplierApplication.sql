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
	@ApplicationStatus		varchar(25),
	@FinalStatusDate		[date],

	@originalCompanyName 	varchar(255),
	@originalFirstName 		varchar(50), 
	@originalLastName 		varchar(50), 
	@originalAddress1 		varchar(255), 
	@originalAddress2 		varchar(255)=NULL, 
	@originalZip 			varchar(10), 
	@originalPhoneNumber 	varchar(15), 
	@originalEmailAddress 	varchar(100), 
	@originalApplicationDate [date],
	@originalApplicationStatus	varchar(25),
	@originalFinalStatusDate	[date],
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
		ApplicationStatus = @ApplicationStatus,
		FinalStatusDate = @FinalStatusDate
	WHERE 
		 CompanyName = @originalCompanyName
		AND FirstName = @originalFirstName
		AND LastName = @originalLastName
		AND Address1 = @originalAddress1
		AND Address2 = @originalAddress2
		AND Zip = @originalZip
		AND PhoneNumber = @originalPhoneNumber
		AND EmailAddress = @originalEmailAddress
		AND ApplicationDate = @originalApplicationDate
		AND ApplicationStatus = @originalApplicationStatus
		AND FinalStatusDate = @originalFinalStatusDate
		AND ApplicationID = @originalApplicationID
	RETURN @@ROWCOUNT