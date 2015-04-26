CREATE PROCEDURE [dbo].[spUpdateEmployee]
	(@firstName 					varchar(50),
	 @lastName	 					varchar(50),
	 @password						varchar(8) = null,
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
			[empPassword] = CASE WHEN @password IS NOT NULL THEN @password ELSE [empPassword] END,
			[empLevel] = @level,
			[active] = @active
	WHERE [employeeID] = @original_employeeID
		AND [firstName] = @original_firstName
		AND [lastName] = @original_lastName
		AND [empLevel] = @original_level
		AND [active] = @original_active
	
	RETURN @@ROWCOUNT
GO