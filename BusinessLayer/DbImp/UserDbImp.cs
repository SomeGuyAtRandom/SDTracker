using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BusinessLayer.DbImp
{
    public class UserDbImp : DbContext 
    {
        protected String connectionString = ConfigurationManager.ConnectionStrings["SDTrackerContext"].ConnectionString;

        public Boolean InitialsExixts(string initials)
        {
            Boolean bReturn = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserPasswordByInitials", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pInitials = new SqlParameter();
                pInitials.ParameterName = "@Initials";
                pInitials.Value = initials;
                cmd.Parameters.Add(pInitials);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!(rdr["Id"] is DBNull))
                    {
                        bReturn = (Convert.ToInt32(rdr["Id"]) > 0);
                    }
                }
            }
            return bReturn;
        }

        public Boolean UserExists(String UserName)
        {
            Boolean bReturn = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserPasswordByUserName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "@UserName";
                pUserName.Value = UserName;
                cmd.Parameters.Add(pUserName);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!(rdr["Id"] is DBNull))
                    {
                        bReturn = (Convert.ToInt32(rdr["Id"]) > 0);
                    }
                }
            }
            return bReturn;
        }


        public bool SaveField(int id, string FieldName, string Value)
        {
            bool bReturn = false;
            
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@Id";
            pId.Value = id;

            SqlParameter pColumnName = new SqlParameter();
            pColumnName.ParameterName = "@columnName";
            pColumnName.Value = FieldName;

            SqlParameter pValueIn = new SqlParameter();
            pValueIn.ParameterName = "@StringIn";
            pValueIn.Value = Value;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUserField", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(pId);
                cmd.Parameters.Add(pColumnName);
                cmd.Parameters.Add(pValueIn);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                bReturn = true;
            }
            return bReturn;
        }
    }
}
