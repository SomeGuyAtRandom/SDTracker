USE [SignalDB];


IF OBJECT_ID('spUpdateUserPassword', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserPassword;
GO

CREATE PROC spUpdateUserPassword
	@UserName nvarchar(20),
	@PassWord nvarchar(20)
	AS
BEGIN  
UPDATE UserPasswords
	SET 
		PassWord= @PassWord,
		DateUpdated= GETDATE()
	WHERE
		UserName = @UserName;

END

GO


IF OBJECT_ID('spGetPlacesWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetPlacesWithSearch;

GO


CREATE PROC spGetPlacesWithSearch  
@word0 nvarchar(80) = null,
@word1 nvarchar(80) = null,
@word2 nvarchar(80) = null,
@word3 nvarchar(80) = null,
@word4 nvarchar(80) = null,
@word5 nvarchar(80) = null,
@word6 nvarchar(80) = null,
@word7 nvarchar(80) = null,
@word8 nvarchar(80) = null,
@word9 nvarchar(80) = null,
@word10 nvarchar(80) = null,
@districtId int = 0

AS

BEGIN
	SELECT TOP 40 *
	FROM Places
	WHERE
			(@word0 IS NULL OR (PlaceLocation LIKE '%' + @word0 + '%' ))
		AND (@word1 IS NULL OR (PlaceLocation LIKE '%' + @word1 + '%' ))
		AND (@word2 IS NULL OR (PlaceLocation LIKE '%' + @word2 + '%' ))
		AND (@word3 IS NULL OR (PlaceLocation LIKE '%' + @word3 + '%' ))
		AND (@word4 IS NULL OR (PlaceLocation LIKE '%' + @word4 + '%' ))
		AND (@word5 IS NULL OR (PlaceLocation LIKE '%' + @word5 + '%' ))
		AND (@word6 IS NULL OR (PlaceLocation LIKE '%' + @word6 + '%' ))
		AND (@word7 IS NULL OR (PlaceLocation LIKE '%' + @word7 + '%' ))
		AND (@word8 IS NULL OR (PlaceLocation LIKE '%' + @word8 + '%' ))
		AND (@word9 IS NULL OR (PlaceLocation LIKE '%' + @word9 + '%' ))
			
            
-- OPTION (RECOMPILE) ---<<<<use if on for SQL 2008 SP1 CU5 (10.0.2746) and later
END

GO


IF OBJECT_ID('spGetEngineerByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetEngineerByUserName;

GO

CREATE PROC spGetEngineerByUserName  
@UserName nvarchar(30)
AS
BEGIN  
 SELECT * FROM Engineers   
 WHERE UserName =@UserName 
END
GO

IF OBJECT_ID('spGetUserEngineerByEmail', 'P') IS NOT NULL
DROP PROCEDURE spGetUserEngineerByEmail;

GO

CREATE PROC spGetUserEngineerByEmail  
@Email nvarchar(100)
AS
BEGIN  
 SELECT Id FROM Engineers   
 WHERE Email=@Email
END
GO


IF OBJECT_ID('spEnableDisableUser', 'P') IS NOT NULL
DROP PROCEDURE spEnableDisableUser;

GO

CREATE PROC spEnableDisableUser
@Id int,
@IsDisabled Bit

AS
BEGIN  
UPDATE UserPasswords SET IsDisabled=@IsDisabled  WHERE  Id=@Id
END

GO

IF OBJECT_ID('spGetEngineerById', 'P') IS NOT NULL
DROP PROCEDURE spGetEngineerById;

GO

CREATE PROC spGetEngineerById  
@Id int
AS
BEGIN  
 SELECT * FROM Engineers   
 WHERE Id=@Id
END
GO



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


IF OBJECT_ID('spGetUserRoles', 'P') IS NOT NULL
DROP PROCEDURE spGetUserRoles;

GO

CREATE PROC spGetUserRoles

@UserName nvarchar(30) = null

AS
BEGIN
DECLARE 
	@ReturnTable TABLE 
	(

		Id int, 
		RoleType nvarchar(255),
		IsMember Bit
	)
DECLARE
	@Id int,
	@RoleType nvarchar(255),
	@IsMember Bit,
	@RoleId int = 0,
	@CountOf int =0,
	@UserId int =0



	DECLARE idxCursor CURSOR FOR SELECT Id, RoleType FROM Roles
	OPEN idxCursor

	FETCH NEXT FROM idxCursor INTO @RoleId, @RoleType
	WHILE @@FETCH_STATUS = 0 
	BEGIN
		
		SELECT @UserId = Id FROM UserPasswords WHERE UserName = @UserName;
		SELECT @CountOf = Count(*) FROM UserRoles WHERE RoleId = @RoleId AND UserId = @UserId;
		

		SELECT @IsMember = 0
		IF @CountOf > 0
		BEGIN
			SELECT @IsMember = 1
		END

		INSERT INTO @ReturnTable (Id, RoleType,IsMember)
		SELECT  @RoleId, @RoleType, @IsMember

		FETCH NEXT FROM idxCursor INTO @RoleId, @RoleType
	END
	
	CLOSE idxCursor    
	DEALLOCATE idxCursor
	SELECT * FROM 	@ReturnTable
END	

		

GO


GO
IF OBJECT_ID('spGetAllRoles', 'P') IS NOT NULL
DROP PROCEDURE spGetAllRoles;

GO

CREATE PROC spGetAllRoles  
AS
BEGIN  
 SELECT *, 0 As IsMember FROM Roles     
END

GO

IF OBJECT_ID('spUserIsInRole', 'P') IS NOT NULL
DROP PROCEDURE spUserIsInRole;

GO

CREATE PROC spUserIsInRole

@UserName nvarchar(30) = null,
@Role nvarchar(30) = null

AS
    BEGIN
	DECLARE @IsInRole bit 
	DECLARE @ReturnCount int

		SELECT @ReturnCount = count(*) 
		FROM UserRoles 
		INNER JOIN UserPasswords
		ON UserPasswords.Id = UserRoles.UserId
		INNER JOIN Roles
		ON Roles.Id = UserRoles.RoleId
		WHERE UserPasswords.UserName = @UserName
		AND Roles.RoleType = @Role;

		IF(@ReturnCount >= 1)
			SET @IsInRole = 1;
		ELSE
			SET @IsInRole = 0;
	SELECT @IsInRole as IsInRole;
END
GO


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

IF OBJECT_ID('spGetAllPlaces', 'P') IS NOT NULL
DROP PROCEDURE spGetAllPlaces;

GO

CREATE PROC spGetAllPlaces  
AS
BEGIN  
 SELECT TOP 100 * FROM Places  ORDER BY PlaceLocation  
END

GO



IF OBJECT_ID('rptYearSummaryByMonthByJobType', 'P') IS NOT NULL
DROP PROCEDURE rptYearSummaryByMonthByJobType

GO

CREATE PROC rptYearSummaryByMonthByJobType(@columnName nvarchar(100), @dateIn DateTime)

AS
BEGIN
DECLARE 
	@ReturnTable TABLE 
	(
	   JobTypeId int,
	   JobName nvarchar(255),
	   JobCode nvarchar(255),
	   JulTotal int,
	   AugTotal int,
	   SepTotal int,
	   OctTotal int,
	   NovTotal int,
	   DecTotal int,
	   JanTotal int,
	   FebTotal int,
	   MarTotal int,
	   AprTotal int,
	   MayTotal int,
	   JunTotal int,
	   SumTotal int,
  	   JulDate DateTime,
	   AugDate DateTime,
	   SepDate DateTime,
	   OctDate DateTime,
	   NovDate DateTime,
	   DecDate DateTime,
	   JanDate DateTime,
	   FebDate DateTime,
	   MarDate DateTime,
	   AprDate DateTime,
	   MayDate DateTime,
	   JunDate DateTime

	)
DECLARE
	@StartDate DateTime = DATEADD(mm, DATEDIFF(mm, 0, DateAdd(Month, -12, @dateIn)) - 1, 0),
	@JobTypeId int =0,
	@JobName nvarchar(255),
	@JobCode nvarchar(255),
	@Jan int = 0,
	@Feb int = 0,
	@Mar int = 0,
	@Apr int = 0,
	@May int = 0,
	@Jun int = 0,
	@Jul int = 0,
	@Aug int = 0,
	@Sep int = 0,
	@Oct int = 0,
	@Nov int = 0,
	@Dec int = 0,

	@JanD datetime = @dateIn,
	@FebD datetime = @dateIn,
	@MarD datetime = @dateIn,
	@AprD datetime = @dateIn,
	@MayD datetime = @dateIn,
	@JunD datetime = @dateIn,
	@JulD datetime = @dateIn,
	@AugD datetime = @dateIn,
	@SepD datetime = @dateIn,
	@OctD datetime = @dateIn,
	@NovD datetime = @dateIn,
	@DecD datetime = @dateIn,


	@MonthSelect int = 0,
	@RowCount int,
	@Total int = 0,
	@Month int = 0

	DECLARE idxCursor CURSOR FOR SELECT Id, Name, JobCode FROM JobTypes
	OPEN idxCursor

	FETCH NEXT FROM idxCursor INTO @JobTypeId, @JobName, @JobCode
	WHILE @@FETCH_STATUS = 0 
	BEGIN

		WHILE @Month < 12
		BEGIN
			SET @StartDate = DateAdd(Month, 1, @StartDate)	
			SELECT @MonthSelect = MONTH(@StartDate)

			SELECT @RowCount = COUNT(*) 
			FROM Projects 
			WHERE 
				CASE @columnName
					WHEN 'DateAssigned' THEN DateAssigned
					WHEN 'StartDate' THEN StartDate
					WHEN 'DateCreated' THEN DateCreated
					WHEN 'DateUpdated' THEN DateUpdated
					ELSE NULL
				END 
			>= @StartDate AND
	
				CASE @columnName
				WHEN 'DateAssigned' THEN DateAssigned
				WHEN 'StartDate' THEN StartDate
				WHEN 'DateCreated' THEN DateCreated
				WHEN 'DateUpdated' THEN DateUpdated
				ELSE NULL
			END
			< DateAdd(Month, 1, @StartDate)	AND JobTypeId = @JobTypeId

			IF @MonthSelect = 1
			BEGIN
				SELECT @Jan = @RowCount
				SELECT @JanD = @StartDate
			END

			IF @MonthSelect = 2
			BEGIN
				SELECT @Feb = @RowCount
				SELECT @FebD = @StartDate
			END

			IF @MonthSelect = 3
			BEGIN
				SELECT @Mar = @RowCount
				SELECT @MarD = @StartDate
			END

			IF @MonthSelect = 4
			BEGIN
				SELECT @Apr = @RowCount
				SELECT @AprD = @StartDate
			END

			IF @MonthSelect = 5
			BEGIN
				SELECT @May = @RowCount
				SELECT @MayD = @StartDate
			END

			IF @MonthSelect = 6
			BEGIN
				SELECT @Jun = @RowCount
				SELECT @JunD = @StartDate

			END

			IF @MonthSelect = 7
			BEGIN
				SELECT @Jul = @RowCount
				SELECT @JulD = @StartDate
			END

			IF @MonthSelect = 8
			BEGIN
				SELECT @Aug = @RowCount
				SELECT @AugD = @StartDate
			END

			IF @MonthSelect = 9
			BEGIN
				SELECT @Sep = @RowCount
				SELECT @SepD = @StartDate
			END

			IF @MonthSelect = 10
			BEGIN
				SELECT @Oct = @RowCount
				SELECT @OctD = @StartDate
			END

			IF @MonthSelect = 11
			BEGIN
				SELECT @Nov = @RowCount
				SELECT @NovD = @StartDate
			END

			IF @MonthSelect = 12
			BEGIN
				SELECT @Dec = @RowCount
				SELECT @DecD = @StartDate
			END
			
			SET @Total = @Total + @RowCount
			SET @Month = @Month + 1
		
		END


		INSERT INTO @ReturnTable 
			        (JobTypeId, JobName, JobCode,  JulTotal,AugTotal,SepTotal,OctTotal,NovTotal,DecTotal,JanTotal,FebTotal,MarTotal,AprTotal,MayTotal,JunTotal,SumTotal,
					                               JulDate, AugDate, SepDate, OctDate, NovDate, DecDate, JanDate, FebDate, MarDate, AprDate, MayDate, JunDate
					
					)
			SELECT  @JobTypeId,@JobName,@JobCode, @Jul,    @Aug,    @Sep,    @Oct,    @Nov,    @Dec,     @Jan,   @Feb,    @Mar,    @Apr,    @May,    @Jun,    @Total,
								                  @JulD,   @AugD,   @SepD,   @OctD,   @NovD,   @DecD,    @JanD,  @FebD,   @MarD,   @AprD,   @MayD,   @JunD;


		SET @Month = 0
		SET @StartDate = DATEADD(mm, DATEDIFF(mm, 0, DateAdd(Month, -12, @dateIn)) - 1, 0);

		SET @Jul= 0
		SET @Aug= 0
		SET @Sep= 0 
		SET @Oct= 0
		SET @Nov= 0
		SET @Dec= 0
		SET @Jan= 0
		SET @Feb= 0
		SET @Mar= 0
		SET @Apr= 0
		SET @May= 0
		SET @Jun= 0
		SET @Total= 0

		FETCH NEXT FROM idxCursor INTO @JobTypeId, @JobName, @JobCode
	END
	CLOSE idxCursor    
	DEALLOCATE idxCursor

	SELECT * FROM 	@ReturnTable
END

GO

IF OBJECT_ID('spGetRequirementsByProjectId', 'P') IS NOT NULL
DROP PROCEDURE spGetRequirementsByProjectId;

GO

CREATE PROC spGetRequirementsByProjectId  
@ProjectId int
AS
BEGIN  
 SELECT 
	Requirements.Id,
	Requirements.RequirementId,
	RequirementType.RequirementName,
	Requirements.ProjectId,
	Requirements.Required,
	Requirements.StartDate,
	Requirements.FinishDate,
	Requirements.CurrentComment,
	Requirements.DateCreated, 
	Requirements.DateUpdated
 
 FROM Requirements
 INNER JOIN RequirementType
 ON  Requirements.RequirementId =  RequirementType.Id 
 WHERE Requirements.ProjectId=@ProjectId
END

GO


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

IF OBJECT_ID('spGetUserPasswordByInitials', 'P') IS NOT NULL
DROP PROCEDURE spGetUserPasswordByInitials;

GO

CREATE PROC spGetUserPasswordByInitials  
@Initials nvarchar(5)
AS
BEGIN  
 SELECT * FROM Engineers   
 WHERE Initials=@Initials
END


GO

IF OBJECT_ID('spGetAllDistricts', 'P') IS NOT NULL
DROP PROCEDURE spGetAllDistricts;

GO

CREATE PROC spGetAllDistricts  
AS
BEGIN  
 SELECT * FROM Districts WHERE Id > 0  
END
GO

IF OBJECT_ID('spDeleteProject', 'P') IS NOT NULL
DROP PROCEDURE spDeleteProject;

GO
CREATE PROC spDeleteProject
@Id int
AS
BEGIN  
	DELETE FROM Requirements WHERE ProjectId=@Id ;
	DELETE FROM Projects WHERE Id=@Id;
END
GO

IF OBJECT_ID('spAddDistrict', 'P') IS NOT NULL
DROP PROCEDURE spAddDistrict;

GO
	
CREATE PROC spAddDistrict
@Name nvarchar(20),
@Code nvarchar(4)

AS
BEGIN  
 INSERT INTO Districts (Name,Code,DateCreated,DateUpdated)  
 VALUES (@Name,@Code,GetDate(),GetDate())  
END;

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

IF OBJECT_ID('spAddJobType', 'P') IS NOT NULL
DROP PROCEDURE spAddJobType;

GO
	

CREATE PROC spAddJobType
@Name nvarchar(255),
@JobCode nvarchar(20)

AS
BEGIN  
 INSERT INTO JobTypes (Name,JobCode,DateCreated,DateUpdated)  
 VALUES (@Name,@JobCode,GetDate(),GetDate())  
END;

GO

IF OBJECT_ID('spAddProject', 'P') IS NOT NULL
DROP PROCEDURE spAddProject;

GO
	
CREATE PROC spAddProject

@Location nvarchar(80),
@DistrictId int,
@CurrRemark nvarchar(100),
@Rush bit,
@ProjNo nvarchar(20),
@FiveDigit nvarchar(10),
@CDs nvarchar(16),
@JobTypeId int,
@HeadEngineerId int,
@DesignEngineerId int,
@CurrentComment nvarchar(255),
@DateAssigned datetime = null,
@StartDate datetime = null
--	CreateDate datetime
AS
BEGIN  
 INSERT INTO Projects (Location,DistrictId,CurrRemark,Rush,ProjNo,FiveDigit,CDs,JobTypeId,HeadEngineerId,DesignEngineerId,
 CurrentComment,
 DateAssigned,StartDate,DateCreated,DateUpdated)  
 VALUES (@Location,@DistrictId,@CurrRemark,@Rush,@ProjNo,@FiveDigit,@CDs,@JobTypeId,@HeadEngineerId,@DesignEngineerId,
 @CurrentComment,
 @DateAssigned,@StartDate, GETDATE(),GETDATE())  
END;

GO


IF OBJECT_ID('spDeleteDistrict', 'P') IS NOT NULL
DROP PROCEDURE spDeleteDistrict;

GO

CREATE PROC spDeleteDistrict
@Id int
AS
BEGIN  
 DELETE FROM Districts
 WHERE Id=@Id   
END

GO

IF OBJECT_ID('spDeleteEngineer', 'P') IS NOT NULL
DROP PROCEDURE spDeleteEngineer;

GO

CREATE PROC spDeleteEngineer
@Id int
AS
BEGIN  
 DELETE FROM Engineers
 WHERE Id=@Id;
 
 DELETE FROM UserPasswords
 WHERE Id=@Id;   


END
GO

IF OBJECT_ID('spDeleteJobType', 'P') IS NOT NULL
DROP PROCEDURE spDeleteJobType;

GO

CREATE PROC spDeleteJobType
@Id int
AS
BEGIN  
 DELETE FROM JobTypes
 WHERE Id=@Id   
END

GO

IF OBJECT_ID('spGetAllEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllEngineers;

GO

CREATE PROC spGetAllEngineers  
AS
BEGIN  
 SELECT * FROM Engineers   
END

GO

IF OBJECT_ID('spGetAllJobTypes', 'P') IS NOT NULL
DROP PROCEDURE spGetAllJobTypes;

GO

CREATE PROC spGetAllJobTypes  
AS
BEGIN  
 SELECT * FROM JobTypes   
END
GO


IF OBJECT_ID('spGetAllProjects', 'P') IS NOT NULL
DROP PROCEDURE spGetAllProjects;

GO

CREATE PROC spGetAllProjects  
AS
BEGIN  
 SELECT TOP 100 * FROM Projects ORDER BY Id
 END
 GO

 IF OBJECT_ID('spGetDistrict', 'P') IS NOT NULL
DROP PROCEDURE spGetDistrict;

GO

CREATE PROC spGetDistrict  
@Id int
AS
BEGIN  
 SELECT * FROM Districts   
 WHERE Id=@Id
END
GO


IF OBJECT_ID('spGetAdminUserById', 'P') IS NOT NULL
DROP PROCEDURE spGetAdminUserById;

GO


CREATE PROC spGetAdminUserById  
@Id int
AS
BEGIN  
 
  
  
 SELECT UserPasswords.Id, 
Engineers.FirstName, Engineers.LastName, Engineers.Email, Engineers.Initials, Engineers.Phone,
UserPasswords.UserName, UserPasswords.IsDisabled, UserPasswords.DateCreated, UserPasswords.DateUpdated 
FROM UserPasswords   
	LEFT JOIN Engineers 
	ON Engineers.Id = UserPasswords.Id
WHERE  UserPasswords.Id=@Id;


END

GO


IF OBJECT_ID('spGetUsersWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetUsersWithSearch;

GO


CREATE PROC spGetUsersWithSearch  
@word0 nvarchar(80) = null,
@word1 nvarchar(80) = null,
@word2 nvarchar(80) = null,
@word3 nvarchar(80) = null,
@word4 nvarchar(80) = null,
@word5 nvarchar(80) = null,
@word6 nvarchar(80) = null,
@word7 nvarchar(80) = null,
@word8 nvarchar(80) = null,
@word9 nvarchar(80) = null,
@word10 nvarchar(80) = null

AS

BEGIN
        SELECT *
        FROM Engineers
        WHERE
                (@word0 IS NULL OR ((FirstName LIKE '%' + @word0 + '%' ) OR (LastName LIKE '%' + @word0 + '%' )))
            AND (@word1 IS NULL OR ((FirstName LIKE '%' + @word1 + '%' ) OR (LastName LIKE '%' + @word1 + '%' )))
			AND (@word2 IS NULL OR ((FirstName LIKE '%' + @word2 + '%' ) OR (LastName LIKE '%' + @word2 + '%' )))
			AND (@word3 IS NULL OR ((FirstName LIKE '%' + @word3 + '%' ) OR (LastName LIKE '%' + @word3 + '%' )))
			AND (@word4 IS NULL OR ((FirstName LIKE '%' + @word4 + '%' ) OR (LastName LIKE '%' + @word4 + '%' )))
			AND (@word5 IS NULL OR ((FirstName LIKE '%' + @word5 + '%' ) OR (LastName LIKE '%' + @word5 + '%' )))
			AND (@word6 IS NULL OR ((FirstName LIKE '%' + @word6 + '%' ) OR (LastName LIKE '%' + @word6 + '%' )))
			AND (@word7 IS NULL OR ((FirstName LIKE '%' + @word7 + '%' ) OR (LastName LIKE '%' + @word7 + '%' )))
			AND (@word8 IS NULL OR ((FirstName LIKE '%' + @word8 + '%' ) OR (LastName LIKE '%' + @word8 + '%' )))
			AND (@word9 IS NULL OR ((FirstName LIKE '%' + @word9 + '%' ) OR (LastName LIKE '%' + @word9 + '%' )))
			
            
        -- OPTION (RECOMPILE) ---<<<<use if on for SQL 2008 SP1 CU5 (10.0.2746) and later
    END

GO

IF OBJECT_ID('spGetJobType', 'P') IS NOT NULL
DROP PROCEDURE spGetJobType;

GO

CREATE PROC spGetJobType 
@Id int
AS
BEGIN  
 SELECT * FROM JobTypes   
 WHERE Id=@Id
END
GO

IF OBJECT_ID('spGetProject', 'P') IS NOT NULL
DROP PROCEDURE spGetProject;

GO

CREATE PROC spGetProject  
@Id int
AS
BEGIN  
 SELECT * FROM Projects   
 WHERE Id=@Id
END
GO

IF OBJECT_ID('spGetProjectsWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetProjectsWithSearch;

GO


CREATE PROC spGetProjectsWithSearch  
@word0 nvarchar(80) = null,
@word1 nvarchar(80) = null,
@word2 nvarchar(80) = null,
@word3 nvarchar(80) = null,
@word4 nvarchar(80) = null,
@word5 nvarchar(80) = null,
@word6 nvarchar(80) = null,
@word7 nvarchar(80) = null,
@word8 nvarchar(80) = null,
@word9 nvarchar(80) = null,
@word10 nvarchar(80) = null,
@districtId int = 0,
@jobTypeId int = 0,
@FieldSelected nvarchar(80),
@startDate DateTime


AS

BEGIN
        SELECT TOP 100 *
        FROM Projects
        WHERE
			CASE @FieldSelected
						WHEN 'StartDate' THEN StartDate
						WHEN 'DateCreated' THEN DateCreated
						WHEN 'DateUpdated' THEN DateUpdated
						ELSE NULL
			END >= @StartDate 

            AND (@word0 IS NULL OR (Location LIKE '%' + @word0 + '%' ))
            AND (@word1 IS NULL OR (Location LIKE '%' + @word1 + '%' ))
			AND (@word2 IS NULL OR (Location LIKE '%' + @word2 + '%' ))
			AND (@word3 IS NULL OR (Location LIKE '%' + @word3 + '%' ))
			AND (@word4 IS NULL OR (Location LIKE '%' + @word4 + '%' ))
			AND (@word5 IS NULL OR (Location LIKE '%' + @word5 + '%' ))
			AND (@word6 IS NULL OR (Location LIKE '%' + @word6 + '%' ))
			AND (@word7 IS NULL OR (Location LIKE '%' + @word7 + '%' ))
			AND (@word8 IS NULL OR (Location LIKE '%' + @word8 + '%' ))
			AND (@word9 IS NULL OR (Location LIKE '%' + @word9 + '%' ))
			AND (@districtId = 0 OR (DistrictId = @districtId))
			AND (@jobTypeId = 0 OR (jobTypeId = @jobTypeId))
			
            
        -- OPTION (RECOMPILE) ---<<<<use if on for SQL 2008 SP1 CU5 (10.0.2746) and later
END

GO

IF OBJECT_ID('spSaveDistrict', 'P') IS NOT NULL
DROP PROCEDURE spSaveDistrict;

GO

CREATE PROC spSaveDistrict
@Id int,
@Name nvarchar(20),
@Code nvarchar(4)

AS
BEGIN  
UPDATE Districts SET
Name = @Name, 
Code= @Code, 
DateUpdated = GetDate()
 WHERE Id=@Id   
END
GO


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
END
GO

IF OBJECT_ID('spSaveJobType', 'P') IS NOT NULL
DROP PROCEDURE spSaveJobType;

GO

CREATE PROC spSaveJobType
@Id int,
@Name nvarchar(255),
@JobCode nvarchar(20)

AS
BEGIN  
UPDATE JobTypes SET
Name = @Name, 
JobCode= @JobCode, 
DateUpdated = GetDate()
 WHERE Id=@Id   
END

GO

IF OBJECT_ID('spSaveProject', 'P') IS NOT NULL
DROP PROCEDURE spSaveProject;

GO

CREATE PROC spSaveProject
@Id int,
@Location nvarchar(80), 
@DistrictId int, 
@CurrRemark nvarchar(80),
@Rush bit,
@ProjNo nvarchar(20),
@FiveDigit nvarchar(10),
@CDs nvarchar(16), 
@JobTypeId int,
@HeadEngineerId int,
@DesignEngineerId int,
@CurrentComment nvarchar(255),
@DateAssigned datetime,
@StartDate datetime

AS
BEGIN  
UPDATE Projects SET
Location = @Location, 
DistrictId= @DistrictId, 
CurrRemark= @CurrRemark,
Rush = @Rush,
ProjNo = @ProjNo,
FiveDigit = @FiveDigit,
CDs = @CDs, 
JobTypeId = @JobTypeId,
HeadEngineerId = @HeadEngineerId,
DesignEngineerId = @DesignEngineerId,
CurrentComment = @CurrentComment,
DateAssigned = @DateAssigned,
StartDate = @StartDate,
DateUpdated = GetDate()
 WHERE Id=@Id   
END
GO



IF OBJECT_ID('spUpdateUserField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserField

GO

CREATE PROC spUpdateUserField

@Id int,
@columnName nvarchar(100),
@StringIn nvarchar(255)
AS 
BEGIN
IF @columnName = 'FirstName'
BEGIN
	UPDATE Engineers SET FirstName = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END
IF @columnName = 'LastName'
BEGIN
	UPDATE Engineers SET LastName = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'Email'
BEGIN
	UPDATE Engineers SET Email = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'Initials'
BEGIN
	UPDATE Engineers SET Initials = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'UserName'
BEGIN
	UPDATE Engineers SET UserName = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'Phone'
BEGIN
	UPDATE Engineers SET Phone = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END
END
GO

IF OBJECT_ID('spUpdateProjectDateField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectDateField

GO

CREATE PROC spUpdateProjectDateField
@columnName nvarchar(100),
@Id int,
@DateIn DateTime

AS 
BEGIN
IF @columnName = 'DateAssigned'
BEGIN
	UPDATE Projects SET DateAssigned = @DateIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'StartDate'
BEGIN
	UPDATE Projects SET StartDate = @DateIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO

IF OBJECT_ID('spUpdateProjectStringField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectStringField

GO

CREATE PROC spUpdateProjectStringField
@columnName nvarchar(100),
@Id int,
@StringIn nvarchar(255)
AS 
BEGIN
IF @columnName = 'Location'
BEGIN
	UPDATE Projects SET Location = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'CurrRemark'
BEGIN
	UPDATE Projects SET CurrRemark = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'ProjNo'
BEGIN
	UPDATE Projects SET ProjNo = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'FiveDigit'
BEGIN
	UPDATE Projects SET FiveDigit = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'CDs'
BEGIN
	UPDATE Projects SET CDs = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'CurrentComment'
BEGIN
	UPDATE Projects SET CurrentComment = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO

IF OBJECT_ID('spUpdateProjectIntField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectIntField

GO

CREATE PROC spUpdateProjectIntField
@columnName nvarchar(100),
@Id int,
@IntIn int
AS 
BEGIN
IF @columnName = 'DistrictId'
BEGIN
	UPDATE Projects SET DistrictId = @IntIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'JobTypeId'
BEGIN
	UPDATE Projects SET JobTypeId = @IntIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'HeadEngineerId'
BEGIN
	UPDATE Projects SET HeadEngineerId = @IntIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'DesignEngineerId'
BEGIN
	UPDATE Projects SET DesignEngineerId = @IntIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO


IF OBJECT_ID('spUserIsValid', 'P') IS NOT NULL
DROP PROCEDURE spUserIsValid;

GO
	
CREATE PROC spUserIsValid

@UserName nvarchar(30) = null,
@Password nvarchar(30) = null


AS
    BEGIN
	DECLARE @IsValid bit 

	DECLARE @ReturnCount int
	SELECT @ReturnCount = count(*) FROM UserPasswords WHERE UserName = @UserName AND Password = @Password AND IsDisabled=0
    IF(@ReturnCount = 1)
		SET @IsValid = 1
	ELSE
		SET @IsValid = 0
    END
	SELECT @IsValid
;
GO




IF OBJECT_ID('spGetAllUserDetails', 'P') IS NOT NULL
DROP PROCEDURE spGetAllUserDetails;

GO

CREATE PROC spGetAllUserDetails  
AS
BEGIN  
 SELECT UserPasswords.Id, UserPasswords.UserName, Password,
 FirstName,LastName,Email,Initials,Phone, 
 UserPasswords.DateCreated,
 UserPasswords.DateUpdated,
 UserPasswords.DateAccessed

 FROM Engineers   
 INNER JOIN UserPasswords
 ON Engineers.Id = UserPasswords.Id
END
GO


IF OBJECT_ID('spUpdateUserDetail', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserDetail;

GO
	
CREATE PROC spUpdateUserDetail

@Id int,
@UserName nvarchar(20),
@PassWord nvarchar(20),

@FirstName nvarchar(30)= null,
@LastName nvarchar(30)= null,
@Email nvarchar(100)= null,
@Initials nvarchar(5)= null,
@Phone nvarchar(20)

AS
BEGIN  

	UPDATE UserPasswords
	SET 
		UserName= @UserName,
		PassWord= @PassWord,
		DateUpdated= GETDATE()
	WHERE
		Id = @Id;

	UPDATE Engineers
	SET 
		FirstName= @FirstName,
		LastName= @LastName,
		Email= @Email,
		Initials= @Initials,
		UserName= @UserName,
		Phone= @Phone,
		DateUpdated= GETDATE()
	WHERE
		Id = @Id;
 END;

GO

IF OBJECT_ID('spDeleteUserDetail', 'P') IS NOT NULL
DROP PROCEDURE spDeleteUserDetail;

GO
	
CREATE PROC spDeleteUserDetail

@Id int

AS
BEGIN  

DELETE FROM UserPasswords WHERE Id = @Id;
DELETE FROM Engineers WHERE Id = @Id;

END;

GO
IF OBJECT_ID('spGetUserPasswordByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetUserPasswordByUserName;

GO

CREATE PROC spGetUserPasswordByUserName  
@UserName nvarchar(15)
AS
BEGIN  
 SELECT Id FROM UserPasswords   /*Only return the Id - UserName is given and password protected*/
 WHERE UserName=@UserName
END;

GO


IF OBJECT_ID('spAddUserPassword', 'P') IS NOT NULL
DROP PROCEDURE spAddUserPassword;
GO
CREATE PROC spAddUserPassword

@UserName nvarchar(20),
@Password nvarchar(20),
@FirstName nvarchar(30)= null,
@LastName nvarchar(30)= null,
@Email nvarchar(100),
@Initials nvarchar(5)= null,
@Phone nvarchar(20)= null

AS
BEGIN  
DECLARE @Id int
DECLARE @table table (id int)

 INSERT INTO UserPasswords 
 (UserName,Password,RememberMe, IsDisabled,DateCreated,DateUpdated,DateAccessed )  
 OUTPUT inserted.id into @table
 VALUES (@UserName,@Password,1,0,GETDATE(), GETDATE(), GETDATE())  ;

 SELECT @Id = id from @table;
 
  INSERT INTO Engineers 
 (Id,FirstName,LastName,Email,Initials,UserName,Phone,
 DateCreated,DateUpdated )  
 VALUES ( @Id, @FirstName,@LastName,@Email,@Initials,@UserName,@Phone,GETDATE(), GETDATE());

 -- RoleId=2 is the Guest Role
 INSERT INTO UserRoles(UserId,RoleId) VALUES (@Id,2);

 END

GO



IF OBJECT_ID('spDeleteEngineerByUserName', 'P') IS NOT NULL
DROP PROCEDURE spDeleteEngineerByUserName;

GO

CREATE PROC spDeleteEngineerByUserName
@UserName nvarchar(30)
AS
BEGIN  
 DELETE FROM Engineers
 WHERE UserName=@UserName;

 DELETE FROM UserPasswords
 WHERE UserName=@UserName;
    
END
GO

IF OBJECT_ID('spGetUserDetailByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetUserDetailByUserName;

GO

CREATE PROC spGetUserDetailByUserName  
@UserName nvarchar(30)
AS
BEGIN  
 SELECT 
	 UserPasswords.Id,
	 UserPasswords.UserName,
	 UserPasswords.Password,
	 Engineers.FirstName,
	 Engineers.LastName,
	 Engineers.Email,
	 Engineers.Phone,
	 Engineers.DateCreated,
	 UserPasswords.DateUpdated,
	 UserPasswords.DateAccessed
 FROM Engineers 
 INNER JOIN UserPasswords
 ON UserPasswords.Id = Engineers.Id
 WHERE UserPasswords.UserName=@UserName
END

GO

IF OBJECT_ID('spGetAllHeadEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllHeadEngineers;

GO

CREATE PROC spGetAllHeadEngineers  
AS
BEGIN  
 SELECT Engineers.* FROM Engineers  
 INNER JOIN UserRoles
 ON Engineers.Id = UserRoles.UserId 
 WHERE  UserRoles.RoleId =4
END

GO




SELECT name AS procedure_name 
    ,SCHEMA_NAME(schema_id) AS schema_name
    ,type_desc
    ,create_date
    ,modify_date
FROM sys.procedures;
GO
