/*-------------------------------------Update/Delete---------------------------- */
CREATE PROCEDURE [dbo].[spUpdateBooking]
	(@GuestID		int,
	@EmployeeID		int,
	@DateBooked		datetime,
	
	@original_BookingID     int,
	@original_GuestID		int,
	@original_EmployeeID	int,
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
		AND DateBooked = @original_DateBooked
END		
	RETURN @@ROWCOUNT