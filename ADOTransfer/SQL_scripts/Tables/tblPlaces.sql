USE [SignalDB];

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

