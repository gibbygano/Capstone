/****************************created by: Tony Noel************************************/

CREATE PROCEDURE [dbo].[spSelectAllBookings]
AS
BEGIN
	SELECT * 
	FROM Booking 
END

	RETURN @@ROWCOUNT