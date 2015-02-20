CREATE PROCEDURE spInsertItemListing
	(
	@StartDate				date, 
	@EndDate				date, 
	@EventItemID		 	int, 
	@Price          		money, 
	@QuantityOffered 		int, 
	@ProductSize			varchar(50)
	)
AS
INSERT INTO ItemListing(StartDate, EndDate, EventItemID, Price, QuantityOffered, ProductSize, Active) 
VALUES (@StartDate, @EndDate, @EventItemID, @Price, @QuantityOffered, @ProductSize, 1)
RETURN @@ROWCOUNT