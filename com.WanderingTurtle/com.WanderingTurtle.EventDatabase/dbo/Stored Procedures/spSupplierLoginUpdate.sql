CREATE PROCEDURE [dbo].[spSupplierLoginUpdate]
	(
	@UserName		varchar(50),
	
	@original_UserName	varchar(50),
	@original_SupplierID int
)
AS
BEGIN
	UPDATE SupplierLogin
		SET 
			UserName = @UserName
	WHERE 
		UserName = @original_UserName
		AND SupplierID = @original_SupplierID
END		
	RETURN @@ROWCOUNT