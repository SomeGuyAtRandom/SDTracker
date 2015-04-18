USE [SignalDB];
GO

IF OBJECT_ID('spAddEngineer', 'P') IS NOT NULL
DROP PROCEDURE spAddEngineer;

GO
	
CREATE PROC spAddEngineer

@FirstName nvarchar(30),
@LastName nvarchar(30),
@Email nvarchar(100),
@Initials nvarchar(5),
@UserName nvarchar(20),
@Phone nvarchar(20)

AS
BEGIN  
 
 
 INSERT INTO Engineers 
 (FirstName,LastName,Email,Initials,UserName,Phone,
 DateCreated,DateUpdated )  
 VALUES (@FirstName,@LastName,@Email,@Initials,@UserName,@Phone,GETDATE(), GETDATE())  ;

 END;

GO


SELECT name AS procedure_name 
    ,SCHEMA_NAME(schema_id) AS schema_name
    ,type_desc
    ,create_date
    ,modify_date
FROM sys.procedures;
GO
