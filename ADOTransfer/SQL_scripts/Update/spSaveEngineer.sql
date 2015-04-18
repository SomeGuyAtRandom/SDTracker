USE [SignalDB];

IF OBJECT_ID('spSaveEngineer', 'P') IS NOT NULL
DROP PROCEDURE spSaveEngineer;

GO

CREATE PROC spSaveEngineer
@Id int,
@FirstName nvarchar(30),
@LastName nvarchar(30),
@Email nvarchar(100),
@Initials nvarchar(5),
@UserName nvarchar(20),
@Phone nvarchar(20)


AS
BEGIN  
UPDATE Engineers SET
FirstName = @FirstName,
LastName = @LastName,
Email = @Email,
Initials = @Initials, 
UserName = @UserName,
Phone =@Phone,
DateUpdated = GETDATE()
 WHERE Id=@Id   
END;