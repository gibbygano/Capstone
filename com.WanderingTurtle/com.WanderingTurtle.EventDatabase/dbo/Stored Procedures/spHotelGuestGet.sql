CREATE PROCEDURE [dbo].[spHotelGuestGet]
	@hotelGuestID int = null
AS
BEGIN
    SELECT [HotelGuestID],[FirstName],[LastName],[Address1],[Address2],[HotelGuest].[Zip],[City],[State],[PhoneNumber],[EmailAddress],[Room], [Active]
    FROM [dbo].[HotelGuest], [dbo].[CityState]
	WHERE [HotelGuest].[Zip] = [CityState].[Zip]
		AND (@hotelGuestID IS NULL OR ([HotelGuestID] = @hotelGuestID))

	RETURN @@ROWCOUNT

END