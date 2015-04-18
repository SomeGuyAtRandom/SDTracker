USE [SignalDB];
-- Entity JobType
-- Table JobTypes 

IF OBJECT_ID('createTblJobTypes', 'P') IS NOT NULL
DROP PROCEDURE createTblJobTypes;

GO

IF OBJECT_ID('addToTblJobTypes', 'P') IS NOT NULL
DROP PROCEDURE addToTblJobTypes;

GO

CREATE PROC createTblJobTypes
AS
BEGIN
IF OBJECT_ID('dbo.JobTypes', 'U') IS NOT NULL
DROP TABLE dbo.JobTypes;

CREATE TABLE JobTypes
(
	Id int IDENTITY PRIMARY KEY,
	Name nvarchar(255),
	JobCode nvarchar(20),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null
)


END
GO

CREATE PROC addToTblJobTypes
AS
BEGIN

SET IDENTITY_INSERT JobTypes ON;
DELETE FROM JobTypes;

INSERT INTO JobTypes(Id, Name, JobCode,DateCreated,DateUpdated) values
(1,'Department New Signal','DNS',GETDATE(), GETDATE()),
(2,'Activated Pedestrian Warning Device','SPWD',GETDATE(), GETDATE()),
(3,'Flashing Beacon - x','FB-x',GETDATE(), GETDATE()),
(4,'Department Signal Modification (Left Turn Phasing)','DMLT',GETDATE(), GETDATE()),
(5,'Department Signal Modification','DM',GETDATE(), GETDATE()),
(6,'Department Overhead Sign','DOH',GETDATE(), GETDATE()),
(7,'Speed Feedback Sign','SFS',GETDATE(), GETDATE()),
(9,'Existing Condition Plan','ECP',GETDATE(), GETDATE()),
(10,'Street Resurfacing','SR',GETDATE(), GETDATE()),
(11,'Capital Improvement Program','CIP',GETDATE(), GETDATE()),
(12,'Capital Improvement Program (Replace Overhead Sign)','CIPOH',GETDATE(), GETDATE()),
(13,'Street Lighting Improvement Program','BSL',GETDATE(), GETDATE()),
(14,'Projects Initiated by Caltrans','CT',GETDATE(), GETDATE()),
(15,'Projects Initiated by Other Agencies','OA',GETDATE(), GETDATE()),
(16,'Plan Conversion','PC',GETDATE(), GETDATE()),
(17,'Metro Rail','MR',GETDATE(), GETDATE()),
(20,'B-Permit','BP',GETDATE(), GETDATE()),
(21,'Bikeways','BIKE',GETDATE(), GETDATE());


SET IDENTITY_INSERT JobTypes OFF;

END
GO

EXEC createTblJobTypes
GO

EXEC addToTblJobTypes
GO

DROP PROCEDURE createTblJobTypes
GO

DROP PROCEDURE addToTblJobTypes
GO

SELECT * FROM JobTypes;