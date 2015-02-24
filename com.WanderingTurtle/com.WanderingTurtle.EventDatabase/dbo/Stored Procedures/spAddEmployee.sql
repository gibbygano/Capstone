CREATE PROCEDURE [dbo].[spAddEmployee]
	(@firstName 					varchar(50),
	 @lastName	 					varchar(50),
	 @empPassword					varchar(8),
	 @empLevel						int)
	
AS
	INSERT INTO employee ([firstName], [lastName], [empPassword], [empLevel])
		VALUES(@firstName, @lastName, @empPassword, @empLevel)
RETURN @@ROWCOUNT		
