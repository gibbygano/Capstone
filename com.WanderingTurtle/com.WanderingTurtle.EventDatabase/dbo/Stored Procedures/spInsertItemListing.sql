CREATE PROCEDURE spInsertItemListing
	(
	@StartDate				date, 
	@EndDate				date, 
	@EventItemID		 	int, 
	@Price          		money, 
	@SupplierID				int,
	@CurrentNumberOfGuests int,
	@MaxNumberOfGuests int,
	@MinNumberOfGuests int
	)
AS
INSERT INTO ItemListing(StartDate, EndDate, EventItemID, Price, Active, SupplierID, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests) 
VALUES (@StartDate, @EndDate, @EventItemID, @Price, 1, @SupplierID, @CurrentNumberOfGuests, @MaxNumberOfGuests, @MinNumberOfGuests)
RETURN @@ROWCOUNT