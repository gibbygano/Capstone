CREATE PROCEDURE [dbo].[spSelectArchivedSupplierLoginTest](
	@userPassword varchar(50),
	@userName varchar(50)
	)
AS
BEGIN
	SELECT [UserID], [UserPassword], [UserName], [SupplierID], [Active]
	FROM [SupplierLogin]
	WHERE [Active] = 0
		AND [UserPassword] = @userPassword
		AND [UserName] = @userName
		
END
