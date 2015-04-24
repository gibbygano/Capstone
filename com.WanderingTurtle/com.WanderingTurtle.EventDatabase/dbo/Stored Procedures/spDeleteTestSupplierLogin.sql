CREATE PROCEDURE [dbo].[spDeleteTestSupplierLogin]

        @Username          varchar(20)
AS
        DELETE FROM SupplierLogin
        WHERE
        UserName = @Username
		RETURN @@ROWCOUNT