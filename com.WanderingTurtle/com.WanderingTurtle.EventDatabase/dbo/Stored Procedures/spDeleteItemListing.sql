CREATE PROCEDURE spDeleteItemListing(
	@ItemListID int, 
	@StartDate date, 
	@EndDate date, 
	@EventItemID int, 
	@Price money,  
	@SupplierID int,
	@CurrentNumberOfGuests int,
	@MaxNumberOfGuests int,
	@MinNumberOfGuests int)
AS
	UPDATE ItemListing
	SET Active = 0
	WHERE
		ItemListID = @ItemListID
		AND StartDate = @StartDate
		AND EndDate = @EndDate
		AND EventItemID = @EventItemID
		AND Price = @Price
		AND SupplierID = @SupplierID
		AND CurrentNumberOfGuests = @CurrentNumberOfGuests
		AND MaxNumberOfGuests = @MaxNumberOfGuests
		AND MinNumberOfGuests = @MinNumberOfGuests
	RETURN @@ROWCOUNT