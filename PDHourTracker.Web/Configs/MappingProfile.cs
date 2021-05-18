using AutoMapper;
using PDHourTracker.Core.Dtos;
using PDHourTracker.Core.Entities;
using PDHourTracker.Infrastructure.Identity;
using PDHourTracker.Web.Models.Agencies;
using PDHourTracker.Web.Models.AttendeeHours;
using PDHourTracker.Web.Models.Attendees;
using PDHourTracker.Web.Models.Employees;
using PDHourTracker.Web.Models.Projects;
using PDHourTracker.Web.Models.ProviderCodes;
using PDHourTracker.Web.Models.SignOutSheets;
using PDHourTracker.Web.Models.Users;
using PDHourTracker.Web.Models.Workshops;
using System.Collections.Generic;

namespace PDHourTracker.Web.Configs
{
    /// <summary>
    /// This class contains the method that maps entities to models using AutoMapper.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigEntityToModelMappings();
        }

        /// <summary>
        /// Creates all the mappings to/from entity to model.
        /// </summary>
        private void ConfigEntityToModelMappings()
        {
            CreateMap<Agency, AgencyViewModel>().ReverseMap();
            CreateMap<Attendee, AttendeeDetailsModel>().ReverseMap();
            CreateMap<Attendee, AttendeeEditModel>().ReverseMap();
            CreateMap<Attendee, AttendeeViewModel>().ReverseMap();
            CreateMap<AttendeeHour, AttendeeHourViewModel>().ReverseMap();
            CreateMap<AttendeeHour, AttendeeHourEditModel>().ReverseMap();
            CreateMap<AttendeeWorkshopHour, AttendeeWorkshopHourModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<ProviderCode, ProviderCodeViewModel>().ReverseMap();
            CreateMap<SignOutSheetUpload, SignOutSheetUploadModel>().ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
            CreateMap<Workshop, WorkshopViewModel>().ReverseMap();
            CreateMap<Workshop, WorkshopEditModel>().ReverseMap();
            
            // Custom objects/dtos
            CreateMap<WorkshopAttendeeHour, WorkshopAttendeeHourModel>().ReverseMap();
            CreateMap<AttendeeWithAgency, AttendeeWithAgencyModel>();
        }
    }
}