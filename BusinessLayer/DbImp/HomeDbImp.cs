using System;
using System.Data.Entity;
using System.Configuration;
using BusinessLayer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace BusinessLayer.DbImp
{
    public class HomeDbImp : DbContext 
    {
        protected String connectionString = ConfigurationManager.ConnectionStrings["SDTrackerContext"].ConnectionString;

        public Boolean AddNewUser(RegisterUser user)
        {
            Boolean bReturn = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUserPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "@UserName";
                pUserName.Value = user.UserName;
                cmd.Parameters.Add(pUserName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@Email";
                pEmail.Value = user.Email;
                cmd.Parameters.Add(pEmail);
                con.Open();
                cmd.ExecuteNonQuery();
                bReturn = true;
            }

            return bReturn;
        }

        public UserDetail GetUserDetail(String UserName)
        {
            UserDetail user = new UserDetail();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserDetailByUserName", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "@UserName";
                pUserName.Value = UserName;
                cmd.Parameters.Add(pUserName);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    user = SetGetRecord(rdr);
                }

            }
            return user;
        }

        public Boolean DoAuthen()
        {
            return true;
        }

        public IEnumerable<Role> GetRoles(String UserName)
        {
            List<Role> roles = new List<Role>();
            Role admin = new Role();
            admin.Id = 1;
            admin.RoleType = "Admin";
            roles.Add(admin);
            return roles;
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

        private bool userNameIsOk(string UserName)
        {
            
            string compareTo = "abcdefghijklmnopqrstuvwxyz0123456789";
            string test = UserName.Trim().ToLower();
            char[] charArray = test.ToCharArray();
            bool bReturn = true;

            foreach (char c in charArray)
            {
                if (!(compareTo.IndexOf(c) > 0))
                {
                    bReturn = false;
                }
            }

            return bReturn;
        }

        public String ValidateUserNameSelection(String UserName)
        {

            string testUser = UserName.ToLower().Trim();
            String msg = "The user name " + testUser + " is avaliable.";

            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            Match match = regex.Match(testUser);


            if (String.IsNullOrEmpty(testUser))
            {
                msg = "User name is required.";
            }

            if (testUser.Length<6)
            {
                msg = "User name must atleast 6 charaters";
            }
            
            if (testUser.Length > 10)
            {
                msg = "User can have no more than 10 charaters ";
            }

            if (!match.Success)
            {
                msg = "User name must only be letters or digits.";
            }


            if (UserExists(testUser))
            {
                msg = "The user " + testUser + " is in use.";
            }

            return msg;

        }




        public String ValidateEmailSelection(String Email)
        {
            string testUser = Email.ToLower().Trim();
            String msg = "The user name " + testUser + " is avaliable.";
            string emailRegex =  @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";


            Regex regex = new Regex(emailRegex);
            Match match = regex.Match(testUser);

            if (String.IsNullOrEmpty(testUser))
            {
                msg = "Email is required.";
            }

            if (!match.Success)
            {
                msg = "Not a valid Email Address.";
            }


            if (EmailExists(testUser))
            {
                msg = "The Email " + testUser + " is in use.";
            }

            return msg;

        }

        public Boolean EmailExists(String Email)
        {

            // @"^\(\d{3}\)\s\d{3}-\d{4}"

            Boolean bReturn = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserEngineerByEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@Email";
                pEmail.Value = Email;
                cmd.Parameters.Add(pEmail);

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

        public Boolean UserIsValid(UserLogin user)
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
                pPassword.Value = user.Password;
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

        private UserDetail SetGetRecord(SqlDataReader rdr)
        {
            UserDetail user = new UserDetail();

            user.Id = Convert.ToInt32(rdr["Id"]);
            user.UserName = rdr["UserName"].ToString();
            user.Password = rdr["Password"].ToString();


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

            try
            {

                if (!(rdr["Initials"] is DBNull))
                {
                    user.Initials = rdr["Initials"].ToString();
                }

            }

            catch { }

            if (!(rdr["Phone"] is DBNull))
            {
                user.Phone = rdr["Phone"].ToString();
            }

            user.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
            user.DateUpdated = Convert.ToDateTime(rdr["DateUpdated"]);
            user.DateAccessed = Convert.ToDateTime(rdr["DateAccessed"]);
            return user;

        }

        public Boolean IsServerSideValid(RegisterUser user)
        {
            Boolean bReturn = true;
            // User Name
            String testPhrase = "The user name " + user.UserName + " is avaliable.";
            String realPhrase = ValidateUserNameSelection(user.UserName);
            if(!testPhrase.Equals(realPhrase))
            {
                return false;
            }


            return bReturn;
        }

    }
}
