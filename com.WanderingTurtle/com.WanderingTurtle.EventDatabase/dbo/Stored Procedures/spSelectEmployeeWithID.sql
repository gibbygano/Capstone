CREATE PROCEDURE [dbo].[spSelectEmployeeWithID]	
	(@EmployeeID	int)

AS
BEGIN
	SELECT employeeID, firstName, lastName, empLevel, active
	FROM employee
	WHERE employeeID = @EmployeeID 

END		
GO