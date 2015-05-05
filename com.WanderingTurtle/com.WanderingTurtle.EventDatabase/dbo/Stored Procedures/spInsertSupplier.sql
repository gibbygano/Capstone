/* stored procedures */

CREATE PROCEDURE spInsertSupplier
	(
	@UserName		varchar(50),

	@CompanyName 	varchar(255),
	@FirstName		varchar(50), 
	@LastName 		varchar(50), 
	@Address1 		varchar(255), 
	@Address2 		varchar(255) , 
	@Zip 			char(5), 
	@PhoneNumber 	varchar(15), 
	@EmailAddress 	varchar(100), 
	@ApplicationID 	int, 
	@SupplyCost		decimal(3,2),
	@password		varchar(256)
	)
AS
DECLARE @rowCount int, @supplierID int
	INSERT INTO Supplier
		(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, SupplyCost, Active) 
	VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @ApplicationID, @SupplyCost, 1)

	SET @supplierID = @@IDENTITY

	SET @rowCount = @@ROWCOUNT
	IF @rowCount = 1
			INSERT INTO [SupplierLogin] (UserName, SupplierID, UserPassword)
			VALUES (@UserName, @SupplierID, @password)
	RETURN @@ROWCOUNT