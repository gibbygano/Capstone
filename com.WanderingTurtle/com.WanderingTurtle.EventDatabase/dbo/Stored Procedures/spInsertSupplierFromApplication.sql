CREATE PROCEDURE [dbo].[spInsertSupplierFromApplication]
(
	@UserName				varchar(50),
	@SupplyCost				decimal(3,2),

	@CompanyName 			varchar(255),
	@CompanyDescription		varchar(255),
	@FirstName 				varchar(50), 
	@LastName 				varchar(50), 
	@Address1 				varchar(255),
	@Address2 				varchar(255), 
	@Zip 					varchar(10), 
	@PhoneNumber 			varchar(15), 
	@EmailAddress 			varchar(100), 
	@ApplicationDate		datetime,
	@ApplicationStatus		varchar(25),
	@LastStatusDate			datetime,
	@Remarks				varchar(255),

	@originalApplicationID 	int, 

	@originalCompanyName 	varchar(255),
	@originalCompanyDescription varchar(255),
	@originalFirstName 		varchar(50), 
	@originalLastName 		varchar(50), 
	@originalAddress1 		varchar(255), 
	@originalAddress2 		varchar(255), 
	@originalZip 			varchar(10), 
	@originalPhoneNumber 	varchar(15), 
	@originalEmailAddress 	varchar(100), 
	@originalApplicationDate datetime,
	
	@originalApplicationStatus	varchar(25),
	@originalLastStatusDate	datetime,
	@originalRemarks		varchar(255)	
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
		LastStatusDate = @LastStatusDate,
		Remarks = @Remarks
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
		AND LastStatusDate = @originalLastStatusDate
		AND Remarks = @originalRemarks
		AND ApplicationID = @originalApplicationID
	BEGIN

	DECLARE @rowCount int, @rowCount2 int, @supplierID int
	SET @rowCount = @@ROWCOUNT

	IF @rowCount = 1
	INSERT INTO [Supplier]
	(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, SupplyCost, Active) 
	VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @originalApplicationID, @SupplyCost, 1)
	
	SET @supplierID = @@IDENTITY
	SET @rowCount2 = @@ROWCOUNT
	
	IF @rowCount2 = 1
			INSERT INTO [SupplierLogin] (UserName, SupplierID)
			VALUES (@UserName, @SupplierID)
	RETURN @@ROWCOUNT

END
