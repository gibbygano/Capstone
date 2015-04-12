CREATE PROCEDURE [dbo].[spCityStateReadCityState] 
	(@City	varchar(50), 
	 @State char(2))
AS
	SELECT Zip, City, State 
	FROM CityState 
	WHERE City = @City
	  AND State = @State