CREATE PROCEDURE spDeleteItemListing(@ItemListID int, @StartDate date, @EndDate date, @EventItemID int, @Price money, @QuantityOffered int, @ProductSize varChar, @SupplierID int, @EventStartTime dateTime2, @EventEndTime dateTime2)
AS
	UPDATE ItemListing
	SET Active = 0
	WHERE
		ItemListID = @ItemListID
		AND StartDate = @StartDate
		AND EndDate = @EndDate
		AND EventItemID = @EventItemID
		AND Price = @Price
		AND QuantityOffered = @QuantityOffered
		AND ProductSize = @ProductSize
		AND SupplierID = @SupplierID
		AND EventStartTime = @EventStartTime
		AND EventEndTime = @EventEndTime
	RETURN @@ROWCOUNT