/* Summary: used to get all of the users
Parameters: none
Exceptions: none*/
CREATE PROCEDURE dbo.spUserLoginGetList
AS
	BEGIN
		SELECT userID, userPassword, userLevel
			FROM UserLogin
	END