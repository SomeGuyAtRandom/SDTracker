USE [SignalDB];

-- This action is to clear out this stored procedure (if it exsists) 
-- This stored procedure will be re-created and dropped after the run of this script
IF OBJECT_ID('createTblUserRoles', 'P') IS NOT NULL
DROP PROCEDURE createTblUserRoles;

GO

IF OBJECT_ID('addToUserRoles', 'P') IS NOT NULL
DROP PROCEDURE addToUserRoles;

GO

CREATE PROC createTblUserRoles
AS
BEGIN

IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL
DROP TABLE dbo.UserRoles;

CREATE TABLE UserRoles
(
	-- Please note the foreign key reference
	UserId int FOREIGN KEY REFERENCES UserPasswords(Id) default null,
	RoleId int FOREIGN KEY REFERENCES Roles(Id) default null,
);
END

GO


CREATE PROC addToUserRoles
AS
BEGIN

-- This action gives all users all roles
INSERT INTO UserRoles(UserId,RoleId) SELECT Id,1 FROM UserPasswords;
INSERT INTO UserRoles(UserId,RoleId) SELECT Id,2 FROM UserPasswords;
INSERT INTO UserRoles(UserId,RoleId) SELECT Id,3 FROM UserPasswords;
INSERT INTO UserRoles(UserId,RoleId) SELECT Id, 4 FROM UserPasswords;

END
GO
EXEC createTblUserRoles
GO
EXEC createTblUserRoles
GO
DROP PROCEDURE createTblUserRoles;
GO
DROP PROCEDURE addToUserRoles;
GO

-- Test
SELECT * FROM UserRoles;