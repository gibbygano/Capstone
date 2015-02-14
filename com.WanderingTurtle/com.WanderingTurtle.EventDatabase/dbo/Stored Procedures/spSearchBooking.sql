/**********************Search for a booking without bookingID- created by: Tony Noel*******************************************/
CREATE PROCEDURE [dbo].[spSearchBooking]
	@guestID int,
	@empID int,
	@date datetime
AS
BEGIN
	SELECT * 
	FROM Booking 
	WHERE GuestID = @guestID
	AND EmployeeID = @empID
	AND DateBooked = @date
END

	RETURN @@ROWCOUNT