/*-------------------------------------Update------------created by: Tony Noel---------------- */
CREATE PROCEDURE [dbo].[spUpdateBooking]
	(@Quantity		int,
	@Discount			decimal,
	@Active			bit,
	@TicketPrice decimal,
	@ExtendedPrice	decimal,
	@TotalCharge		decimal,
	
	@original_BookingID     int,
	@original_GuestID		int,
	@original_EmployeeID	int,
	@original_ItemListID     int,
	@original_Quantity     int,
	@original_DateBooked	datetime,
	@original_Discount			decimal,
	@original_Active            bit,
	@original_TicketPrice	decimal,
	@original_ExtendedPrice decimal,
	@original_TotalCharge	decimal)
AS
BEGIN
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
END		
	RETURN @@ROWCOUNT