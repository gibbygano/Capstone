/*-------------------------------------Update------------created by: Tony Noel---------------- */
CREATE PROCEDURE [dbo].[spUpdateBooking]
	(@Quantity		int,
	@Refund			decimal,
	@Cancel			bit,
	@Active			bit,
	
	@original_BookingID     int,
	@original_GuestID		int,
	@original_EmployeeID	int,
	@original_ItemListID     int,
	@original_Quantity     int,
	@original_DateBooked	datetime,
	@original_Refund			decimal,
	@original_Cancel			bit,
	@original_Active            bit)
AS
BEGIN
	UPDATE Booking
		SET Quantity = @Quantity,
			Cancel = @Cancel,
			Refund = @Refund,
			Active = @Active
	WHERE BookingID = @original_BookingID
		AND GuestID = @original_GuestID
		AND EmployeeID = @original_EmployeeID
		AND ItemListID = @original_ItemListID
		AND Quantity = @original_Quantity
		AND DateBooked = @original_DateBooked
		AND Cancel = @original_Cancel
		AND Refund = @original_Refund
		AND Active = @original_Active
END		
	RETURN @@ROWCOUNT