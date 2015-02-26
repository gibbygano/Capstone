CREATE PROCEDURE [dbo].[spEmployeeSelectName]	
	(@firstName						varchar(50),
	 @lastName						varchar(50)
)

AS
	SELECT employeeID, firstName, lastName, empLevel, active
	FROM employee
	WHERE firstName = @firstName 
		AND lastName = @lastName
		
GO