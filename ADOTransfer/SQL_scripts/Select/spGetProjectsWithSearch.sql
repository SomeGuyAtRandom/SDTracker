USE [SignalDB];

IF OBJECT_ID('spGetProjectsWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetProjectsWithSearch;

GO

CREATE PROC spGetProjectsWithSearch  
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
@districtId int = 0,
@jobTypeId int = 0,
@FieldSelected nvarchar(80),
@startDate DateTime
AS

BEGIN
        SELECT TOP 40 *
        FROM Projects
        WHERE

		CASE @FieldSelected
			WHEN 'StartDate' THEN StartDate 
			WHEN 'DateCreated' THEN DateCreated 
			WHEN 'DateUpdated' THEN DateUpdated 
			ELSE @StartDate
		END >= @StartDate 
		
		AND (@word0 IS NULL OR (Location LIKE '%' + @word0 + '%' ))
		AND (@word1 IS NULL OR (Location LIKE '%' + @word1 + '%' ))
		AND (@word2 IS NULL OR (Location LIKE '%' + @word2 + '%' ))
		AND (@word3 IS NULL OR (Location LIKE '%' + @word3 + '%' ))
		AND (@word4 IS NULL OR (Location LIKE '%' + @word4 + '%' ))
		AND (@word5 IS NULL OR (Location LIKE '%' + @word5 + '%' ))
		AND (@word6 IS NULL OR (Location LIKE '%' + @word6 + '%' ))
		AND (@word7 IS NULL OR (Location LIKE '%' + @word7 + '%' ))
		AND (@word8 IS NULL OR (Location LIKE '%' + @word8 + '%' ))
		AND (@word9 IS NULL OR (Location LIKE '%' + @word9 + '%' ))
		AND (@districtId = 0 OR (DistrictId = @districtId))
		AND (@jobTypeId = 0 OR (JobTypeId = @jobTypeId))
			

        -- OPTION (RECOMPILE) ---<<<<use if on for SQL 2008 SP1 CU5 (10.0.2746) and later
    END
