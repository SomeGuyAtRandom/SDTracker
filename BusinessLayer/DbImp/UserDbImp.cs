using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BusinessLayer.Common;
using BusinessLayer.Models;

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

        // For the User/ChangePassword view
        private Boolean UserIsValid(UserPassword user)
        {
            Boolean bReturn = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUserIsValid", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "@UserName";
                pUserName.Value = user.UserName;
                cmd.Parameters.Add(pUserName);

                SqlParameter pPassword = new SqlParameter();
                pPassword.ParameterName = "@Password";
                pPassword.Value = Crypto.Encryptdata(user.OriginalPassword);
                cmd.Parameters.Add(pPassword);
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

        public bool ChangePassword(UserPassword user)
        {
            Boolean bReturn = false;

            //spUpdateUserPassword
            if (UserIsValid(user))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateUserPassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pUserName = new SqlParameter();
                    pUserName.ParameterName = "@UserName";
                    pUserName.Value = user.UserName;
                    cmd.Parameters.Add(pUserName);

                    SqlParameter pPassword = new SqlParameter();
                    pPassword.ParameterName = "@Password";
                    pPassword.Value = Crypto.Encryptdata(user.Password);
                    cmd.Parameters.Add(pPassword);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    bReturn = true;
                }
            }
            return bReturn;
        }

        public Engineer GetEngineerByUserName(String UserName)
        {
            Engineer user = new Engineer();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEngineerByUserName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@UserName";
                pId.Value = UserName;
                cmd.Parameters.Add(pId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    user = setEngineerToReader(rdr);
                }
                SetSubObjects(user);
            }
            return user;
        }

        private Engineer setEngineerToReader(SqlDataReader rdr)
        {
            Engineer user = new Engineer();

            user.Id = Convert.ToInt32(rdr["Id"]);
            user.FirstName = rdr["FirstName"].ToString();
            user.LastName = rdr["LastName"].ToString();
            user.Email = rdr["Email"].ToString();
            user.Initials = rdr["Initials"].ToString();
            user.UserName = rdr["UserName"].ToString();

            if (!(rdr["Phone"] is DBNull))
            {
                user.Phone = rdr["Phone"].ToString();
            }

            try
            {

                if (!(rdr["IsDisabled"] is DBNull))
                {
                    user.IsDisabled = Convert.ToBoolean(rdr["IsDisabled"]);
                }

            }
            catch { user.IsDisabled = false; }

            user.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
            user.DateUpdated = Convert.ToDateTime(rdr["DateUpdated"]);
            return user;

        }

        private void SetSubObjects(Engineer user)
        {

        }

    }
}
