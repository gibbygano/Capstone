/* -------------------------Booking Stored Procedures ---------------------------created by: Tony Noel*/
CREATE PROCEDURE [dbo].[spAddBooking]
	@GuestID		int,
	@EmployeeID		int,
	@ItemListID		int,
	@Quantity		int,
	@DateBooked		datetime
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Booking(GuestID, EmployeeID, ItemListID, Quantity, DateBooked)
    VALUES(@GuestID, @EmployeeID, @ItemListID, @Quantity, @DateBooked)

END