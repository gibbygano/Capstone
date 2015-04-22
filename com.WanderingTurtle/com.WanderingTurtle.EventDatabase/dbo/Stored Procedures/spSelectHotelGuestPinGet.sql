CREATE PROCEDURE [dbo].[spSelectHotelGuestPinGet]
	(
		@guestPIN char(5)
	)
AS

BEGIN
    SELECT [HotelGuestID],[FirstName],[LastName],[Address1],[Address2],[HotelGuest].[Zip],[City],[State],[PhoneNumber],[EmailAddress],[Room], [GuestPIN], [Active]
    FROM [dbo].[HotelGuest], [dbo].[CityState]
	WHERE [HotelGuest].[Zip] = [CityState].[Zip]
		AND [GuestPIN] = @guestPIN
END

