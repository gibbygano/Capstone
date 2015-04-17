CREATE PROCEDURE [dbo].[spDeleteTestSupplier]

        @SupplierID          INT
AS
        DELETE FROM Supplier
        WHERE
        SupplierID = @SupplierID
		RETURN @@ROWCOUNT