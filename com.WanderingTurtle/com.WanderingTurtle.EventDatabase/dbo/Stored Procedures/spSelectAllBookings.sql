/****************************created by: Tony Noel************************************/

CREATE PROCEDURE [dbo].[spSelectAllBookings]
AS
BEGIN
	SELECT BookingID, GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Cancel, Refund, Active
	FROM Booking 
END

	RETURN @@ROWCOUNT