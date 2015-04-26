CREATE PROCEDURE [dbo].[spSelectHotelGuest]
	@hotelGuestID int = null
AS
BEGIN
    SELECT [HotelGuestID],[FirstName],[LastName],[Address1],[Address2],[HotelGuest].[Zip],[City],[State],[PhoneNumber],[EmailAddress],[Room], [GuestPIN], [Active]
    FROM [dbo].[HotelGuest], [dbo].[CityState]
	WHERE [HotelGuest].[Zip] = [CityState].[Zip]
		AND (@hotelGuestID IS NULL OR ([HotelGuestID] = @hotelGuestID))

	RETURN @@ROWCOUNT

END