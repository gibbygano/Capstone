CREATE PROCEDURE [dbo].[spCityStateReadCity] 
	(@City	varchar(50))
AS
	SELECT Zip, City, State 
	FROM CityState 
	WHERE City = @City