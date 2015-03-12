/* -------------------------Booking Stored Procedures ---------------------------created by: Tony Noel*/
CREATE PROCEDURE [dbo].[spAddBooking]
	@GuestID		int,
	@EmployeeID		int,
	@ItemListID		int,
	@Quantity		int,
	@DateBooked		DateTime,
	@Discount		Decimal,
	@TicketPrice	Decimal,
	@ExtendedPrice	Decimal,
	@TotalCharge	Decimal
	
AS
    INSERT INTO Booking(GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Discount, TicketPrice, ExtendedPrice, TotalCharge)
    VALUES(@GuestID, @EmployeeID, @ItemListID, @Quantity, @DateBooked, @Discount, @TicketPrice, @ExtendedPrice, @TotalCharge)
RETURN @@ROWCOUNT