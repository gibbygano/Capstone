/* Summary: used to update the user's password and level of accessibility
   Parameters: new password and level as well as the id of the user, the old password, and the old level
   Exceptions: none */
CREATE PROCEDURE dbo.spUserLoginUpdate
	(
		@userPassword varchar(50),
		@userLevel char,
		
		@original_userID int,
		@original_userPassword varchar(50),
		@original_userLevel char
	)
AS
	BEGIN
		UPDATE UserLogin
			SET userPassword = @userPassword,
				userLevel = @userLevel
			WHERE userID = @original_userID
			AND userPassword = @original_userPassword
			AND userLevel = @original_userLevel
	END
		
		RETURN @@ROWCOUNT