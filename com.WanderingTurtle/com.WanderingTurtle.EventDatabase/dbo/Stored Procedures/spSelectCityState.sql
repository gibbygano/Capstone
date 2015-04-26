CREATE PROCEDURE [dbo].[spSelectCityState] 
	(@Zip	char(5))
AS
	SELECT Zip, City, State 
	FROM CityState 
	WHERE Zip = @Zip