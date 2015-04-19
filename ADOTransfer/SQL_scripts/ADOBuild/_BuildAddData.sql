USE [SignalDB];
-- This script will create the database from scratch.

DELETE FROM UserPasswords

GO  

SET IDENTITY_INSERT UserPasswords ON

GO

INSERT INTO UserPasswords 
(Id,UserName,Password,RememberMe,IsDisabled,DateCreated,DateUpdated,DateAccessed) VALUES 
(1,'acuevas','acuevas',1,1,GETDATE(), GETDATE(),GETDATE()),
(2,'amashian','amashian',1,1,GETDATE(), GETDATE(),GETDATE()),
(3,'bhicks','bhicks' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(4,'bwheeler','bwheeler',1,1,GETDATE(), GETDATE(),GETDATE()),
(5,'chan','chan',1,1,GETDATE(), GETDATE(),GETDATE()),
(6,'chrishy','chrishy',1,1,GETDATE(), GETDATE(),GETDATE()),
(7,'cquan','cquan',1,1,GETDATE(), GETDATE(),GETDATE()),
(8,'dnolasco','dnolasco',1,1,GETDATE(), GETDATE(),GETDATE()),
(9,'eaghajani','eaghajani',1,1,GETDATE(), GETDATE(),GETDATE()),
(10,'echow','echow',1,1,GETDATE(), GETDATE(),GETDATE()),
(11,'ehermoso','ehermoso',1,1,GETDATE(), GETDATE(),GETDATE()),
(12,'epena','epena',1,1,GETDATE(), GETDATE(),GETDATE()),
(13,'eworkneh','eworkneh',1,1,GETDATE(), GETDATE(),GETDATE()),
(14,'farias','farias',1,1,GETDATE(), GETDATE(),GETDATE()),
(15,'jvarghese','jvarghese',1,1,GETDATE(), GETDATE(),GETDATE()),
(16,'manaya','manaya',1,1,GETDATE(), GETDATE(),GETDATE()),
(17,'mdelpasand','mdelpasand',1,1,GETDATE(), GETDATE(),GETDATE()),
(18,'mmoshksar','mmoshksar',1,1,GETDATE(), GETDATE(),GETDATE()),
(19,'randrade','x',1,1,GETDATE(), GETDATE(),GETDATE()),
(20,'rlarios','rlarios',1,1,GETDATE(), GETDATE(),GETDATE()),
(21,'rmolato','rmolato',1,1,GETDATE(), GETDATE(),GETDATE()),
(22,'rrivera','rrivera',1,1,GETDATE(), GETDATE(),GETDATE()),
(23,'x','x',1,1,GETDATE(), GETDATE(),GETDATE()),
(24,'account','Account#@1' ,1,1,GETDATE(), GETDATE(),GETDATE());

SET IDENTITY_INSERT UserPasswords OFF

GO


SET IDENTITY_INSERT Roles ON 

GO

INSERT INTO Roles (Id,RoleType) values
(1,'Admin'),
(2,'Guest'),
(3,'Design Engineer'),
(4,'Head Engineer')

GO
SET IDENTITY_INSERT Roles OFF

GO


SET IDENTITY_INSERT JobTypes ON

GO

DELETE FROM JobTypes

GO

INSERT INTO JobTypes(Id, Name, JobCode,DateCreated,DateUpdated) VALUES
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
(21,'Bikeways','BIKE',GETDATE(), GETDATE())

GO

SET IDENTITY_INSERT JobTypes OFF

GO


SET IDENTITY_INSERT RequirementType ON

GO

DELETE FROM RequirementType

GO

INSERT INTO RequirementType (Id,RequirementName, DateCreated, DateUpdated) VALUES
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
(13,'As Built', GETDATE(), GETDATE())

GO

SET IDENTITY_INSERT RequirementType OFF

GO

DELETE FROM Engineers

GO

INSERT INTO Engineers 
(Id,FirstName,LastName,Email,Initials,UserName,Phone,
 DateCreated,DateUpdated) VALUES 
(1,'Armando','Cuevas','armando.cuevas@lacity.org','AC','acuevas','(213)928-9677',GETDATE(), GETDATE()),
(2,'Aziz','Mashian','aziz.mashian@lacity.org','AM','amashian','(213)928-9673',GETDATE(), GETDATE()),
(3,'Bernard','Hicks','bernard.hicks@lacity.org','BH','bhicks','',GETDATE(), GETDATE()),
(4,'Bruce','Wheeler','bruce.wheeler@lacity.org','BW','bwheeler','(213)928-9653',GETDATE(), GETDATE()),
(5,'Bill','Chan','bill.chan@lacity.org','BC','chan','(213)928-9640',GETDATE(), GETDATE()),
(6,'chris','Hy','chris.hy@lacity.org','CH','chrishy','',GETDATE(), GETDATE()),
(7,'C','Quan','c.quan@lacity.org','Q','cquan','',GETDATE(), GETDATE()),
(8,'David','Nolasco','david.nolasco@lacity.org','DN','dnolasco','',GETDATE(), GETDATE()),
(9,'Essie','Aghajani','essie.aghajani@lacity.org','EA','eaghajani','',GETDATE(), GETDATE()),
(10,'Ed','Chow','ed.chow@lacity.org','EC','echow','(213)928-9683',GETDATE(), GETDATE()),
(11,'Eduardo','Hermoso','eduardo.hermoso@lacity.org','EH','ehermoso','',GETDATE(), GETDATE()),
(12,'Evelinda','Pena','evelinda.pena@lacity.org','EP','epena','(213)928-9678',GETDATE(), GETDATE()),
(13,'Engdu','Workneh','engdu.workneh@lacity.org','EW','eworkneh','',GETDATE(), GETDATE()),
(14,'Fabio','Arias','fabio.arias@lacity.org','FA','farias','(213)928-9684',GETDATE(), GETDATE()),
(15,'John','Varghese','john.varghese@lacity.org','JV','jvarghese','(213)928-9681',GETDATE(), GETDATE()),
(16,'Manuel','Anaya','manuel.anaya@lacity.org','MA','manaya','(213)928-9670',GETDATE(), GETDATE()),
(17,'M','Delpasand','m.delpasand@lacity.org','MD','mdelpasand','',GETDATE(), GETDATE()),
(18,'Mike','Moshksar','mike.moshksar@lacity.org','MM','mmoshksar','(213)928-9672',GETDATE(), GETDATE()),
(19,'Ray','Andrade','andrade.ray@gmail.com','RA','randrade','(818)317-1293',GETDATE(), GETDATE()),
(20,'Ramone','Larios','ramone.larios@lacity.org','RL','rlarios','',GETDATE(), GETDATE()),
(21,'Richard','Molato','richard.molato@lacity.org','RM','rmolato','',GETDATE(), GETDATE()),
(22,'Ricardo','Rivera','ricardo.rivera@lacity.org','RR','rrivera','(213)928-9680',GETDATE(), GETDATE())

GO

ALTER TABLE Projects ADD HeadEngineerTxtID nvarchar(30)
GO

ALTER TABLE Projects ADD DesignEngineerTxtID nvarchar(30)
GO


SET IDENTITY_INSERT Projects ON

GO

DELETE FROM Projects

GO

INSERT INTO Projects 
(Id,Location,DistrictId,CurrRemark,Rush,ProjNo,FiveDigit,CDs,JobTypeId, HeadEngineerId, DesignEngineerId, 
CurrentComment, DateAssigned,StartDate,DateCreated,DateUpdated,HeadEngineerTxtID,DesignEngineerTxtID) 
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

GO

SET IDENTITY_INSERT Projects OFF

GO

UPDATE Projects 
SET 
HeadEngineerID = UserPasswords.Id
FROM Projects
INNER JOIN UserPasswords
ON UserPasswords.UserName = Projects.HeadEngineerTxtID

GO

UPDATE Projects 
SET 
DesignEngineerID = UserPasswords.Id
FROM Projects
INNER JOIN UserPasswords
ON UserPasswords.UserName = Projects.DesignEngineerTxtID

GO

UPDATE Projects 
SET 
DistrictId = sdn_main_projects.DIST_ID
FROM Projects
INNER JOIN sdn_main_projects
ON Projects.Id = sdn_main_projects.MAIN_PROJECTS_KEY
WHERE sdn_main_projects.DIST_ID <> 0

GO

ALTER TABLE Projects DROP COLUMN HeadEngineerTxtID

GO

ALTER TABLE Projects DROP COLUMN DesignEngineerTxtID

GO


-- Engineers does not have any foreign demendancies


--



DELETE FROM Requirements

GO

INSERT INTO Requirements 
SELECT 1,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_BASE,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET StartDate = TO_BASE
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =1 
AND TO_BASE is not NULL

GO

UPDATE  Requirements 
SET Required = NEED_BASE
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =1

GO

-- FIELD CHECK

INSERT INTO Requirements 
SELECT 2,MAIN_PROJECTS_KEY,1,DATE_CREATED, FIELD_CHECK,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET Required = NEED_FIELD_CHECK
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =2

GO

-- PRE-DESIGN

INSERT INTO Requirements 
SELECT 3,MAIN_PROJECTS_KEY,1,DATE_CREATED, PRE_DESIGN,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET Required = NEED_PRE_DESIGN
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =3;

GO
-- DWP

INSERT INTO Requirements 
SELECT 4,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_DWP,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET StartDate = TO_DWP
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =4 
AND TO_DWP is not NULL

GO

UPDATE  Requirements 
SET Required = NEED_DWP
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =4

GO

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
AND TO_BSL is not NULL

GO

UPDATE  Requirements 
SET Required = NEED_BSL
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =5

GO

-- BSL Signed

INSERT INTO Requirements 
SELECT 6,MAIN_PROJECTS_KEY,1,DATE_CREATED, BSL_SIGNED,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET Required = NEED_BSL
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =6

GO
-- DISTRICT

INSERT INTO Requirements 
SELECT 7,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_DISTRICT,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET StartDate = TO_DISTRICT
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =7
AND TO_DISTRICT is not NULL

GO

UPDATE  Requirements 
SET Required = NEED_DISTRICT
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =7

GO
-- TIMING
INSERT INTO Requirements 
SELECT 8,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_TIMING,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET StartDate = TO_TIMING
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =8
AND TO_TIMING is not NULL

GO


UPDATE  Requirements 
SET Required = NEED_TIMING
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =8

GO

-- OTHER AGENCIES

INSERT INTO Requirements 
SELECT 9,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_OTHER_AGENCIES,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET StartDate = TO_OTHER_AGENCIES
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =9
AND TO_OTHER_AGENCIES is not NULL

GO

UPDATE  Requirements 
SET Required = NEED_OTHER_AGENCIES
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =9

GO

-- REVIEW

INSERT INTO Requirements 
SELECT 10,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_REVIEW,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET StartDate = TO_REVIEW
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =10
AND TO_REVIEW is not NULL

GO

-- DOT Approval

INSERT INTO Requirements 
SELECT 11,MAIN_PROJECTS_KEY,1,DATE_CREATED, DOT_APPROVED,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

-- To Coordinate

INSERT INTO Requirements 
SELECT 12,MAIN_PROJECTS_KEY,1,DATE_CREATED, COORD,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

-- As Built

INSERT INTO Requirements 
SELECT 13,MAIN_PROJECTS_KEY,1,DATE_CREATED, FROM_AS_BUILT,'', 
CASE WHEN START_DATE < DATE_CREATED
THEN START_DATE
ELSE DATE_CREATED
END
, GETDATE() FROM sdn_main_projects

GO

UPDATE  Requirements 
SET StartDate = TO_AS_BUILT
FROM sdn_main_projects
INNER JOIN  Requirements
ON MAIN_PROJECTS_KEY = Id
WHERE RequirementId =13
AND TO_REVIEW is not NULL

GO



DELETE FROM UserRoles

GO


INSERT INTO UserRoles 
SELECT id,1 FROM  UserPasswords
GO

INSERT INTO UserRoles 
SELECT id,2 FROM  UserPasswords
GO

INSERT INTO UserRoles 
SELECT id,3 FROM  UserPasswords
GO

INSERT INTO UserRoles 
SELECT id,4 FROM  UserPasswords
GO

