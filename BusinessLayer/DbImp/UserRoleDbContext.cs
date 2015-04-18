using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLayer.DbImp
{
    public class UserRoleDbContext : BaseDbContext
    {
        public string[] GetUserRoles(String UserName)
        {
            string sUserRoles = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserRoles", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "@UserName";
                pUserName.Value = UserName;
                cmd.Parameters.Add(pUserName);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!(rdr[0] is DBNull))
                    {
                        sUserRoles += rdr[0].ToString() + ",";
                    }
                }
                if (sUserRoles.EndsWith(","))
                {
                    sUserRoles = sUserRoles.Remove(sUserRoles.LastIndexOf(','), 1);
                }

            }
            return sUserRoles.Split(',');
        }
        public Boolean IsUserInRole(String UserName, String Role)
        {

            Boolean bReturn = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUserIsInRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "@UserName";
                pUserName.Value = UserName;
                cmd.Parameters.Add(pUserName);

                SqlParameter pRole = new SqlParameter();
                pRole.ParameterName = "@Role";
                pRole.Value = Role;
                cmd.Parameters.Add(pRole);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    if (!(rdr[0] is DBNull))
                    {
                        bReturn = Convert.ToBoolean(rdr[0]);
                    }
                }
            }

            return bReturn;
            
        }
    }
}
