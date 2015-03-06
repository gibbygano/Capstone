CREATE PROCEDURE spUpdateItemListing(
	@ItemListID int, 
	@StartDate date,
	@EndDate date, 
	@EventStartTime dateTime2,
	@EventEndTime dateTime2,
	@SupplierID int,
	@EventItemID int,
	@Price money, 
	@QuantityOffered int, 
	@ProductSize varchar,
	@CurrentNumberOfGuests int,
	@MaxNumberOfGuests int,
	@MinNumberOfGuests int,
	@originalStartDate date,
	@originalEndDate date, 
	@originalEventStartTime dateTime2,
	@originalEventEndTime dateTime2,
	@originalSupplierID int,
	@originalEventItemID int,
	@originalPrice money, 
	@originalQuantityOffered int, 
	@originalProductSize varchar,
	@originalCurrentNumberOfGuests int,
	@originalMaxNumberOfGuests int,
	@originalMinNumberOfGuests int)
AS
	UPDATE ItemListing
	SET 
		StartDate = @StartDate,
		EndDate = @EndDate,
		EventItemID = @EventItemID,
		Price = @Price,
		QuantityOffered = @QuantityOffered,
		ProductSize = @ProductSize,
		SupplierID = @SupplierID,
		CurrentNumberOfGuests = @CurrentNumberOfGuests,
		MaxNumberOfGuests = @MaxNumberOfGuests,
		MinNumberOfGuests = @MinNumberOfGuests
	WHERE
		ItemListID = @ItemListID
		AND StartDate = @originalStartDate
		AND EndDate = @originalEndDate
		AND EventItemID = @originalEventItemID
		AND Price = @originalPrice
		AND QuantityOffered = @originalQuantityOffered
		AND ProductSize = @originalProductSize
		AND SupplierID = @originalSupplierID
		AND CurrentNumberOfGuests = @originalCurrentNumberOfGuests
		AND MaxNumberOfGuests = @originalMaxNumberOfGuests
		AND MinNumberOfGuests = @originalMinNumberOfGuests
		AND Active = 1
	RETURN @@ROWCOUNT