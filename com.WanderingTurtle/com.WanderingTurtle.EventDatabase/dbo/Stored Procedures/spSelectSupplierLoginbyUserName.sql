CREATE PROCEDURE [dbo].[spSelectSupplierLoginbyUserName]
	(
		@userName varchar(50)
	)
AS
BEGIN
	SELECT [UserName]
	FROM [SupplierLogin]
	WHERE [UserName] = @userName	
END
