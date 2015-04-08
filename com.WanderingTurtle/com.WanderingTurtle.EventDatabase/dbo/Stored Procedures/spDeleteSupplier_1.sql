CREATE PROCEDURE spDeleteSupplier_1
	(
		@SupplierID 			int
	)
AS
	Delete Supplier
	WHERE supplierid = @SupplierID;
