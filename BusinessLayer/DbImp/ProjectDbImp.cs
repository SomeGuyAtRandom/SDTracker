﻿using System;
using BusinessLayer.Models;
using System.Data.Entity;
using System.Configuration;

using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;


namespace BusinessLayer.DbImp
{
    public class ProjectDbImp : DbContext 
    {
        protected String connectionString = ConfigurationManager.ConnectionStrings["SDTrackerContext"].ConnectionString;

        public int CreateProject(int ProjectId)
        {
            int iReturn = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddProject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pPlaceId = new SqlParameter();
                pPlaceId.ParameterName = "@PlaceId";
                pPlaceId.Value = ProjectId;
                cmd.Parameters.Add(pPlaceId);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!(rdr["ProjectId"] is DBNull))
                    {
                        iReturn = Convert.ToInt32(rdr["ProjectId"]);
                    }
                }
            }
            return iReturn;
        }

        public void DeleteProject(int ProjectId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pPlaceId = new SqlParameter();
                pPlaceId.ParameterName = "@Id";
                pPlaceId.Value = ProjectId;
                cmd.Parameters.Add(pPlaceId);
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public bool SaveField(int id, string FieldName, string Value)
        {
            bool bReturn = false;
            string[] reqestItem = FieldName.Split('.');

            int spId = 0;
            string sFieldName = "";
            string sp = "";

            DateTime dVal = new DateTime();
            int iVal = 0;
            string sVal = "";

            if (reqestItem.Length > 1)
            {
                try { spId = Int32.Parse(reqestItem[1]); }
                catch { throw new Exception("Requseting ID is not an integer."); }
                sFieldName = reqestItem[0];

                sp = "spUpdateRequirementsFieldById";
                switch (sFieldName)
                {
                    case "StartDate":
                    case "FinishDate":
                        {
                            DateTime d = new DateTime();
                            try
                            {
                                d = DateTime.Parse(Value);
                                dVal = d;
                            }
                            catch { throw new Exception("Value to update to is not a DateTime value."); }
                            sp += "DateTime";
                            break;
                        }
                    case "CurrentComment":
                        {
                            sp += "String";
                            break;
                        }
                }


            }
            else
            {
                sp = "spUpdateProjectsFieldById";
                switch (FieldName)
                {
                    case "Location":
                    case "CurrRemark":
                    case "ProjNo":
                    case "FiveDigit":
                    case "CurrentComment":
                        {
                            sVal = Value;

                            sp += "String";
                            break;
                        }
                    case "Districts":
                    case "JobTypes":
                    case "HeadEngineers":
                    case "DesignEngineers":
                        {
                            try
                            {
                                iVal = Int32.Parse(Value);
                            }
                            catch
                            {

                                iVal = 0;

                            }
                            sp += "Int";
                            break;
                        }
                    case "DateAssigned":
                    case "StartDate":
                        {
                            try
                            {
                                dVal = DateTime.Parse(Value);
                            }
                            catch
                            {
                                throw new Exception("Value to update to is not a DateTime value.");
                            }
                            sp += "DateTime";
                            break;
                        }
                }
            }


            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@Id";
            pId.Value = spId;

            SqlParameter pColumnName = new SqlParameter();
            pColumnName.ParameterName = "@columnName";
            pColumnName.Value = sFieldName;

            SqlParameter pValueIn = new SqlParameter();
            pValueIn.ParameterName = "@ValueIn";

            if (sp.EndsWith("DateTime"))
            {
                pValueIn.Value = dVal;
            }
            if (sp.EndsWith("String"))
            {
                pValueIn.Value = sVal;

            }
            if (sp.EndsWith("Int"))
            {
                pValueIn.Value = iVal;
            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sp, con);
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

        public IEnumerable<Place> GetPlacesWithSearch(String searchTerm, int districtId)
        {
            string[] words = searchTerm.Split(' ');
            List<Place> places = new List<Place>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetPlacesWithSearch", con);

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

                SqlParameter pDistrictId = new SqlParameter();
                pDistrictId.ParameterName = "@districtId";
                pDistrictId.Value = districtId;
                cmd.Parameters.Add(pDistrictId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    places.Add(setGetPlace(rdr));
                }

            }

            return places;
        }

        public IEnumerable<Project> Projects()
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

        public IEnumerable<Place> Places()
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

        public IEnumerable<AdminUser> HeadEngineers()
        {
            List<AdminUser> engineers = new List<AdminUser>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllHeadEngineers", con);
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

        public IEnumerable<AdminUser> DesignEngineers()
        {
            List<AdminUser> engineers = new List<AdminUser>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllDesignEngineers", con);
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

        public IEnumerable<District> Districts()
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


        public IEnumerable<JobType> JobTypes()
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


        public IEnumerable<Project> GetProjectsWithSearch(String searchTerm, int districtId, int jobTypeId, String Field, DateTime startDate)
        {
            string[] words = searchTerm.Split(' ');
            List<Project> projects = new List<Project>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetProjectsWithSearch", con);

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
                SqlParameter pDistrictId = new SqlParameter();
                pDistrictId.ParameterName = "@districtId";
                pDistrictId.Value = districtId;
                cmd.Parameters.Add(pDistrictId);
                //


                SqlParameter pJobTypeId = new SqlParameter();
                pJobTypeId.ParameterName = "@jobTypeId";
                pJobTypeId.Value = jobTypeId;
                cmd.Parameters.Add(pJobTypeId);


                SqlParameter pField = new SqlParameter();
                pField.ParameterName = "@FieldSelected";
                pField.Value = Field;
                cmd.Parameters.Add(pField);

                SqlParameter pStartDate = new SqlParameter();
                pStartDate.ParameterName = "@startDate";
                pStartDate.Value = startDate;
                cmd.Parameters.Add(pStartDate);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    projects.Add(setGetProject(rdr));
                }

            }
            foreach (Project project in projects)
            {
                SetSubObjectsForProject(project);
            }
            return projects;
        }

        public Project GetProject(int Id)
        {
            Project project = new Project();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetProject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.Value = Id;
                cmd.Parameters.Add(pId);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    project = setGetProject(rdr);
                }
            }
            SetSubObjectsForProject(project);

            return project;
        }

        private void SetSubObjectsForProject(Project project)
        {

            project.JobType = getJobTypeById(project.JobTypeId);
            project.HeadEngineer = getEngineerById(project.HeadEngineerId);
            project.DesignEngineer = getEngineerById(project.DesignEngineerId);
            project.District = getDistrictById(project.DistrictId);
            project.Requirements = getRequirements(project.Id);
        }

        private AdminUser getEngineerById(int Id)
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

        private District getDistrictById(int Id)
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

        private JobType getJobTypeById(int Id)
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

        private District setGetDistrict(SqlDataReader rdr)
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

        private JobType setGetJobType(SqlDataReader rdr)
        {
            JobType jobType = new JobType();
            jobType.Id = Convert.ToInt32(rdr["Id"]);
            jobType.Name = rdr["Name"].ToString();
            jobType.JobCode = rdr["JobCode"].ToString();
            return jobType;

        }

        private Project setGetProject(SqlDataReader rdr)
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

        private Requirement setGetRequirement(SqlDataReader rdr)
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

        private Place setGetPlace(SqlDataReader rdr)
        {
            Place place = new Place();
            place.Id = Convert.ToInt32(rdr["Id"]);
            place.FiveDigit = rdr["FiveDigit"].ToString();
            place.DistrictId = Convert.ToInt32(rdr["DistrictId"]);
            place.PlaceLocation = rdr["PlaceLocation"].ToString();
            return place;
        }

        private AdminUser setGetUsersToAdmin(SqlDataReader rdr)
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

    }
}
