CREATE PROCEDURE spInsertItemListing
	(
	@StartDate				date, 
	@EndDate				date, 
	@EventItemID		 	int, 
	@Price          		money, 
	@QuantityOffered 		int, 
	@ProductSize			varchar(50),
	@SupplierID				int,
	@CurrentNumberOfGuests int,
	@MaxNumberOfGuests int,
	@MinNumberOfGuests int
	)
AS
INSERT INTO ItemListing(StartDate, EndDate, EventItemID, Price, QuantityOffered, ProductSize, Active, SupplierID, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests) 
VALUES (@StartDate, @EndDate, @EventItemID, @Price, @QuantityOffered, @ProductSize, 1, @SupplierID, @CurrentNumberOfGuests, @MaxNumberOfGuests, @MinNumberOfGuests)
RETURN @@ROWCOUNT