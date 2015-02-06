/* -------------------------Booking Stored Procedures ----------------------------------*/
CREATE PROCEDURE [dbo].[spAddBooking]
    @BookingID 	int,
	@GuestID		int,
	@EmployeeID		int,
	@DateBooked		datetime
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Booking(BookingID, GuestID, EmployeeID, DateBooked)
    VALUES(@BookingID, @GuestID, @EmployeeID, @DateBooked)

END