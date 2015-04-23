USE [SignalDB];

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblProjects', 'P') IS NOT NULL
DROP PROCEDURE createTblProjects;

GO

IF OBJECT_ID('addToProjects', 'P') IS NOT NULL
DROP PROCEDURE addToProjects;

GO

	
CREATE PROC createTblProjects
AS
BEGIN
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL
DROP TABLE dbo.Projects;


CREATE TABLE Projects
(
	Id int primary key identity,
	Location nvarchar(80),
	-- Please note the foreign key reference 
	DistrictId int FOREIGN KEY REFERENCES Districts(Id) default null,
	CurrRemark nvarchar(80),
	Rush bit,
	ProjNo nvarchar(20),
	FiveDigit nvarchar(10),
	CDs nvarchar(16), 
	-- Please note the foreign key reference
	JobTypeId int FOREIGN KEY REFERENCES JobTypes(Id) default null,
	HeadEngineerId int FOREIGN KEY REFERENCES Engineers(Id) default null,
	DesignEngineerId int FOREIGN KEY REFERENCES Engineers(Id) default null,
	CurrentComment nvarchar(255),
	DateAssigned datetime,
	StartDate datetime,
	DateCreated DateTime, 
	DateUpdated DateTime
	
);

ALTER TABLE dbo.Projects ADD HeadEngineerTxt VARCHAR(255) NULL, DesignEngineerTxtID VARCHAR(255) NULL ;

END



GO

CREATE PROC addToProjects
AS
BEGIN

SET IDENTITY_INSERT Projects ON;

DELETE FROM Projects;

INSERT INTO Projects 
(Id,Location,DistrictId,CurrRemark,Rush,ProjNo,FiveDigit,CDs,JobTypeId, HeadEngineerId, DesignEngineerId, 
CurrentComment, DateAssigned,StartDate,DateCreated,DateUpdated,HeadEngineerTxt,DesignEngineerTxtID) 

SELECT 
MAIN_PROJECTS_KEY,LOC,
NULL, 
CURR_REMARK, 
RUSH, 
PROJ_NO, 
ROUTE, 
CD_NUMS, 
JOB_TYPE_KEY,null,null,
CURR_COMMENT, DATE_ASSIGNED, START_DATE, DATE_CREATED, GETDATE(),
RTP_TO_USERNAME,
DES_ENGR_USERNAME 
FROM sdn_main_projects

SET IDENTITY_INSERT Projects OFF;
UPDATE Projects 
SET 
HeadEngineerID = UserPasswords.Id
FROM Projects
INNER JOIN UserPasswords
ON UserPasswords.UserName = Projects.HeadEngineerTxt;


UPDATE Projects 
SET 
DesignEngineerID = UserPasswords.Id
FROM Projects
INNER JOIN UserPasswords
ON UserPasswords.UserName = Projects.DesignEngineerTxtID;

UPDATE Projects 
SET 
DistrictId = sdn_main_projects.DIST_ID
FROM Projects
INNER JOIN sdn_main_projects
ON Projects.Id = sdn_main_projects.MAIN_PROJECTS_KEY
WHERE sdn_main_projects.DIST_ID <> 0;




END
GO

EXEC createTblProjects
GO

EXEC addToProjects
GO

DROP PROCEDURE createTblProjects
GO

DROP PROCEDURE addToProjects
GO

SELECT * FROM Projects;