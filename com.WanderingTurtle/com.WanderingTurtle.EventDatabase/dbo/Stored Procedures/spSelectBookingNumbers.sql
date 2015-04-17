CREATE PROCEDURE spSelectBookingNumbers
 @ItemListID		int
AS
BEGIN
select FirstName, LastName, Room, Quantity from HotelGuest join Booking on HotelGuest.HotelGuestID = Booking.GuestID where ItemListID = @ItemListID AND Booking.Active = 1
END