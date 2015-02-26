CREATE PROCEDURE [dbo].[spEmployeeUpdate]
	(@firstName 					varchar(50),
	 @lastName	 					varchar(50),
	 @level							int,
	 @active						bit,
	 @original_employeeID 			int,
	 @original_firstName			varchar(50),
	 @original_lastName				varchar(50),
	 @original_level				int,
	 @original_active				bit)
AS
	UPDATE [Employee]
		SET [firstName] = @firstName,
			[lastName] = @lastName,
			[empLevel] = @level,
			[active] = @active
	WHERE [employeeID] = @original_employeeID
		AND [firstName] = @original_firstName
		AND [lastName] = @original_lastName
		AND [empLevel] = @original_level
		AND [active] = @original_active
	
	RETURN @@ROWCOUNT
GO