/*-------------------------------------Update------------created by: Tony Noel---------------- */
CREATE PROCEDURE [dbo].[spUpdateBookingUpdateListingNum]
	(@Quantity		int,
	@Discount			decimal(3,2),
	@Active			bit,
	@TicketPrice decimal(8,2),
	@ExtendedPrice	decimal(12,2),
	@TotalCharge		decimal(12,2),
	
	@original_BookingID     int,
	@original_GuestID		int,
	@original_EmployeeID	int,
	@original_ItemListID     int,
	@original_Quantity     int,
	@original_DateBooked	datetime,
	@original_Discount			decimal(3,2),
	@original_Active            bit,
	@original_TicketPrice	decimal(8,2),
	@original_ExtendedPrice decimal(12,2),
	@original_TotalCharge	decimal(12,2))
AS
BEGIN

	DECLARE @bookingTotal int, @bookingRowCount int

	UPDATE Booking
		SET Quantity = @Quantity,
			Discount = @Discount,
			Active = @Active,
			TicketPrice = @TicketPrice,
			ExtendedPrice = @ExtendedPrice,
			TotalCharge = @TotalCharge
	WHERE BookingID = @original_BookingID
		AND GuestID = @original_GuestID
		AND EmployeeID = @original_EmployeeID
		AND ItemListID = @original_ItemListID
		AND Quantity = @original_Quantity
		AND DateBooked = @original_DateBooked
		AND Discount = @original_Discount
		AND Active = @original_Active
		AND TicketPrice = @original_TicketPrice
		AND ExtendedPrice = @original_ExtendedPrice
		AND TotalCharge = @original_TotalCharge	
	
	SET @bookingRowCount = @@ROWCOUNT
	IF @bookingRowCount = 1

	SELECT @bookingTotal = SUM(Quantity)
	FROM Booking
	WHERE Booking.ItemListID = @original_ItemListID
	
	UPDATE ItemListing
		SET CurrentNumberOfGuests = @bookingTotal  
		WHERE ItemListing.ItemListID = @original_ItemListID

RETURN @@ROWCOUNT
END