USE [SignalDB];
-- PLEASE NOTE TO RUN THIS SCRIPT YOU NEED TO DROP THE TABLE Projects FIRST

-- DROP TABLE Projects;

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblDistricts', 'P') IS NOT NULL
DROP PROCEDURE createTblDistricts;

GO

IF OBJECT_ID('addToDistricts', 'P') IS NOT NULL
DROP PROCEDURE addToDistricts;

GO

CREATE PROC createTblDistricts
AS
BEGIN
IF OBJECT_ID('dbo.Districts', 'U') IS NOT NULL
DROP TABLE dbo.Districts;

CREATE TABLE Districts
(
	Id int IDENTITY PRIMARY KEY,
	Name nvarchar(20),
	Code nvarchar(4),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null
);

END
GO

CREATE PROC addToDistricts
AS
BEGIN

SET IDENTITY_INSERT Districts ON;
DELETE FROM Districts;
-- TODO: Refactor Name to District...
-- On most cases, there will be no need to edit data
insert into Districts (Id,Name,Code,DateCreated,DateUpdated)  
values ( 1,'Hollywood','H',GETDATE(), GETDATE()),
(20,'Central','C',GETDATE(), GETDATE()),
(22,'Southern','S',GETDATE(), GETDATE()),
(23,'Western','W',GETDATE(), GETDATE()),
(100,'ATSAC','NULL',GETDATE(), GETDATE()),
(501,'Culver City','CC',GETDATE(), GETDATE()),
(502,'East Valley','EV',GETDATE(), GETDATE()),
(505,'West Valley','WV',GETDATE(), GETDATE());

SET IDENTITY_INSERT Districts OFF;

END
GO

EXEC createTblDistricts
GO

EXEC addToDistricts
GO

DROP PROCEDURE createTblDistricts
GO

DROP PROCEDURE addToDistricts
GO

SELECT * FROM Districts;
