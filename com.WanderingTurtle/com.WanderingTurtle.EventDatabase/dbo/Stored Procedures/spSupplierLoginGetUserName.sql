CREATE PROCEDURE [dbo].[spSupplierLoginGetUserName]
	(
		@userName varchar(50)
	)
AS
BEGIN
	SELECT [UserName]
	FROM [SupplierLogin]
	WHERE [UserName] = @userName	
END
