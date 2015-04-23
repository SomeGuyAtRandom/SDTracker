USE [SignalDB];

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createRequirements', 'P') IS NOT NULL
DROP PROCEDURE createRequirements;

GO

IF OBJECT_ID('addToRequirements', 'P') IS NOT NULL
DROP PROCEDURE addToRequirements;

GO

CREATE PROC createRequirements
AS
BEGIN

IF OBJECT_ID('dbo.Requirements', 'U') IS NOT NULL
DROP TABLE dbo.Requirements;

CREATE TABLE Requirements
(
	Id int primary key identity,
	-- Please note the foreign key referencse
	RequirementId int FOREIGN KEY REFERENCES Requirement(Id) default 0,
	ProjectId int FOREIGN KEY REFERENCES Projects(Id) default 0,
	Required bit,
	StartDate datetime,
	FinishDate datetime,
	CurrentComment nvarchar(255),
	DateCreated DateTime, 
	DateUpdated DateTime 

);

END
GO

CREATE PROC addToRequirements
AS
BEGIN

DELETE FROM Requirements;

INSERT INTO Requirements 
SELECT 1,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_BASE,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET StartDate = TO_BASE
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =1 
AND TO_BASE is not NULL;

UPDATE  Requirements 
SET Required = NEED_BASE
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =1;

-- FIELD CHECK

INSERT INTO Requirements 
SELECT 2,MAIN_PROJECTS_KEY,1,DATE_CREATED, FIELD_CHECK,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET Required = NEED_FIELD_CHECK
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =2;

-- PRE-DESIGN

INSERT INTO Requirements 
SELECT 3,MAIN_PROJECTS_KEY,1,DATE_CREATED, PRE_DESIGN,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET Required = NEED_PRE_DESIGN
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =3;

-- DWP

INSERT INTO Requirements 
SELECT 4,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_DWP,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET StartDate = TO_DWP
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =4 
AND TO_DWP is not NULL;

UPDATE  Requirements 
SET Required = NEED_DWP
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =4;

-- BSL

INSERT INTO Requirements 
SELECT 5,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_BSL,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET StartDate = TO_BSL
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =5 
AND TO_BSL is not NULL;

UPDATE  Requirements 
SET Required = NEED_BSL
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =5;

-- BSL Signed

INSERT INTO Requirements 
SELECT 6,MAIN_PROJECTS_KEY,1,DATE_CREATED, BSL_SIGNED,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET Required = NEED_BSL
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =6;
-- DISTRICT

INSERT INTO Requirements 
SELECT 7,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_DISTRICT,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET StartDate = TO_DISTRICT
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =7
AND TO_DISTRICT is not NULL;

UPDATE  Requirements 
SET Required = NEED_DISTRICT
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =7;
-- TIMING
INSERT INTO Requirements 
SELECT 8,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_TIMING,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;


UPDATE  Requirements 
SET StartDate = TO_TIMING
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =8
AND TO_TIMING is not NULL;


UPDATE  Requirements 
SET Required = NEED_TIMING
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =8;

-- OTHER AGENCIES

INSERT INTO Requirements 
SELECT 9,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_OTHER_AGENCIES,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET StartDate = TO_OTHER_AGENCIES
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =9
AND TO_OTHER_AGENCIES is not NULL;

UPDATE  Requirements 
SET Required = NEED_OTHER_AGENCIES
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =9;

-- REVIEW

INSERT INTO Requirements 
SELECT 10,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_REVIEW,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET StartDate = TO_REVIEW
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =10
AND TO_REVIEW is not NULL;

-- DOT Approval

INSERT INTO Requirements 
SELECT 11,MAIN_PROJECTS_KEY,1,DATE_CREATED, DOT_APPROVED,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

-- To Coordinate

INSERT INTO Requirements 
SELECT 12,MAIN_PROJECTS_KEY,1,DATE_CREATED, COORD,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

-- As Built

INSERT INTO Requirements 
SELECT 13,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_AS_BUILT,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects;

UPDATE  Requirements 
SET StartDate = TO_AS_BUILT
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =13
AND TO_REVIEW is not NULL;

END
GO


EXEC createRequirements
GO

EXEC addToRequirements
GO

DROP PROCEDURE createRequirements
GO

DROP PROCEDURE addToRequirements
GO


SELECT * FROM Requirements;

