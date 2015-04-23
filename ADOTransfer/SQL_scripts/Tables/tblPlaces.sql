USE [SignalDB];

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblPlaces', 'P') IS NOT NULL
DROP PROCEDURE createTblPlaces;

GO


CREATE PROC createTblPlaces
AS
BEGIN
   IF OBJECT_ID('dbo.Places', 'U') IS NOT NULL
   DROP TABLE dbo.Places;

CREATE TABLE Places
(
	Id int IDENTITY PRIMARY KEY,
	FiveDigit nvarchar(10),
	-- Please note the foreign key reference
	DistrictId int FOREIGN KEY REFERENCES Districts(Id) default null,
	PlaceLocation nvarchar(80),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null,
	unique(FiveDigit)
);

END
GO

EXEC createTblPlaces
GO

DROP PROCEDURE createTblPlaces
GO

