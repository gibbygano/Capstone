CREATE PROCEDURE [dbo].[spSupplierLoginGetByID]
	(
		@supplierID int
	)
AS
BEGIN
	SELECT [UserName]
	FROM [SupplierLogin]
	WHERE [SupplierID] = @supplierID	
END

