using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;



namespace ADOTransfer
{
    public class TransferTable
    {
        private String MySqlCnnStr = "User ID=randrade; Password=0racle8i; Host=localhost; Port=3306; Database=signaldesigntrack; Protocol=TCP; Compress=false; Pooling=true; Min Pool Size=0;Max Pool Size=100; Connection Lifetime=0;";
        private String MSSqlCnnStr = "server=.; data source=.\\SQLEXPRESS; database=SignalDB; integrated security=SSPI";

        private DateTime dLow = DateTime.Parse("2000-01-01");
        private DateTime dHi = DateTime.Parse("2100-01-01");

        public TransferTable()
        {
 
        }

        public void populatePlaces()
        {
            string sql;
            SqlCommand MScmd;
            try
            {
                SqlConnection MScnn = new SqlConnection(MSSqlCnnStr);
                MScnn.Open();
                sql = SQLScripts.dropTable_Places();
                MScmd = new SqlCommand(sql, MScnn);
                MScmd.ExecuteNonQuery();

                sql = SQLScripts.createTable_Places();
                MScmd = new SqlCommand(sql, MScnn);
                MScmd.ExecuteNonQuery();

                MySqlConnection cnn = new MySqlConnection(MySqlCnnStr);
                sql = SQLScripts.select_Places();
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rs;
                cnn.Open();

                rs = cmd.ExecuteReader();
                
                while (rs.Read())
                {
                    MScmd = new SqlCommand("spTransferPlaces", MScnn);
                    
                    MScmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pFiveDigit = new SqlParameter();
                    SqlParameter pDistrictId = new SqlParameter();
                    SqlParameter pPlace = new SqlParameter();

                    pFiveDigit.Value = rs.GetString(rs.GetOrdinal("FiveDigit"));
                    pDistrictId.Value = rs.GetInt32(rs.GetOrdinal("DistrictId"));
                    pPlace.Value = rs.GetString(rs.GetOrdinal("PlaceLocation")).Trim();

                    pFiveDigit.ParameterName = "@FiveDigit";
                    pDistrictId.ParameterName = "@DistrictId";
                    pPlace.ParameterName = "@PlaceLocation";


                    MScmd.Parameters.Add(pFiveDigit);
                    MScmd.Parameters.Add(pDistrictId);
                    MScmd.Parameters.Add(pPlace);

                    Console.WriteLine("To save : " + rs.GetString(rs.GetOrdinal("FiveDigit")));

                    try { MScmd.ExecuteNonQuery(); }
                    catch (SqlException ex) { Console.WriteLine(ex); }

                    
                }
                MScnn.Close();
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void sdn_main_projects()
        {
            string sql;
            SqlCommand MScmd;
            try
            {
                SqlConnection MScnn = new SqlConnection(MSSqlCnnStr);
                MScnn.Open();
                sql = SQLScripts.dropTable_sdn_main_projects();

                MScmd = new SqlCommand(sql, MScnn);
                MScmd.ExecuteNonQuery();

                sql = SQLScripts.createTable_sdn_main_projects();
                MScmd = new SqlCommand(sql, MScnn);
                MScmd.ExecuteNonQuery();

                MySqlConnection cnn = new MySqlConnection(MySqlCnnStr);
                sql = "SELECT * FROM sdn_main_projects ORDER BY MAIN_PROJECTS_KEY ";
                cnn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rs;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    MScmd = new SqlCommand("spTransferProjects", MScnn);
                    MScmd.CommandType = CommandType.StoredProcedure;

                    foreach (String s in project_FIELD)
                    {
                        SqlParameter p = new SqlParameter();
                        p.ParameterName = "@" + s;

                        if (rs[s] != System.DBNull.Value)
                        {
                            if (rs[s].GetType().ToString().Equals("System.SByte"))
                            {
                                p.Value = rs.GetBoolean(s);
                            }
                            else if (rs[s].GetType().ToString().Equals("System.DateTime"))
                            {
                                if ((rs.GetDateTime(s).CompareTo(dLow) < 0) || (rs.GetDateTime(s).CompareTo(dHi) > 0))
                                {
                                }
                                else
                                {
                                    p.Value = rs.GetDateTime(s);

                                }
                            }
                            else
                            {
                                p.Value = rs[s];
                            }
                        }
                        //System.DateTime
                        MScmd.Parameters.Add(p);
                    }
                    Console.WriteLine("To save : " + rs[0].ToString());
                    MScmd.ExecuteNonQuery();

                }
                MScnn.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally 
            {
 
            }
 
        }

        string[] project_FIELD = 
        {
            "MAIN_PROJECTS_KEY",
            "LOC",
            "SYS_NUM",
            "INT_NUM",
            "ROUTE",
            "JOB_TYPE_CODE",
            "JOB_TYPE_KEY",
            "PROJ_NO",
            "CD_NUMS",
            "DIST_ID",
            "DIST_CODE",
            "RTP_TO_USERNAME",
            "DES_ENGR_USERNAME",
            "DATE_ASSIGNED",
            "TO_BASE",
            "FROM_BASE",
            "NEED_BSL",
            "TO_BSL",
            "FROM_BSL",
            "DOT_DUE_DATE",
            "DOT_APPROVED",
            "NEED_DWP",
            "TO_DWP",
            "FROM_DWP",
            "TO_DISTRICT",
            "FROM_DISTRICT",
            "TO_TIMING",
            "FROM_TIMING",
            "FIELD_CHECK",
            "PRE_DESIGN",
            "NEED_REVIEW",
            "TO_REVIEW",
            "REVIEW_NO",
            "FROM_REVIEW",
            "COORD",
            "CURR_REMARK",
            "CURR_COMMENT",
            "NEW_SIG",
            "LT_PH",
            "LAST_UPDATED",
            "LAST_UPDATED_USERNAME",
            "RUSH",
            "DATE_CREATED",
            "NEED_DISTRICT",
            "NEED_TIMING",
            "NEED_OTHER_AGENCIES",
            "TO_OTHER_AGENCIES",
            "FROM_OTHER_AGENCIES",
            "NEED_BASE",
            "NEED_FIELD_CHECK",
            "NEED_PRE_DESIGN",
            "PROJ_TO_BASE",
            "PROJ_TO_BSL",
            "PROJ_TO_DISTRICT",
            "PROJ_TO_DWP",
            "PROJ_TO_OTHER_AGENCIES",
            "PROJ_TO_TIMING",
            "PROJ_TO_REVIEW",
            "PROJ_FROM_BASE",
            "PROJ_FROM_BSL",
            "PROJ_FROM_DISTRICT",
            "PROJ_FROM_DWP",
            "PROJ_FROM_OTHER_AGENCIES",
            "PROJ_FROM_TIMING",
            "PROJ_FROM_REVIEW",
            "PROJ_FIELD_CHECK",
            "PROJ_PRE_DESIGN",
            "PROJ_COORD",
            "PROJ_DOT_APPROVED",
            "PROJ_START_DATE",
            "PROJ_COMPLETED",
            "START_DATE",
            "COMPLETED",
            "TO_AS_BUILT",
            "FROM_AS_BUILT",
            "PROJ_TO_AS_BUILT",
            "PROJ_FROM_AS_BUILT",
            "BSL_SIGNED",
            "PROJ_BSL_SIGNED"
        };
    }
}
