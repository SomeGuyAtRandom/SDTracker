USE [SignalDB];

IF OBJECT_ID('spGetUserDetailById', 'P') IS NOT NULL
DROP PROCEDURE spGetUserDetailById;

GO

CREATE PROC spGetUserDetailById  
@Id int
AS
BEGIN  
 SELECT 
	 UserPasswords.Id,
	 UserPasswords.UserName,
	 UserPasswords.Password,
	 Engineers.FirstName,
	 Engineers.LastName,
	 Engineers.Email,
	 Engineers.Initials,
	 Engineers.Phone,
	 Engineers.DateCreated,
	 UserPasswords.DateUpdated,
	 UserPasswords.DateAccessed
 FROM Engineers 
 INNER JOIN UserPasswords
 ON UserPasswords.Id = Engineers.Id
 WHERE UserPasswords.Id=@Id
END

GO

