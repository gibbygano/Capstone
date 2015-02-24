CREATE PROCEDURE [dbo].[spUpdateEmployee]
	(@firstName 					varchar(50),
	 @lastName	 					varchar(50),
	 @empPassword					varchar(8),
	 @active						bit,
	 @original_employeeID 			int,
	 @original_firstName			varchar(50),
	 @original_lastName				varchar(50),
     @original_password				varchar(8),
	 @original_active				bit)
AS
	UPDATE employee
		SET empPassword = @empPassword,
			active = @active,
			lastName = @lastName,
			firstName = @firstName
	WHERE employeeID = @original_employeeID
	
	RETURN @@ROWCOUNT
GO