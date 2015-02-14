CREATE PROCEDURE spSelectItemListing(@ItemListID int)
AS
	SELECT *
	FROM ItemListing
	WHERE ItemListID = @ItemListID
	AND Active = 1
	RETURN @@ROWCOUNT