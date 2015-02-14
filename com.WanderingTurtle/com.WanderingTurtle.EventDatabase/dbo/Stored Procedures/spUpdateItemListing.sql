CREATE PROCEDURE spUpdateItemListing(@ItemListID int, @StartDate date, @EndDate date, @EventItemID int, @Price money, @QuantityOffered int, @ProductSize varchar)
AS
	UPDATE ItemListing
	SET 
		StartDate = @StartDate,
		EndDate = @EndDate,
		EventItemID = @EventItemID,
		Price = @Price,
		QuantityOffered = @QuantityOffered,
		ProductSize = @ProductSize
	WHERE
		ItemListID = @ItemListID
		AND StartDate = @StartDate
		AND EndDate = @EndDate
		AND EventItemID = @EventItemID
		AND Price = @Price
		AND QuantityOffered = @QuantityOffered
		AND ProductSize = @ProductSize
		AND Active = 1
	RETURN @@ROWCOUNT