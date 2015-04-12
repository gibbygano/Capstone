/* stored procedures */

CREATE PROCEDURE spInsertSupplier
	(
	@CompanyName 	varchar(255),
	@FirstName		varchar(50), 
	@LastName 		varchar(50), 
	@Address1 		varchar(255), 
	@Address2 		varchar(255) , 
	@Zip 			char(5), 
	@PhoneNumber 	varchar(15), 
	@EmailAddress 	varchar(100), 
	@ApplicationID 	int, 
	@UserID 		int,
	@SupplyCost		decimal
	)
AS
INSERT INTO Supplier
	(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, UserID, SupplyCost, Active) 
VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @ApplicationID, @UserID, @SupplyCost, 1)
RETURN @@ROWCOUNT