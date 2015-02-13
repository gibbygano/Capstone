CREATE PROCEDURE [dbo].[spAddEmployee]
	(@employeeID 					int,
	 @firstName 					varchar(50),
	 @lastName	 					varchar(50),
	 @userID						int,
	 @active						bit)
	
AS
	INSERT INTO employee ([employeeID], [firstName], [lastName], [userID], [active])
		VALUES(@employeeID, @firstName, @lastName, @userID, @active)
		
GO