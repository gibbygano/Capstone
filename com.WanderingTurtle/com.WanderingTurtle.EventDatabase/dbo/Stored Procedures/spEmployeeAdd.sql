CREATE PROCEDURE [dbo].[spEmployeeAdd]
	(@firstName 					varchar(50),
	 @lastName	 					varchar(50),
	 @empPassword					varchar(8),
	 @empLevel						int,
	 @active						bit)
	
AS
	INSERT INTO [Employee] ([firstName], [lastName], [empPassword], [empLevel], [active])
		VALUES(@firstName, @lastName, @empPassword, @empLevel, @active)
RETURN @@ROWCOUNT		
