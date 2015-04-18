using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using BusinessLayer.Models;
using System.Data;
using System.Data.Entity;
using System.Configuration;

namespace BusinessLayer.DbImp
{
    public class AdminDbImp : DbContext 
    {
        protected String connectionString = ConfigurationManager.ConnectionStrings["SDTrackerContext"].ConnectionString;

        public AdminUser getAdminUserById(int Id)
        {
            AdminUser user = new AdminUser();
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
                    user = setGetUsersToAdmin(rdr);
                }
                SetSubObjectsForAdminUser(user);
            }
            return user;
        }

        public IEnumerable<AdminUser> GetUserWithSearch(String searchTerm)
        {
            string[] words = searchTerm.Split(' ');
            List<AdminUser> users = new List<AdminUser>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUsersWithSearch", con);

                cmd.CommandType = CommandType.StoredProcedure;
                int i = 0;
                while (i < words.Length && i <= 10) /* Look at stored procedure to understand this limitation */
                {
                    SqlParameter word = new SqlParameter();
                    word.ParameterName = "@word" + i;
                    word.Value = words[i];
                    cmd.Parameters.Add(word);
                    i++;
                }

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    users.Add(setGetUsersToAdmin(rdr));
                }


            }


            return users;
        }

        public Boolean EnableDisableUser(int id, bool isDisabled)
        {
            bool bReturn = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEnableDisableUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.Value = id;
                cmd.Parameters.Add(pId);

                SqlParameter pIsDisabled = new SqlParameter();
                pIsDisabled.ParameterName = "@isDisabled";
                pIsDisabled.Value = isDisabled;
                cmd.Parameters.Add(pIsDisabled);

                con.Open();

                cmd.ExecuteNonQuery();
                bReturn = true;
            }

            return bReturn;

        }

        public Boolean AddRemoveRole(int id, string roleName, bool isMember)
        {
            bool bReturn = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUserRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.Value = id;
                cmd.Parameters.Add(pId);

                SqlParameter pRoleName = new SqlParameter();
                pRoleName.ParameterName = "@roleName";
                pRoleName.Value = roleName;
                cmd.Parameters.Add(pRoleName);

                SqlParameter pIsMember = new SqlParameter();
                pIsMember.ParameterName = "@isMember";
                pIsMember.Value = isMember;
                cmd.Parameters.Add(pIsMember);


                con.Open();

                cmd.ExecuteNonQuery();
                bReturn = true;
            }

            return bReturn;

        }

        public Boolean SaveField(int id, string FieldName, string Value)
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
                pColumnName.Value = FieldName;
                cmd.Parameters.Add(pColumnName);

                SqlParameter pValue = new SqlParameter();
                pValue.ParameterName = "@StringIn";
                pValue.Value = Value;
                cmd.Parameters.Add(pValue);
                con.Open();

                cmd.ExecuteNonQuery();
                bReturn = true;
            }
            return bReturn;
        }




        public IEnumerable<AdminUser> AdminUsers
        {
            get
            {
                List<AdminUser> engineers = new List<AdminUser>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllEngineers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        engineers.Add(setGetUsersToAdmin(rdr));
                    }
                }
                return engineers;
            }
        }

        public IEnumerable<Role> AllRoles
        {
            get
            {
                List<Role> roles = new List<Role>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllRoles", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        roles.Add(setGetRole(rdr));
                    }
                }
                return roles;
            }
        }

        public AdminUser setGetUsersToAdmin(SqlDataReader rdr)
        {
            AdminUser user = new AdminUser();

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
            catch { user.IsDisabled = false;  }

            user.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
            user.DateUpdated = Convert.ToDateTime(rdr["DateUpdated"]);
            return user;

        }

        public AdminUser setGetUserPassword(SqlDataReader rdr)
        {
            AdminUser user = new AdminUser();

            user.Id = Convert.ToInt32(rdr["Id"]);

            if (!(rdr["FirstName"] is DBNull))
            {
                user.FirstName = rdr["FirstName"].ToString();
            }

            if (!(rdr["LastName"] is DBNull))
            {
                user.LastName = rdr["LastName"].ToString();
            }

            if (!(rdr["Email"] is DBNull))
            {
                user.Email = rdr["Email"].ToString();
            }

            if (!(rdr["Initials"] is DBNull))
            {
                user.Initials = rdr["Initials"].ToString();
            }

            if (!(rdr["UserName"] is DBNull))
            {
                user.Initials = rdr["UserName"].ToString();
            }

            if (!(rdr["Phone"] is DBNull))
            {
                user.Phone = rdr["Phone"].ToString();
            }

            if (!(rdr["IsDisabled"] is DBNull))
            {
                user.IsDisabled = Convert.ToBoolean(rdr["IsDisabled"]);
            }
            
            user.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
            user.DateUpdated = Convert.ToDateTime(rdr["DateUpdated"]);
            return user;

        }

        public void SetSubObjectsForAdminUser(AdminUser user)
        {
            user.AllRoles = AllRoles;
            user.Roles = RolesForUser(user);
        }


 
        

        public Role setGetRole(SqlDataReader rdr)
        {
            Role role = new Role();
            role.Id = Convert.ToInt32(rdr["Id"]);
            role.RoleType = rdr["RoleType"].ToString();


            if (!(rdr["IsMember"] is DBNull))
            {
                role.IsMember = Convert.ToBoolean(rdr["IsMember"]);
            }
            else { role.IsMember = false; }

            return role;

        }

        public IEnumerable<Role> RolesForUser(AdminUser user)
        {
            List<Role> role = new List<Role>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserRoles", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter pUser = new SqlParameter();
                pUser.ParameterName = "@UserName";
                pUser.Value = user.UserName;
                cmd.Parameters.Add(pUser);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    role.Add(setGetRole(rdr));
                }

            }
            return role;
        }
        
    }
}
