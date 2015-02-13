CREATE PROCEDURE [dbo].[spHotelGuestGet]
    @hotelGuestID 	int
AS
BEGIN
    SELECT [HotelGuestID],[FirstName],[LastName],[Zip],[Address1],[Address2],[PhoneNumber],[EmailAddress],[HotelGuestPIN]
    FROM [dbo].[HotelGuest]
    WHERE [HotelGuestID] = @hotelGuestID

END