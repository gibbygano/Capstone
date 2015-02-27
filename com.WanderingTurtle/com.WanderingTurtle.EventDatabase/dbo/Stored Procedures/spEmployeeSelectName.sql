CREATE PROCEDURE [dbo].[spEmployeeSelectName]	
	(@firstName						varchar(50),
	 @lastName						varchar(50)
)

AS
	SELECT employeeID, firstName, lastName, empLevel, active
	FROM [Employee]
	WHERE firstName = @firstName 
		AND lastName = @lastName
		
GO