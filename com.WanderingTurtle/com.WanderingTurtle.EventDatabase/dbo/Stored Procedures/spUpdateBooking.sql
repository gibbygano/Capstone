/*-------------------------------------Update------------created by: Tony Noel---------------- */
CREATE PROCEDURE [dbo].[spUpdateBooking]
	(@GuestID		int,
	@EmployeeID		int,
	@DateBooked		datetime,
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
		DateBooked = @DateBooked
	WHERE BookingID = @original_BookingID
		AND GuestID = @original_GuestID
		AND EmployeeID = @original_EmployeeID
		AND ItemListID = @original_EmployeeID
		AND Quantity = @original_EmployeeID
		AND DateBooked = @original_DateBooked
END		
	RETURN @@ROWCOUNT