CREATE PROCEDURE [dbo].[spCityStateReadState] 
	(@State	char(2))
AS
	SELECT Zip, City, State 
	FROM CityState 
	WHERE State = @State