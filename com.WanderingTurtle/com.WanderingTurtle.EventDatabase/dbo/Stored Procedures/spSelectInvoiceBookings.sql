/********Created by Pat Banks 2/27/2015 *********/
/************* creates a list of all bookings related to one hotel guest ***********************/

CREATE PROCEDURE spSelectInvoiceBookings
(@hotelGuestID int)
AS
	SELECT Distinct BookingID, Booking.GuestID, EmployeeID, Booking.ItemListID, Booking.Quantity, DateBooked, Discount, Booking.Active, ItemListing.Price,ItemListing.StartDate, EventItem.EventItemName
	FROM Invoice, Booking, HotelGuest, EventItem, ItemListing
	WHERE Booking.GuestID = @hotelGuestID
	AND Invoice.HotelGuestID = Booking.GuestID
	AND Booking.ItemListID = ItemListing.ItemListID
	AND ItemListing.EventItemID = EventItem.EventItemID

