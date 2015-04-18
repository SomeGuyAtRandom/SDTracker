using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADOTransfer
{
    public class SQLScripts
    {
        public static string createTable_sdn_main_projects()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("CREATE TABLE sdn_main_projects ( ");
            sb.Append("MAIN_PROJECTS_KEY int NOT NULL DEFAULT '0', ");
            sb.Append("LOC varchar(80) DEFAULT NULL, ");
            sb.Append("SYS_NUM int DEFAULT NULL, ");
            sb.Append("INT_NUM int DEFAULT NULL, ");
            sb.Append("ROUTE varchar(10) DEFAULT NULL, ");
            sb.Append("JOB_TYPE_CODE varchar(20) DEFAULT NULL, ");
            sb.Append("JOB_TYPE_KEY int DEFAULT NULL, ");
            sb.Append("PROJ_NO varchar(10) DEFAULT NULL, ");
            sb.Append(" CD_NUMS varchar(16) DEFAULT NULL, ");
            sb.Append(" DIST_ID int DEFAULT NULL, ");
            sb.Append(" DIST_CODE varchar(25) DEFAULT NULL, ");
            sb.Append("RTP_TO_USERNAME varchar(255) DEFAULT NULL, ");
            sb.Append("DES_ENGR_USERNAME varchar(255) DEFAULT NULL, ");
            sb.Append("DATE_ASSIGNED date DEFAULT NULL, ");
            sb.Append("TO_BASE date DEFAULT NULL, ");
            sb.Append("FROM_BASE date DEFAULT NULL, ");
            sb.Append("NEED_BSL bit DEFAULT NULL, ");
            sb.Append("TO_BSL date DEFAULT NULL, ");
            sb.Append("FROM_BSL date DEFAULT NULL, ");
            sb.Append("DOT_DUE_DATE date DEFAULT NULL, ");
            sb.Append("DOT_APPROVED date DEFAULT NULL, ");
            sb.Append("NEED_DWP bit DEFAULT NULL, ");
            sb.Append("TO_DWP date DEFAULT NULL, ");
            sb.Append("FROM_DWP date DEFAULT NULL, ");
            sb.Append("TO_DISTRICT date DEFAULT NULL, ");
            sb.Append("FROM_DISTRICT date DEFAULT NULL, ");
            sb.Append("TO_TIMING date DEFAULT NULL, ");
            sb.Append("FROM_TIMING date DEFAULT NULL, ");
            sb.Append("FIELD_CHECK date DEFAULT NULL, ");
            sb.Append("PRE_DESIGN date DEFAULT NULL, ");
            sb.Append("NEED_REVIEW bit DEFAULT NULL, ");
            sb.Append("TO_REVIEW date DEFAULT NULL, ");
            sb.Append("REVIEW_NO int DEFAULT NULL, ");
            sb.Append("FROM_REVIEW date DEFAULT NULL, ");
            sb.Append("COORD date DEFAULT NULL, ");
            sb.Append("CURR_REMARK varchar(255) DEFAULT NULL, ");
            sb.Append("CURR_COMMENT varchar(255) DEFAULT NULL, ");
            sb.Append("NEW_SIG bit DEFAULT NULL, ");
            sb.Append("LT_PH int DEFAULT NULL, ");
            sb.Append("LAST_UPDATED datetime DEFAULT NULL, ");
            sb.Append("LAST_UPDATED_USERNAME varchar(255) DEFAULT NULL, ");
            sb.Append("RUSH bit DEFAULT NULL, ");
            sb.Append("DATE_CREATED date DEFAULT NULL, ");
            sb.Append("NEED_DISTRICT bit DEFAULT NULL, ");
            sb.Append("NEED_TIMING bit DEFAULT NULL, ");
            sb.Append("NEED_OTHER_AGENCIES bit DEFAULT NULL, ");
            sb.Append("TO_OTHER_AGENCIES date DEFAULT NULL, ");
            sb.Append("FROM_OTHER_AGENCIES date DEFAULT NULL, ");
            sb.Append("NEED_BASE bit DEFAULT NULL, ");
            sb.Append("NEED_FIELD_CHECK bit DEFAULT NULL, ");
            sb.Append(" NEED_PRE_DESIGN bit DEFAULT NULL, ");
            sb.Append("PROJ_TO_BASE date DEFAULT NULL, ");
            sb.Append("PROJ_TO_BSL date DEFAULT NULL, ");
            sb.Append("PROJ_TO_DISTRICT date DEFAULT NULL, ");
            sb.Append("PROJ_TO_DWP date DEFAULT NULL, ");
            sb.Append("PROJ_TO_OTHER_AGENCIES date DEFAULT NULL, ");
            sb.Append("PROJ_TO_TIMING date DEFAULT NULL, ");
            sb.Append("PROJ_TO_REVIEW date DEFAULT NULL, ");
            sb.Append("PROJ_FROM_BASE datetime DEFAULT NULL, ");
            sb.Append("PROJ_FROM_BSL date DEFAULT NULL, ");
            sb.Append("PROJ_FROM_DISTRICT date DEFAULT NULL, ");
            sb.Append("PROJ_FROM_DWP date DEFAULT NULL, ");
            sb.Append("PROJ_FROM_OTHER_AGENCIES date DEFAULT NULL, ");
            sb.Append("PROJ_FROM_TIMING date DEFAULT NULL, ");
            sb.Append("PROJ_FROM_REVIEW date DEFAULT NULL, ");
            sb.Append("PROJ_FIELD_CHECK date DEFAULT NULL, ");
            sb.Append("PROJ_PRE_DESIGN date DEFAULT NULL, ");
            sb.Append("PROJ_COORD date DEFAULT NULL, ");
            sb.Append("PROJ_DOT_APPROVED date DEFAULT NULL, ");
            sb.Append("PROJ_START_DATE date DEFAULT NULL, ");
            sb.Append("PROJ_COMPLETED date DEFAULT NULL, ");
            sb.Append("START_DATE date DEFAULT NULL, ");
            sb.Append("COMPLETED date DEFAULT NULL, ");
            sb.Append("TO_AS_BUILT date DEFAULT NULL, ");
            sb.Append("FROM_AS_BUILT date DEFAULT NULL, ");
            sb.Append("PROJ_TO_AS_BUILT date DEFAULT NULL, ");
            sb.Append("PROJ_FROM_AS_BUILT date DEFAULT NULL, ");
            sb.Append("BSL_SIGNED date DEFAULT NULL, ");
            sb.Append("PROJ_BSL_SIGNED date DEFAULT NULL, ");
            sb.Append("PRIMARY KEY (MAIN_PROJECTS_KEY) ");
            sb.Append(") ");
            
            sb.Append("");
            return sb.ToString();
        }

        public static string dropTable_sdn_main_projects()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("IF OBJECT_ID('dbo.sdn_main_projects', 'U') IS NOT NULL ");
            sb.Append("DROP TABLE dbo.sdn_main_projects");
            return sb.ToString();
        }

        public static string dropTable_Places()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("IF OBJECT_ID('dbo.Places', 'U') IS NOT NULL ");
            sb.Append("DROP TABLE dbo.Places");
            return sb.ToString();
        }

        public static string createTable_Places()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("CREATE TABLE Places ");
            sb.Append("( ");
            sb.Append("	Id int IDENTITY PRIMARY KEY, ");
            sb.Append("	FiveDigit nvarchar(10), ");
            sb.Append("	DistrictId int FOREIGN KEY REFERENCES Districts(Id) default null, ");
            sb.Append("	PlaceLocation nvarchar(80), ");
            sb.Append("	DateCreated DateTime not null,  ");
            sb.Append("	DateUpdated DateTime not null, ");
            sb.Append("	unique(FiveDigit) ");
            sb.Append(") ");
            sb.Append("");
            return sb.ToString();
        }

        public static string select_Places()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("SELECT  ");
            sb.Append("	cs_route_code.ROUTE AS FiveDigit, ");
            sb.Append("	cs_dot_district.DIST_ID AS DistrictId, ");
            sb.Append("	cs_street_names.LOC AS PlaceLocation ");
            sb.Append("FROM cs_route_code ");

            sb.Append("	INNER JOIN cs_dot_district ");
            sb.Append("	ON cs_dot_district.SYS_NUM = cs_route_code.SYS_NUM ");
            sb.Append("	AND cs_dot_district.INT_NUM = cs_route_code.INT_NUM ");

            sb.Append("	INNER JOIN cs_street_names ");
            sb.Append("	ON cs_street_names.SYS_NUM = cs_route_code.SYS_NUM ");
            sb.Append("	AND cs_street_names.INT_NUM = cs_route_code.INT_NUM ");

            return sb.ToString();
 
        }
        
    }
}
