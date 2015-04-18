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
        
    }
}
