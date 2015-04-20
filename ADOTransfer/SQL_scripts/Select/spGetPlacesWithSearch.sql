USE [SignalDB];

IF OBJECT_ID('spGetPlacesWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetPlacesWithSearch;

GO


CREATE PROC spGetPlacesWithSearch  
@word0 nvarchar(80) = null,
@word1 nvarchar(80) = null,
@word2 nvarchar(80) = null,
@word3 nvarchar(80) = null,
@word4 nvarchar(80) = null,
@word5 nvarchar(80) = null,
@word6 nvarchar(80) = null,
@word7 nvarchar(80) = null,
@word8 nvarchar(80) = null,
@word9 nvarchar(80) = null,
@word10 nvarchar(80) = null,
@districtId int = 0

AS

BEGIN
    SELECT TOP 40 *
    FROM Places
    WHERE
            (@word0 IS NULL OR (PlaceLocation LIKE '%' + @word0 + '%' ))
        AND (@word1 IS NULL OR (PlaceLocation LIKE '%' + @word1 + '%' ))
		AND (@word2 IS NULL OR (PlaceLocation LIKE '%' + @word2 + '%' ))
		AND (@word3 IS NULL OR (PlaceLocation LIKE '%' + @word3 + '%' ))
		AND (@word4 IS NULL OR (PlaceLocation LIKE '%' + @word4 + '%' ))
		AND (@word5 IS NULL OR (PlaceLocation LIKE '%' + @word5 + '%' ))
		AND (@word6 IS NULL OR (PlaceLocation LIKE '%' + @word6 + '%' ))
		AND (@word7 IS NULL OR (PlaceLocation LIKE '%' + @word7 + '%' ))
		AND (@word8 IS NULL OR (PlaceLocation LIKE '%' + @word8 + '%' ))
		AND (@word9 IS NULL OR (PlaceLocation LIKE '%' + @word9 + '%' ))
			
            
    -- OPTION (RECOMPILE) ---<<<<use if on for SQL 2008 SP1 CU5 (10.0.2746) and later
END
