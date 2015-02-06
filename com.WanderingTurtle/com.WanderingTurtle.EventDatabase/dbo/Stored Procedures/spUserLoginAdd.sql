CREATE PROCEDURE dbo.spUserLoginAdd
	(
		@userPassword varchar(50),
		@userLevel char
	)
AS
	BEGIN
		INSERT INTO UserLogin
			(userPassword, userLevel)
		VALUES
			(@userPassword, @userLevel)
	END
	
	RETURN @@ROWCOUNT