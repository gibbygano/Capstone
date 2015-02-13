CREATE PROCEDURE spInsertSupplierApplication
	(
	@CompanyName 	varchar(255),
	@FirstName		varchar(50), 
	@LastName 		varchar(50), 
	@Address1 		varchar(255), 
	@Address2 		varchar(255)=NULL, 
	@Zip 			varchar(10), 
	@PhoneNumber 	varchar(15), 
	@EmailAddress 	varchar(100), 
	@ApplicationDate date, 
	@Approved		bit,
	@ApprovalDate	date
	)
AS
INSERT INTO SupplierApplication
	(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate) 
VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @ApplicationDate, @Approved, @ApprovalDate)
RETURN @@ROWCOUNT