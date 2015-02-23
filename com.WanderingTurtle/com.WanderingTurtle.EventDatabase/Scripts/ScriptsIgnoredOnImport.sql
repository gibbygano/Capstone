

CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255), 
	@originalRating int, 
	@originalNotes varchar(255) )
AS
	UPDATE SupplierFeedbackRecord
	SET
		Rating = @Rating,
		Notes = @Notes,
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @originalRating
		AND Nots = @originalNotes
	RETURN @@ROWCOUNT

GO

--Syntax Error: Incorrect syntax near WHERE.
--
--CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
--	@RatingID int, 
--	@SupplierID int, 
--	@EmployeeID int, 
--	@Rating int, 
--	@Notes varChar(255), 
--	@originalRating int, 
--	@originalNotes varchar(255) )
--AS
--	UPDATE SupplierFeedbackRecord
--	SET
--		Rating = @Rating,
--		Notes = @Notes,
--	WHERE
--		RatingID = @RatingID
--		AND SupplierID = @SupplierID
--		AND EmployeeID = @EmployeeID
--		AND Rating = @originalRating
--		AND Nots = @originalNotes
--	RETURN @@ROWCOUNT

GO


CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255) )
AS
	DELETE FROM SupplierFeedbackRecord
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @Rating
		AND Notes = @Notes
END
	RETURN @@ROWCOUNT

GO

--Syntax Error: Incorrect syntax near RETURN.
--
--CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
--	@RatingID int, 
--	@SupplierID int, 
--	@EmployeeID int, 
--	@Rating int, 
--	@Notes varChar(255) )
--AS
--	DELETE FROM SupplierFeedbackRecord
--	WHERE
--		RatingID = @RatingID
--		AND SupplierID = @SupplierID
--		AND EmployeeID = @EmployeeID
--		AND Rating = @Rating
--		AND Notes = @Notes
--END
--	RETURN @@ROWCOUNT



GO


CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255), 
	@originalRating int, 
	@originalNotes varchar(255) )
AS
	UPDATE SupplierFeedbackRecord
	SET
		Rating = @Rating,
		Notes = @Notes,
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @originalRating
		AND Nots = @originalNotes
	RETURN @@ROWCOUNT

GO

--Syntax Error: Incorrect syntax near WHERE.
--
--CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
--	@RatingID int, 
--	@SupplierID int, 
--	@EmployeeID int, 
--	@Rating int, 
--	@Notes varChar(255), 
--	@originalRating int, 
--	@originalNotes varchar(255) )
--AS
--	UPDATE SupplierFeedbackRecord
--	SET
--		Rating = @Rating,
--		Notes = @Notes,
--	WHERE
--		RatingID = @RatingID
--		AND SupplierID = @SupplierID
--		AND EmployeeID = @EmployeeID
--		AND Rating = @originalRating
--		AND Nots = @originalNotes
--	RETURN @@ROWCOUNT



GO
