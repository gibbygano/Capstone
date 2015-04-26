CREATE PROCEDURE spArchiveSupplierSupplierLogin
	(
	@CompanyName 			varchar(255),
	@FirstName 				varchar(50), 
	@LastName 				varchar(50), 
	@Address1 				varchar(255),
	@Address2 				varchar(255) , 
	@Zip 					char(5), 
	@PhoneNumber 			varchar(15), 
	@EmailAddress 			varchar(100), 
	@ApplicationID 			int, 
	@SupplierID 			int
	)
AS
	DECLARE @ArchiveRowCount int

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
		AND ApplicationID = @ApplicationID
		AND Active = 1
	SET @ArchiveRowCount = @@ROWCOUNT

	IF @ArchiveRowCount = 1
	UPDATE SupplierLogin
	SET Active = 0
	WHERE SupplierLogin.SupplierID = @SupplierID
	
	RETURN @@ROWCOUNT

