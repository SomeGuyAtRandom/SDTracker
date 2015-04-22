USE [SignalDB];
-- The purpose of this tools is to make sure ONLY those stored proceedurs created are those listed in the Build folder
-- before count 37

-- Note!!! The following are not do be deleted
-- They are required by ADOTransfer


-- Chaff
IF OBJECT_ID('spUpdateProjectDateTimeField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectDateTimeField;

IF OBJECT_ID('spUpdateRequirementsFieldByIdDateTime', 'P') IS NOT NULL
DROP PROCEDURE spUpdateRequirementsFieldByIdDateTime;

IF OBJECT_ID('spUpdateRequirementsFieldByIdString', 'P') IS NOT NULL
DROP PROCEDURE spUpdateRequirementsFieldByIdString;


-- End of Chaff
--spGetEngineerByUserName


IF OBJECT_ID('spGetEngineerByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetEngineerByUserName;


IF OBJECT_ID('spUpdateUserPassword', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserPassword;


IF OBJECT_ID('spGetPlacesWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetPlacesWithSearch;


IF OBJECT_ID('spTransferProjects', 'P') IS NOT NULL
DROP PROCEDURE spTransferProjects;


IF OBJECT_ID('spTransferPlaces', 'P') IS NOT NULL
DROP PROCEDURE spTransferPlaces;


IF OBJECT_ID('spDeleteProject', 'P') IS NOT NULL
DROP PROCEDURE spDeleteProject;

IF OBJECT_ID('spGetUserEngineerByEmail', 'P') IS NOT NULL
DROP PROCEDURE spGetUserEngineerByEmail;

IF OBJECT_ID('spEnableDisableUser', 'P') IS NOT NULL
DROP PROCEDURE spEnableDisableUser;

IF OBJECT_ID('spGetEngineerById', 'P') IS NOT NULL
DROP PROCEDURE spGetEngineerById;

IF OBJECT_ID('spUpdateUserRole', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserRole;

IF OBJECT_ID('spGetUserRoles', 'P') IS NOT NULL
DROP PROCEDURE spGetUserRoles;

IF OBJECT_ID('spGetAllRoles', 'P') IS NOT NULL
DROP PROCEDURE spGetAllRoles;

IF OBJECT_ID('spUserIsInRole', 'P') IS NOT NULL
DROP PROCEDURE spUserIsInRole;

IF OBJECT_ID('spGetUserDetailById', 'P') IS NOT NULL
DROP PROCEDURE spGetUserDetailById;

IF OBJECT_ID('spGetAllPlaces', 'P') IS NOT NULL
DROP PROCEDURE spGetAllPlaces;

IF OBJECT_ID('rptYearSummaryByMonthByJobType', 'P') IS NOT NULL
DROP PROCEDURE rptYearSummaryByMonthByJobType;

IF OBJECT_ID('spGetRequirementsByProjectId', 'P') IS NOT NULL
DROP PROCEDURE spGetRequirementsByProjectId;

IF OBJECT_ID('spGetAllDesignEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllDesignEngineers;

IF OBJECT_ID('spGetUserPasswordByInitials', 'P') IS NOT NULL
DROP PROCEDURE spGetUserPasswordByInitials;

IF OBJECT_ID('spGetAllDistricts', 'P') IS NOT NULL
DROP PROCEDURE spGetAllDistricts;

IF OBJECT_ID('spAddDistrict', 'P') IS NOT NULL
DROP PROCEDURE spAddDistrict;

IF OBJECT_ID('spAddEngineer', 'P') IS NOT NULL
DROP PROCEDURE spAddEngineer;

IF OBJECT_ID('spAddJobType', 'P') IS NOT NULL
DROP PROCEDURE spAddJobType;

IF OBJECT_ID('spAddProject', 'P') IS NOT NULL
DROP PROCEDURE spAddProject;

IF OBJECT_ID('spDeleteDistrict', 'P') IS NOT NULL
DROP PROCEDURE spDeleteDistrict;

IF OBJECT_ID('spDeleteEngineer', 'P') IS NOT NULL
DROP PROCEDURE spDeleteEngineer;

IF OBJECT_ID('spDeleteJobType', 'P') IS NOT NULL
DROP PROCEDURE spDeleteJobType;

IF OBJECT_ID('spGetAllEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllEngineers;

IF OBJECT_ID('spGetAllJobTypes', 'P') IS NOT NULL
DROP PROCEDURE spGetAllJobTypes;

IF OBJECT_ID('spGetAllProjects', 'P') IS NOT NULL
DROP PROCEDURE spGetAllProjects;

IF OBJECT_ID('spGetDistrict', 'P') IS NOT NULL
DROP PROCEDURE spGetDistrict;

IF OBJECT_ID('spGetAdminUserById', 'P') IS NOT NULL
DROP PROCEDURE spGetAdminUserById;

IF OBJECT_ID('spGetUsersWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetUsersWithSearch;

IF OBJECT_ID('spGetJobType', 'P') IS NOT NULL
DROP PROCEDURE spGetJobType;

IF OBJECT_ID('spGetProject', 'P') IS NOT NULL
DROP PROCEDURE spGetProject;

IF OBJECT_ID('spGetProjectsWithSearch', 'P') IS NOT NULL
DROP PROCEDURE spGetProjectsWithSearch;

IF OBJECT_ID('spSaveDistrict', 'P') IS NOT NULL
DROP PROCEDURE spSaveDistrict;

IF OBJECT_ID('spSaveEngineer', 'P') IS NOT NULL
DROP PROCEDURE spSaveEngineer;

IF OBJECT_ID('spSaveJobType', 'P') IS NOT NULL
DROP PROCEDURE spSaveJobType;

IF OBJECT_ID('spSaveProject', 'P') IS NOT NULL
DROP PROCEDURE spSaveProject;

IF OBJECT_ID('spUpdateUserField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserField;

IF OBJECT_ID('spUpdateProjectDateField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectDateField;

IF OBJECT_ID('spUpdateProjectStringField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectStringField;

IF OBJECT_ID('spUpdateProjectIntField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectIntField;

IF OBJECT_ID('spUserIsValid', 'P') IS NOT NULL
DROP PROCEDURE spUserIsValid;

IF OBJECT_ID('spGetAllUserDetails', 'P') IS NOT NULL
DROP PROCEDURE spGetAllUserDetails;

IF OBJECT_ID('spUpdateUserDetail', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserDetail;

IF OBJECT_ID('spDeleteUserDetail', 'P') IS NOT NULL
DROP PROCEDURE spDeleteUserDetail;

IF OBJECT_ID('spGetUserPasswordByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetUserPasswordByUserName;

IF OBJECT_ID('spAddUserPassword', 'P') IS NOT NULL
DROP PROCEDURE spAddUserPassword;

IF OBJECT_ID('spDeleteEngineerByUserName', 'P') IS NOT NULL
DROP PROCEDURE spDeleteEngineerByUserName;

IF OBJECT_ID('spGetUserDetailByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetUserDetailByUserName;

IF OBJECT_ID('spGetAllHeadEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllHeadEngineers;



SELECT name AS procedure_name 
    ,SCHEMA_NAME(schema_id) AS schema_name
    ,type_desc
    ,create_date
    ,modify_date
FROM sys.procedures;
GO

-- count 0