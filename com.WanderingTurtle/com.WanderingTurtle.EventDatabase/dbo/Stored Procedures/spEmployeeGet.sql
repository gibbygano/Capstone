CREATE PROCEDURE [dbo].[spEmployeeGet] 
	@employeeID int = null
AS
BEGIN
	SELECT [employeeID]
		  ,[firstName]
		  ,[lastName]
		  ,[empLevel]
		  ,[active]
	  FROM [dbo].[Employee]
	  WHERE (@employeeID IS NOT NULL OR ([active] = '1'))
		AND (@employeeID IS NULL OR ([employeeID] = @employeeID))
END