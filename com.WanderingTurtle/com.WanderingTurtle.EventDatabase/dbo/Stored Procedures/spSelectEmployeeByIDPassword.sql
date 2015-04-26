CREATE PROCEDURE [dbo].[spSelectEmployeeByIDPassword]
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