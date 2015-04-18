USE [SignalDB];

IF OBJECT_ID('spUpdateUserRole', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserRole;

GO

CREATE PROC spUpdateUserRole
@Id int,
@roleName nvarchar(20),
@isMember Bit

AS
BEGIN  
DECLARE 
 @RoleId int = 0

 SELECT  @RoleId = Id FROM Roles WHERE RoleType = @roleName;
 BEGIN TRANSACTION 
	DELETE FROM UserRoles  WHERE  UserId=@Id AND RoleId = @RoleId;
 COMMIT

IF @isMember=1
BEGIN 
	INSERT INTO UserRoles (UserId, RoleId) VALUES (@Id,@RoleId);
END
END

GO