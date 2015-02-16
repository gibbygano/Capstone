/*-------------------------------------Update------------created by: Tony Noel---------------- */
CREATE PROCEDURE [dbo].[spUpdateBooking]
	(@GuestID		int,
	@EmployeeID		int,
	@ItemListID		int,
	@Quantity		int,
	
	@original_BookingID     int,
	@original_GuestID		int,
	@original_EmployeeID	int,
	@original_ItemListID     int,
	@original_Quantity     int,
	@original_DateBooked	datetime)
AS
BEGIN
	UPDATE Booking
		SET GuestID = @GuestID,
		EmployeeID = @EmployeeID,
		ItemListID = @ItemListID,
		Quantity = @Quantity
	WHERE BookingID = @original_BookingID
		AND GuestID = @original_GuestID
		AND EmployeeID = @original_EmployeeID
		AND ItemListID = @original_ItemListID
		AND Quantity = @original_Quantity
		AND DateBooked = @original_DateBooked
END		
	RETURN @@ROWCOUNT