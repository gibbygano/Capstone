CREATE PROCEDURE [dbo].[spCityStateUpdate] 
	(@Zip	char(5),
	 @City	varchar(50),
	 @State	char(2),
	 @original_City		varchar(50),
	 @original_State	char(2))
AS
	UPDATE 	CityState
	   SET 	City 	= @City, 
			State	= @State
	 WHERE	Zip 	= @Zip 
	   AND	City	= @original_City 
	   AND	State 	= @original_State