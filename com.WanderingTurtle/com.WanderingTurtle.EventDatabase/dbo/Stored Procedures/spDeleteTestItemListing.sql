--Hunter Lind
--Delete method only for use in ItemListingAccessorTest
CREATE PROCEDURE [dbo].[spDeleteTestItemListing](

        @EndDate          datetime)
AS
        DELETE FROM ItemListing
        WHERE
        EndDate = @EndDate
		RETURN @@ROWCOUNT