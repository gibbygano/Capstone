CREATE PROCEDURE spSelectInvoiceByGuest
	(@guestID int)
AS
BEGIN
	SELECT InvoiceID, Invoice.HotelGuestID, DateOpened, Invoice.Active, HotelGuest.LastName, HotelGuest.FirstName, HotelGuest.Room
	FROM Invoice, HotelGuest
	WHERE Invoice.HotelGuestID = @guestID
	AND Invoice.HotelGuestID = HotelGuest.HotelGuestID

END