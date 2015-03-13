CREATE PROCEDURE spUpdateItemListing(
	@StartDate datetime,
	@EndDate datetime,
	@ItemListID int,
	@EventItemID int,
	@Price money, 
	@MaxNumberOfGuests int,
	@CurrentNumberOfGuests int,
	@SupplierID int,	
	@originalStartDate datetime,
	@originalEndDate datetime, 
	@originalEventItemID int,
	@originalPrice money, 
	@originalSupplierID int,
	@originalCurrentNumberOfGuests int,
	@originalMaxNumberOfGuests int)
AS
	UPDATE ItemListing
	SET 
		StartDate = @StartDate,
		EndDate = @EndDate,
		EventItemID = @EventItemID,
		Price = @Price,
		SupplierID = @SupplierID,
		CurrentNumberOfGuests = @CurrentNumberOfGuests,
		MaxNumberOfGuests = @MaxNumberOfGuests
	WHERE
		ItemListID = @ItemListID
		AND StartDate = @originalStartDate
		AND EndDate = @originalEndDate
		AND EventItemID = @originalEventItemID
		AND Price = @originalPrice
		AND SupplierID = @originalSupplierID
		AND CurrentNumberOfGuests = @originalCurrentNumberOfGuests
		AND MaxNumberOfGuests = @originalMaxNumberOfGuests

	RETURN @@ROWCOUNT