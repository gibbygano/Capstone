CREATE PROCEDURE [dbo].[spInsertSupplierLogin](
	@userName varchar(50),
	@supplierID int,
	@password varchar(256)
	)
AS
BEGIN
	INSERT INTO [SupplierLogin] (UserName, SupplierID, UserPassword)
	VALUES (@userName, @supplierID, @password)
	
	RETURN @@ROWCOUNT
END