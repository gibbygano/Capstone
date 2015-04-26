CREATE PROCEDURE [dbo].[spSelectSupplierLoginByID]
	(
		@supplierID int
	)
AS
BEGIN
	SELECT [UserName]
	FROM [SupplierLogin]
	WHERE [SupplierID] = @supplierID	
END

