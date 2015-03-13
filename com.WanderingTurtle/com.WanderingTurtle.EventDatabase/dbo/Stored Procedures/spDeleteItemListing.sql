CREATE PROCEDURE spDeleteItemListing(
	@ItemListID int, 
	@StartDate dateTime, 
	@EndDate dateTime, 
	@EventItemID int, 
	@Price money,  
	@SupplierID int,
	@CurrentNumGuests int,
	@MaxNumGuests int)
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
		AND CurrentNumberOfGuests = @CurrentNumGuests
		AND MaxNumberOfGuests = @MaxNumGuests
	RETURN @@ROWCOUNT