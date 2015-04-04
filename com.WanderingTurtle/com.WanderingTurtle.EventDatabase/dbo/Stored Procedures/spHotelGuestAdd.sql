/* -------------------------Add Hotel Guest Stored Procedures ---------------------------created by: Miguel Santana*/
/* Added insert into Invoice table for when a hotel guest checks in - Pat Banks  02/27/15 */

CREATE PROCEDURE [dbo].[spHotelGuestAdd]
	@firstName varchar(50),
	@lastName varchar(50),
	@zip varchar(10),
	@address1 varchar(255),
	@address2 varchar(255),
	@phoneNumber varchar(15),
	@email varchar(100),
	@room int,
	@guestPIN int
AS
BEGIN
	DECLARE @guestID int, @rowCount int
	INSERT INTO [HotelGuest] ([FirstName],[LastName],[Zip],[Address1],[Address2],[PhoneNumber],[EmailAddress], [Room], [GuestPIN]) 
	VALUES (@firstName, @lastName, @zip, @address1, @address2, @phoneNumber, @email, @room, @guestPIN)

	SET @guestID = @@IDENTITY
	SET @rowCount = @@ROWCOUNT
	IF @rowCount = 1
		INSERT INTO [Invoice] (HotelGuestID, Active, DateOpened)
		VALUES (@guestID, DEFAULT, CURRENT_TIMESTAMP)

	RETURN @@ROWCOUNT

END

