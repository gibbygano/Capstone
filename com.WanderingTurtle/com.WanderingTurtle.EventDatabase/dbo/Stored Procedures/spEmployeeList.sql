CREATE PROCEDURE [dbo].[spEmployeeList]
AS
	SELECT employeeID, firstName, lastName, empLevel
	FROM employee 
	WHERE active = '1' 
	
GO