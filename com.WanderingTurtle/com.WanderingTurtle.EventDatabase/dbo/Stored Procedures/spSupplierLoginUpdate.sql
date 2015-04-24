CREATE PROCEDURE [dbo].[spSupplierLoginUpdate]
	(
	@Password	varchar(50),
	
	@original_UserName	varchar(50),
	@original_Password	varchar(50),
	@original_SupplierID int
)
AS
BEGIN
	UPDATE SupplierLogin
		SET 
			[UserPassword] = @Password
	WHERE 
		UserName = @original_UserName
		AND [UserPassword] = @original_Password
		AND SupplierID = @original_SupplierID
END		
	RETURN @@ROWCOUNT