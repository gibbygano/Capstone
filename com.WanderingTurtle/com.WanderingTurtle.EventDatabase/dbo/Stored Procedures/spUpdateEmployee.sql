CREATE PROCEDURE [dbo].[spUpdateEmployee]
	(@employeeID 					int,
	 @firstName 					varchar(50),
	 @lastName	 					varchar(50),
	 @userID						int,
	 @active						bit,
	 @original_employeeID 			int,
	 @original_firstName			varchar(50),
	 @original_lastName				varchar(50),
	 @original_userID				int,
	 @original_active				bit)
AS
	UPDATE employee
		SET employeeID = @employeeID,
			firstName  = @firstName,
			lastName = @lastName,
			userID = @userID,
			active = @active
	WHERE employeeID = @original_employeeID
		AND firstName  = @original_firstName
		AND userID = @original_userID
		AND active = @original_active
	
	RETURN @@ROWCOUNT
GO