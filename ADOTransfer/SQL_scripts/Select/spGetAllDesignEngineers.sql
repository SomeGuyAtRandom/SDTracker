USE [SignalDB];

IF OBJECT_ID('spGetAllDesignEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllDesignEngineers;

GO

CREATE PROC spGetAllDesignEngineers  
AS
BEGIN  
 SELECT Engineers.* FROM Engineers  
 INNER JOIN UserRoles
 ON Engineers.Id = UserRoles.UserId 
 WHERE  UserRoles.RoleId =3
END

GO



