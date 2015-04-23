USE [SignalDB];

-- REQUIRES A RUN OF ADO Transfer
-- Also note that the table Engineers is dependent on UserPasswords
-- and thus will need to be re-created in this script.
-- ALSO note that the table UserRoles is dependent on UserPasswords
-- and thus will need to be re-created in this script.
-- ALSO note that the table UserRoles is Requirements (with an 's') 

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('dbo.Requirements', 'U') IS NOT NULL
DROP TABLE dbo.Requirements;

-- Note: the table Projects has a forg
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL
DROP TABLE dbo.Projects;

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblEngineers', 'P') IS NOT NULL
DROP PROCEDURE createTblEngineers;
GO

IF OBJECT_ID('addToEngineers', 'P') IS NOT NULL
DROP PROCEDURE addToEngineers;
GO

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblUserPasswords', 'P') IS NOT NULL
DROP PROCEDURE createTblUserPasswords;
GO

IF OBJECT_ID('addToUserPasswords', 'P') IS NOT NULL
DROP PROCEDURE addToUserPasswords;
GO

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblUserRoles', 'P') IS NOT NULL
DROP PROCEDURE createTblUserRoles;
GO

IF OBJECT_ID('addToUserRoles', 'P') IS NOT NULL
DROP PROCEDURE addToUserRoles;
GO

CREATE PROC createTblEngineers
AS
BEGIN
IF OBJECT_ID('dbo.Engineers', 'U') IS NOT NULL
DROP TABLE dbo.Engineers;

 CREATE TABLE Engineers
(
	-- Please note the foreign key reference
	Id int FOREIGN KEY REFERENCES UserPasswords(Id) default null,
	FirstName nvarchar(30),
	LastName nvarchar(30),
	Email nvarchar(100),
	Initials nvarchar(5),
	UserName nvarchar(30) not null,
	Phone nvarchar(20),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null,
	unique(Id),
	unique(UserName),
	unique(Email)
);

END
GO
	
CREATE PROC createTblUserPasswords
AS
BEGIN


 CREATE TABLE UserPasswords
(
	Id int primary key identity,
	UserName nvarchar(30) not null,
	Password nvarchar(30) not null,
	RememberMe bit,
	IsDisabled bit,
	DateCreated DateTime not null,
	DateUpdated DateTime not null,
	DateAccessed DateTime not null,
	unique(UserName)

);

END
GO

CREATE PROC createTblUserRoles
AS
BEGIN

IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL
DROP TABLE dbo.UserRoles;

CREATE TABLE UserRoles
(
	UserId int FOREIGN KEY REFERENCES UserPasswords(Id) default null,
	RoleId int FOREIGN KEY REFERENCES Roles(Id) default null,
);
END

GO

-- CREATE TABLES

IF OBJECT_ID('dbo.Engineers', 'U') IS NOT NULL
DROP TABLE dbo.Engineers;

IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL
DROP TABLE dbo.UserRoles;

IF OBJECT_ID('dbo.UserPasswords', 'U') IS NOT NULL
DROP TABLE dbo.UserPasswords;

-- ORDER IS IMPORTANT
EXEC createTblUserPasswords
GO

EXEC createTblUserRoles
GO

EXEC createTblEngineers 
GO

-- ADD TO TABLE PROCEEDURES
CREATE PROC addToEngineers
AS
BEGIN

DELETE FROM Engineers;
insert into Engineers 
(Id,FirstName,LastName,Email,Initials,UserName,Phone,
 DateCreated,DateUpdated) values 
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
(22,'Ricardo','Rivera','ricardo.rivera@lacity.org','RR','rrivera','(213)928-9680',GETDATE(), GETDATE());

END
GO

CREATE PROC addToUserPasswords
AS
BEGIN

SET IDENTITY_INSERT UserPasswords ON;
DELETE FROM UserPasswords;

insert into UserPasswords 
(Id,UserName,Password,RememberMe,IsDisabled,DateCreated,DateUpdated,DateAccessed) values 

(1,'acuevas','acuevas',1,1,GETDATE(), GETDATE(),GETDATE()),
(2,'amashian','amashian',1,1,GETDATE(), GETDATE(),GETDATE()),
(3,'bhicks','bhicks' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(4,'bwheeler','bwheeler' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(5,'chan','chan' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(6,'chrishy','chrishy' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(7,'cquan','cquan' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(8,'dnolasco','dnolasco' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(9,'eaghajani','eaghajani' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(10,'echow','echow' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(11,'ehermoso','ehermoso',1,1,GETDATE(), GETDATE(),GETDATE()),
(12,'epena','epena' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(13,'eworkneh','eworkneh',1,1,GETDATE(), GETDATE(),GETDATE()),
(14,'farias','farias',1,1,GETDATE(), GETDATE(),GETDATE()),
(15,'jvarghese','jvarghese',1,1,GETDATE(), GETDATE(),GETDATE()),
(16,'manaya','manaya',1,1,GETDATE(), GETDATE(),GETDATE()),
(17,'mdelpasand','mdelpasand',1,1,GETDATE(), GETDATE(),GETDATE()),
(18,'mmoshksar','mmoshksar',1,1,GETDATE(), GETDATE(),GETDATE()),
(19,'randrade','x' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(20,'rlarios','rlarios' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(21,'rmolato','rmolato' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(22,'rrivera','rrivera' ,1,1,GETDATE(), GETDATE(),GETDATE()),
(23,'x','x',1,1,GETDATE(), GETDATE(),GETDATE()),
(24,'account','Account#@1' ,1,1,GETDATE(), GETDATE(),GETDATE());

SET IDENTITY_INSERT UserPasswords OFF;

END
GO

-- WORK



-- ADD TO TABLE PROCEEDURES


CREATE PROC addToUserRoles
AS
BEGIN
INSERT INTO UserRoles(UserId,RoleId)
SELECT Id,1 FROM UserPasswords;

INSERT INTO UserRoles(UserId,RoleId)
SELECT Id,2 FROM UserPasswords;

INSERT INTO UserRoles(UserId,RoleId)
SELECT Id,3 FROM UserPasswords;

INSERT INTO UserRoles(UserId,RoleId)
SELECT Id, 4 FROM UserPasswords;

END
GO

EXEC addToUserPasswords
GO

EXEC addToUserRoles
GO

EXEC addToEngineers
GO

-- CLEAN UP
IF OBJECT_ID('createTblEngineers', 'P') IS NOT NULL
DROP PROCEDURE createTblEngineers;
GO

IF OBJECT_ID('addToEngineers', 'P') IS NOT NULL
DROP PROCEDURE addToEngineers;
GO

IF OBJECT_ID('createTblUserPasswords', 'P') IS NOT NULL
DROP PROCEDURE createTblUserPasswords;
GO

IF OBJECT_ID('addToUserPasswords', 'P') IS NOT NULL
DROP PROCEDURE addToUserPasswords;
GO


IF OBJECT_ID('createTblUserRoles', 'P') IS NOT NULL
DROP PROCEDURE createTblUserRoles;
GO

IF OBJECT_ID('addToUserRoles', 'P') IS NOT NULL
DROP PROCEDURE addToUserRoles;
GO


-- Test
SELECT * FROM UserPasswords;

GO


