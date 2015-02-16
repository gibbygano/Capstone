CREATE PROCEDURE [dbo].[spHotelGuestGetList]
AS
BEGIN
    SELECT [HotelGuestID],[FirstName],[LastName],[Zip],[Address1],[Address2],[PhoneNumber],[EmailAddress]
    FROM [dbo].[HotelGuest]

END