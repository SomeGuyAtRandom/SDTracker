using System;
using BusinessLayer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;



namespace BusinessLayer.DbImp
{
    public class EngineerDbImp : DbContext 
    {
        protected String connectionString = ConfigurationManager.ConnectionStrings["SDTrackerContext"].ConnectionString;

        public Engineer getEngineerUserId(int Id)
        {
            Engineer user = new Engineer();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAdminUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.Value = Id;
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

        public bool SaveField(int id, string fieldName, string txtValue)
        {
            bool bReturn = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUserField", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.Value = id;
                cmd.Parameters.Add(pId);

                SqlParameter pColumnName = new SqlParameter();
                pColumnName.ParameterName = "@columnName";
                pColumnName.Value = fieldName;
                cmd.Parameters.Add(pColumnName);

                SqlParameter pValue = new SqlParameter();
                pValue.ParameterName = "@StringIn";
                pValue.Value = txtValue;
                cmd.Parameters.Add(pValue);
                con.Open();

                cmd.ExecuteNonQuery();
                bReturn = true;
            }
            return bReturn;
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
