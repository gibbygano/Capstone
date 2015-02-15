CREATE PROCEDURE spSelectAllItemListings
AS
	SELECT *
	FROM ItemListing
	WHERE Active = 1
	RETURN @@ROWCOUNT