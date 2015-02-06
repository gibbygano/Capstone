/* Summary: used to get a single user
Parameters: @original_userID int, @original_userPassword varchar(50)
Exceptions: none*/
CREATE PROCEDURE dbo.spUserLoginGet
	(
		@original_userID int,
		@original_userPassword varchar(50)
	)
AS
	BEGIN
		SELECT userID, userPassword, userLevel
			FROM UserLogin
			WHERE userID = @original_userID
			AND userPassword = @original_userPassword
	END