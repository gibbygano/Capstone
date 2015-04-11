CREATE PROCEDURE spInsertSupplierApplication
	(
	@CompanyName 	varchar(255),
	@FirstName		varchar(50), 
	@LastName 		varchar(50),
	@CompanyDescription varchar(255), 
	@Address1 		varchar(255), 
	@Address2 		varchar(255)=NULL, 
	@Zip 			varchar(10), 
	@PhoneNumber 	varchar(15), 
	@EmailAddress 	varchar(100), 
	@ApplicationDate dateTime,
	@ApplicationStatus	varchar(25),
	@LastStatusDate	datetime,
	@Remarks		varchar(255) = null
	)
AS
INSERT INTO SupplierApplication
	(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, ApplicationStatus, LastStatusDate, CompanyDescription, Remarks) 
VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @ApplicationDate, @ApplicationStatus, @LastStatusDate, @CompanyDescription, @Remarks);
return @@ROWCOUNT

