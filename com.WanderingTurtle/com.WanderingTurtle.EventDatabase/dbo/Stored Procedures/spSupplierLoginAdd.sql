CREATE PROCEDURE [dbo].[spSupplierLoginAdd](
	@userName varchar(50)
	)
AS
BEGIN
	INSERT INTO [SupplierLogin] (UserName)
	VALUES (@userName)
	
	RETURN @@ROWCOUNT
END