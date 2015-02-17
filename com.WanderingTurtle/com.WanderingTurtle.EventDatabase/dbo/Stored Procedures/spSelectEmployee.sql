CREATE PROCEDURE [dbo].[spSelectEmployee]	
	(@firstName						varchar(50),
	 @lastName						varchar(50),
	 @active						bit)

AS
	SELECT employeeID, firstName, lastName, empLevel, active
	FROM employee
	WHERE firstName = @firstName 
		AND lastName = @lastName
		
GO