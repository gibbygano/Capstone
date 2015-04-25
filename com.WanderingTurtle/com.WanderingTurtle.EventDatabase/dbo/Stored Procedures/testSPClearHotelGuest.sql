Create procedure [dbo].[testSPClearHotelGuest]
AS
BEGIN
DECLARE @invoice int

SET @invoice = (Select InvoiceID FROM Invoice, HotelGuest WHERE dbo.HotelGuest.FirstName = 'Fake' AND dbo.HotelGuest.PhoneNumber = '5556667777' AND dbo.HotelGuest.HotelGuestID = dbo.Invoice.HotelGuestID)
DELETE FROM Invoice WHERE Invoice.InvoiceID = @invoice;
END