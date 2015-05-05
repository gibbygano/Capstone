CREATE PROCEDURE [dbo].[spSelectEmployee] 
	@employeeID int = null
AS
BEGIN
	SELECT [employeeID]
		  ,[firstName]
		  ,[lastName]
		  ,[empLevel]
		  ,[active]
	  FROM [dbo].[Employee]
	  WHERE (@employeeID IS NULL OR ([employeeID] = @employeeID))
END