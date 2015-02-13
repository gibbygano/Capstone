CREATE PROCEDURE [dbo].[spCityStateRead] 
	(@Zip	varchar(10))
AS
	SELECT Zip, City, State 
	FROM CityState 
	WHERE Zip = @Zip