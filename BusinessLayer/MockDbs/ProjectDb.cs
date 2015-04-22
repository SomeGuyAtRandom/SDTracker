using System;
using BusinessLayer.DbImp;
using BusinessLayer.Models;
using System.Collections.Generic;
using BusinessLayer.DbInterfaces;

namespace BusinessLayer.MockDbs
{
    public class ProjectDb : IProjectDb
    {
        private ProjectDbImp context = new ProjectDbImp();

        public int CreateProject(string FiveDigit)
        {
            return context.CreateProject(FiveDigit);
        }

        public bool SaveField(int id, string FieldName, string Value)
        {
            return context.SaveField(id, FieldName, Value);
        }

        public void DeleteProject(int ProjectId)
        {
            context.DeleteProject(ProjectId);
        }

        public IEnumerable<Place> GetPlacesWithSearch(String searchTerm, int districtId)
        {
            return context.GetPlacesWithSearch(searchTerm, districtId);
        }

        public Project GetProject(int Id)
        {
            return context.GetProject(Id);
        }

        public IEnumerable<Project> GetProjectsWithSearch(String searchTerm, int districtId, int jobTypeId, String Field, DateTime startDate)
        {
            return context.GetProjectsWithSearch(searchTerm, districtId, jobTypeId, Field, startDate);
        }

        public IEnumerable<Place> Places() 
        {
            return context.Places();
        }

        public IEnumerable<Project> Projects() 
        {
            return context.Projects();
        }

        public IEnumerable<AdminUser> HeadEngineers()
        {
            return context.HeadEngineers();
        }

        public IEnumerable<AdminUser> DesignEngineers()
        {
            return context.DesignEngineers();
        }

        public IEnumerable<District> Districts()
        {
            return context.Districts();
        }

        public IEnumerable<JobType> JobTypes()
        {
            return context.JobTypes();
        }
        
        
    }
}
