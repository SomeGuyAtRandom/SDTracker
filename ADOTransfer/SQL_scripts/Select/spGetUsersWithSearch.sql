USE [SignalDB];

IF OBJECT_ID('spGetUsersWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetUsersWithSearch;

GO


CREATE PROC spGetUsersWithSearch  
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
@word10 nvarchar(80) = null

AS

BEGIN
        SELECT *
        FROM Engineers
        WHERE
                (@word0 IS NULL OR ((FirstName LIKE '%' + @word0 + '%' ) OR (LastName LIKE '%' + @word0 + '%' )))
            AND (@word1 IS NULL OR ((FirstName LIKE '%' + @word1 + '%' ) OR (LastName LIKE '%' + @word1 + '%' )))
			AND (@word2 IS NULL OR ((FirstName LIKE '%' + @word2 + '%' ) OR (LastName LIKE '%' + @word2 + '%' )))
			AND (@word3 IS NULL OR ((FirstName LIKE '%' + @word3 + '%' ) OR (LastName LIKE '%' + @word3 + '%' )))
			AND (@word4 IS NULL OR ((FirstName LIKE '%' + @word4 + '%' ) OR (LastName LIKE '%' + @word4 + '%' )))
			AND (@word5 IS NULL OR ((FirstName LIKE '%' + @word5 + '%' ) OR (LastName LIKE '%' + @word5 + '%' )))
			AND (@word6 IS NULL OR ((FirstName LIKE '%' + @word6 + '%' ) OR (LastName LIKE '%' + @word6 + '%' )))
			AND (@word7 IS NULL OR ((FirstName LIKE '%' + @word7 + '%' ) OR (LastName LIKE '%' + @word7 + '%' )))
			AND (@word8 IS NULL OR ((FirstName LIKE '%' + @word8 + '%' ) OR (LastName LIKE '%' + @word8 + '%' )))
			AND (@word9 IS NULL OR ((FirstName LIKE '%' + @word9 + '%' ) OR (LastName LIKE '%' + @word9 + '%' )))
			
			
			
			
			
			
			
            
        -- OPTION (RECOMPILE) ---<<<<use if on for SQL 2008 SP1 CU5 (10.0.2746) and later
    END
