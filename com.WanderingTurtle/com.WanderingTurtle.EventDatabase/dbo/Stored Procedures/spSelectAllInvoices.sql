CREATE PROCEDURE spSelectAllInvoices
AS
BEGIN
	SELECT InvoiceID, Invoice.HotelGuestID, DateOpened, Invoice.Active, HotelGuest.LastName, HotelGuest.FirstName
	FROM Invoice, HotelGuest
	WHERE Invoice.Active = 1
	AND Invoice.HotelGuestID = HotelGuest.HotelGuestID

END