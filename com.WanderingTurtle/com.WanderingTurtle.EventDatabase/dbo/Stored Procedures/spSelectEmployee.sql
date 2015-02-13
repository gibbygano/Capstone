CREATE PROCEDURE [dbo].[spSelectEmployee]	
	(@firstName						varchar(50),
	 @lastName						varchar(50))

AS
	SELECT employeeID, firstName, lastName, userID, active
	FROM employee
	WHERE firstName = @firstName 
		AND lastName = @lastName
		
GO