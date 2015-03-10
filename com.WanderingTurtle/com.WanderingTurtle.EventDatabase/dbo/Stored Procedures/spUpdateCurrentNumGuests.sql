CREATE PROCEDURE [dbo].[spUpdateCurrentNumGuests]
	@ItemListID int, 
	@CurrentNumberOfGuests int,
	@original_CurrentNumberOfGuests int
AS
	UPDATE ItemListing
	SET 
		CurrentNumberOfGuests = @CurrentNumberOfGuests
	WHERE
		ItemListID = @ItemListID
		AND CurrentNumberOfGuests = @original_CurrentNumberOfGuests
	RETURN @@ROWCOUNT