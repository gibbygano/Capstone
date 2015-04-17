CREATE PROCEDURE [dbo].[spDeleteTestApplication]
(
	@CompanyName		varchar(50)
)
AS
	DELETE FROM SupplierApplication
	WHERE CompanyName = @CompanyName

