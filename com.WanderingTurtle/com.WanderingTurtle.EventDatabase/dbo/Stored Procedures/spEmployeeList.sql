CREATE PROCEDURE [dbo].[spEmployeeList]
AS
	SELECT employeeID, firstName, lastName, userID, active
	FROM employee
	WHERE active = '1'
	
GO