CREATE PROCEDURE [dbo].[spEmployeeCheckPassword]
(
	@employeeID INT,
	@empPassword VARCHAR(8)
)
AS
	SELECT employeeID, firstName, lastName, empLevel
	FROM Employee 
	WHERE employeeID  = @employeeID 
	  AND empPassword = @empPassword
	  AND active = 1