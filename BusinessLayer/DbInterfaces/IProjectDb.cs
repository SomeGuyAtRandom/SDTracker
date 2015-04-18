using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.DbInterfaces
{
    public interface IProjectDb
    {
        int CreateProject(int ProjectId);

        bool SaveField(int id, string FieldName, string Value);

        void DeleteProject(int ProjectId);

        IEnumerable<Place> GetPlacesWithSearch(String searchTerm, int districtId);

        Project GetProject(int Id);

        IEnumerable<Project> GetProjectsWithSearch(String searchTerm, int districtId, int jobTypeId, String Field, DateTime startDate);

        IEnumerable<Place> Places();

        IEnumerable<Project> Projects();

        IEnumerable<AdminUser> HeadEngineers();

        IEnumerable<AdminUser> DesignEngineers();

        IEnumerable<District> Districts();

        IEnumerable<JobType> JobTypes();
    }
}
