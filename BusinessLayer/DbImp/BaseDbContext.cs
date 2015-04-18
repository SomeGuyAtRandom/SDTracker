using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using BusinessLayer.Models;
using System.Data;
using System.Data.Entity;

namespace BusinessLayer.DbImp
{
    public class BaseDbContext : DbContext
    {
        
        protected String connectionString = ConfigurationManager.ConnectionStrings["SDTrackerContext"].ConnectionString;
        //protected String connectionString =@"server=.; data source=.\SQLEXPRESS; database=SignalDB; integrated security=SSPI";


        public IEnumerable<AdminUser> Engineers
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

        public IEnumerable<Project> Projects
        {
            get
            {
                List<Project> projects = new List<Project>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllProjects", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Project project = setGetProject(rdr);
                        projects.Add(project);
                    }
                }
                foreach (Project project in projects)
                {
                    SetSubObjectsForProject(project);
                }
                return projects;
            }
        }

        public IEnumerable<Place> Places
        {
            get
            {
                List<Place> places = new List<Place>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllPlaces", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Place place = setGetPlace(rdr);
                        places.Add(place);
                    }
                }

                return places;
            }
        }

        public IEnumerable<District> Districts
        {
            get
            {
                List<District> district = new List<District>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllDistricts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        district.Add(setGetDistrict(rdr));
                    }
                }
                return district;
            }
        }

        public IEnumerable<JobType> JobTypes
        {
            get
            {
                List<JobType> jobTypes = new List<JobType>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllJobTypes", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        jobTypes.Add(setGetJobType(rdr));
                    }
                }
                return jobTypes;
            }
        }

        public IEnumerable<Role> Roles
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

        //--------------------------------------------------------------------------

        public void SetSubObjectsForProject(Project project)
        {

            project.JobType = getJobTypeById(project.JobTypeId);
            project.HeadEngineer = getEngineerById(project.HeadEngineerId);
            project.DesignEngineer = getEngineerById(project.DesignEngineerId);
            project.District = getDistrictById(project.DistrictId);
            project.Requirements = getRequirements(project.Id); 
        }

        private IEnumerable<Requirement> getRequirements(int projectId)
        {
            List<Requirement> requirements = new List<Requirement>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetRequirementsByProjectId", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@ProjectId";
                pId.Value = projectId;
                cmd.Parameters.Add(pId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    requirements.Add(setGetRequirement(rdr));
                }

            }

            return requirements;
        }

 

        //--------------------------------------------------------------------------

        public Place setGetPlace(SqlDataReader rdr)
        {
            Place place = new Place();
            place.Id = Convert.ToInt32(rdr["Id"]);
            place.FiveDigit = rdr["FiveDigit"].ToString();
            place.DistrictId = Convert.ToInt32(rdr["DistrictId"]);
            place.PlaceLocation = rdr["PlaceLocation"].ToString();
            return place;
        }

        public Project setGetProject(SqlDataReader rdr)
        {
            Project project = new Project();

            project.Id = Convert.ToInt32(rdr["Id"]);


            if (!(rdr["Location"] is DBNull))
            {
                project.Location = rdr["Location"].ToString();
            }
            else { project.Location = ""; }


            if (!(rdr["DistrictId"] is DBNull))
            {
                project.DistrictId = Convert.ToInt32(rdr["DistrictId"]);
            }

            else { project.DistrictId = 0; }

            if (!(rdr["CurrRemark"] is DBNull))
            {
                project.CurrRemark = rdr["CurrRemark"].ToString();
            }
            else { project.CurrRemark = ""; }


            if (!(rdr["Rush"] is DBNull))
            {
                project.Rush = Convert.ToBoolean(rdr["Rush"]);
            }
            else { project.Rush = false; }


            if (!(rdr["ProjNo"] is DBNull))
            {
                project.ProjNo = rdr["ProjNo"].ToString();
            }
            else { project.ProjNo = ""; }


            if (!(rdr["FiveDigit"] is DBNull))
            {
                project.FiveDigit = rdr["FiveDigit"].ToString();
            }
            else { project.FiveDigit = ""; }


            if (!(rdr["CDs"] is DBNull))
            {
                project.CDs = rdr["CDs"].ToString();
            }
            else { project.CDs = ""; }


            if (!(rdr["JobTypeId"] is DBNull))
            {
                project.JobTypeId = Convert.ToInt32(rdr["JobTypeId"]);
            }
            else { project.JobTypeId = 0; }


            if (!(rdr["HeadEngineerId"] is DBNull))
            {
                project.HeadEngineerId = Convert.ToInt32(rdr["HeadEngineerId"]);
            }
            else { project.HeadEngineerId = 0; }


            if (!(rdr["DesignEngineerId"] is DBNull))
            {
                project.DesignEngineerId = Convert.ToInt32(rdr["DesignEngineerId"]);
            }
            else { project.DesignEngineerId = 0; }


            if (!(rdr["CurrentComment"] is DBNull))
            {
                project.CurrentComment = rdr["CurrentComment"].ToString();
            }
            else { project.CurrentComment = ""; }



            if (!(rdr["DateAssigned"] is DBNull))
            {
                project.DateAssigned = Convert.ToDateTime(rdr["DateAssigned"]);
            }
            else { project.DateAssigned = DateTime.Now; }


            if (!(rdr["StartDate"] is DBNull))
            {
                project.StartDate = Convert.ToDateTime(rdr["StartDate"]);
            }
            else { project.StartDate = DateTime.Now; }


            if (!(rdr["DateCreated"] is DBNull))
            {
                project.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
            }
            else { project.StartDate = DateTime.Now; }
            return project;

        }

        public JobType setGetJobType(SqlDataReader rdr)
        {
            JobType jobType = new JobType();
            jobType.Id = Convert.ToInt32(rdr["Id"]);
            jobType.Name = rdr["Name"].ToString();
            jobType.JobCode = rdr["JobCode"].ToString();
            return jobType;

        }

        public Role setGetRole(SqlDataReader rdr)
        {
            Role role = new Role();
            role.Id = Convert.ToInt32(rdr["Id"]);
            role.RoleType = rdr["RoleType"].ToString();
            return role;

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

            user.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
            user.DateUpdated = Convert.ToDateTime(rdr["DateUpdated"]);

            return user;

        }


        public void SetGetUsersToAdmin(AdminUser user)
        {
            //user.AllRoles = Roles 

            //project.JobType = getJobTypeById(project.JobTypeId);
           
        }


        public District setGetDistrict(SqlDataReader rdr)
        {
            District district = new District();
            district.Id = Convert.ToInt32(rdr["Id"]);

            if (!(rdr["Code"] is DBNull))
            {
                district.Code = rdr["Code"].ToString();
            }
            else { district.Code = ""; }

            if (!(rdr["Name"] is DBNull))
            {
                district.Name = rdr["Name"].ToString();
            }
            else { district.Name = ""; }


            return district;

        }

        public Requirement setGetRequirement(SqlDataReader rdr)
        {
            Requirement requirement = new Requirement();
            requirement.Id = Convert.ToInt32(rdr["Id"]);
            requirement.RequirementId = Convert.ToInt32(rdr["RequirementId"]);
            requirement.ProjectId = Convert.ToInt32(rdr["ProjectId"]);
            requirement.RequirementName = rdr["RequirementName"].ToString();

            if (!(rdr["Required"] is DBNull))
            {
                requirement.Required = Convert.ToBoolean(rdr["Required"]);
            }
            else { requirement.Required = false; }


            if (!(rdr["StartDate"] is DBNull))
            {
                requirement.StartDate = Convert.ToDateTime(rdr["StartDate"]);
            }
            else { requirement.StartDate = null; }

            if (!(rdr["FinishDate"] is DBNull))
            {
                requirement.FinishDate = Convert.ToDateTime(rdr["FinishDate"]);
            }
            else { requirement.FinishDate = null; }



            return requirement;

        }

    
        //--------------------------------------------------------------------------

        


        public AdminUser getEngineerById(int Id)
        {
            AdminUser user = new AdminUser();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEngineerById", con);
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
            }

            return user;
        }

        public District getDistrictById(int Id)
        {
            District district = new District();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetDistrict", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.Value = Id;
                cmd.Parameters.Add(pId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    district = setGetDistrict(rdr);
                }
            }

            return district;
        }

        public JobType getJobTypeById(int Id)
        {
            JobType jobType = new JobType();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetJobType", con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.Value = Id;
                cmd.Parameters.Add(pId);



                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    jobType = setGetJobType(rdr);
                }
            }

            return jobType;
        }

        //--------------------------------------------------------------------------


    }
}
