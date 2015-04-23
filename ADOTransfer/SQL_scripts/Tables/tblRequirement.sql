USE [SignalDB];

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblRequirement', 'P') IS NOT NULL
DROP PROCEDURE createTblRequirement;

GO

IF OBJECT_ID('addToRequirement', 'P') IS NOT NULL
DROP PROCEDURE addToRequirement;

GO

CREATE PROC createTblRequirement
AS
BEGIN
IF OBJECT_ID('dbo.RequirementType', 'U') IS NOT NULL
DROP TABLE dbo.RequirementType;
CREATE TABLE RequirementType
(
	Id int primary key identity,
	RequirementName nvarchar(255),
	DateCreated DateTime,
	DateUpdated DateTime 
);

END

GO

CREATE PROC addToRequirement
AS
BEGIN

SET IDENTITY_INSERT Requirement ON;
DELETE FROM Requirement;
INSERT INTO Requirement (Id,RequirementType, DateCreated, DateUpdated) values
(1,'BASE', GETDATE(), GETDATE()),
(2,'FIELD CHECK', GETDATE(), GETDATE()),
(3,'PRE-DESIGN', GETDATE(), GETDATE()),
(4,'DWP', GETDATE(), GETDATE()),
(5,'BSL', GETDATE(), GETDATE()),
(6,'BSL Signed', GETDATE(),GETDATE()),
(7,'DISTRICT', GETDATE(), GETDATE()),
(8,'TIMING', GETDATE(), GETDATE()),
(9,'OTHER AGENCIES', GETDATE(), GETDATE()),
(10,'REVIEW', GETDATE(), GETDATE()),
(11,'DOT Approval', GETDATE(), GETDATE()),
(12,'To Coordinate', GETDATE(), GETDATE()),
(13,'As Built', GETDATE(), GETDATE());

SET IDENTITY_INSERT Requirement OFF;

END
GO

EXEC createTblRequirement
GO

EXEC addToRequirement
GO

DROP PROCEDURE createTblRequirement
GO

DROP PROCEDURE addToRequirement
GO

SELECT * FROM Requirement;

