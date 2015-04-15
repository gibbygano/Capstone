CREATE PROCEDURE [dbo].[spDeleteTestSupplier]

        @UserID          INT
AS
        DELETE FROM Supplier
        WHERE
        UserID = @UserID
		RETURN @@ROWCOUNT