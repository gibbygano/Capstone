CREATE PROCEDURE spUpdateItemListing(
	@ItemListID int, 
	@StartDate date,
	@EndDate date, 
	@EventItemID int,
	@Price money, 
	@QuantityOffered int, 
	@ProductSize varchar,
	@originalStartDate date,
	@originalEndDate date, 
	@originalEventItemID int,
	@originalPrice money, 
	@originalQuantityOffered int, 
	@originalProductSize varchar)
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
		AND StartDate = @originalStartDate
		AND EndDate = @originalEndDate
		AND EventItemID = @originalEventItemID
		AND Price = @originalPrice
		AND QuantityOffered = @originalQuantityOffered
		AND ProductSize = @originalProductSize
		AND Active = 1
	RETURN @@ROWCOUNT