using AutoMapper;
using PDHourTracker.Core.Dtos;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Extensions;
using PDHourTracker.Web.Models.Agencies;
using PDHourTracker.Web.Models.AttendeeHours;
using PDHourTracker.Web.Models.Attendees;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class AttendeeService : IAttendeeService
    {
        private IAttendeeRepo<Attendee> _attendeeRepo;
        private IAttendeeReportProvider _attendeeReportProvider;
        private IAttendeeHourProvider _attendeeHourProvider;
        private IAgencyRepo<Agency> _agencyRepo;
        private IMapper _mapper;

        public AttendeeService(IAttendeeRepo<Attendee> attendeeRepo,
            IAttendeeReportProvider attendeeReportProvider,
            IAttendeeHourProvider attendeeHourProvider,
            IAgencyRepo<Agency> agencyRepo,
            IMapper mapper)
        {
            _attendeeRepo = attendeeRepo;
            _attendeeReportProvider = attendeeReportProvider;
            _attendeeHourProvider = attendeeHourProvider;
            _agencyRepo = agencyRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new attendee
        /// </summary>
        /// <param name="attendeeEditModel"></param>
        public void Add(AttendeeEditModel attendeeEditModel, string username)
        {
            var attendee = _mapper.Map<Attendee>(attendeeEditModel);

            // Set rec user and date
            attendee.RecUser = username;
            attendee.RecDate = DateTime.Now;

            // CertId: Get new random CertId if no id given
            if (string.IsNullOrEmpty(attendee.CertId))
                attendee.CertId = GetCertId();

            _attendeeRepo.Add(attendee);
        }

        /// <summary>
        /// Update attendee data
        /// </summary>
        /// <param name="attendeeEditModel"></param>
        public void Update(AttendeeEditModel attendeeEditModel, string username)
        {
            var attendee = _mapper.Map<Attendee>(attendeeEditModel);

            // Set mod user and date
            attendee.ModUser = username;
            attendee.ModDate = DateTime.Now;

            // Get existing record from db
            var dbAttendee = _attendeeRepo.Get(attendee.Id);
            if (dbAttendee != null)
            {
                // Copy Rec user and date
                attendee.RecUser = dbAttendee.RecUser;
                attendee.RecDate = dbAttendee.RecDate;

                // If no Cert ID given, copy existing from db
                if (string.IsNullOrEmpty(attendee.CertId))
                {
                    attendee.CertId = dbAttendee.CertId;
                }

                // Update db record
                _attendeeRepo.Update(attendee);
            }
            else
            {
                // Throw because we don't have an existing record
                throw new Exception("Could not update attendee. Attendee not found.");
            }
        }

        /// <summary>
        /// Gets a random string to be used as a CertId
        /// for an attendee when one is not given.
        /// Example: "P37201"
        /// </summary>
        /// <returns></returns>
        public string GetCertId()
        {
            var randomGenerator = new RandomGenerator();
            var certId = "P" + randomGenerator.GetNumberString(5);

            while (CertIdExists(certId))
            {
                certId = "P" + randomGenerator.GetNumberString(5);
            }

            return certId;
        }

        /// <summary>
        /// Returns true/false if attendee has given CertId.
        /// </summary>
        /// <param name="certId"></param>
        /// <returns></returns>
        public bool CertIdExists(string certId)
        {
            return _attendeeRepo.CertIdExists(certId);
        }

        /// <summary>
        /// Returns true/false if attendee exists based on name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public bool Exists(string firstName, string lastName, string middleName)
        {
            return _attendeeRepo.Exists(firstName, lastName, middleName);
        }

        /// <summary>
        /// Gets attendee by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendeeViewModel Get(int id)
        {
            var attendeeViewModel = new AttendeeViewModel();

            var attendee = _attendeeRepo.Get(id);
            
            if(attendee != null)
            {
                attendeeViewModel = _mapper.Map<AttendeeViewModel>(attendee);
            }

            return attendeeViewModel;
        }

        /// <summary>
        /// Gets attendees paged.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AttendeeViewModel> GetAttendees(int pageNum, int pageSize)
        {
            var attendeeViewModels = new List<AttendeeViewModel>();

            var attendees = _attendeeRepo.GetAttendees(pageNum, pageSize);

            foreach(var attendee in attendees)
            {
                attendeeViewModels.Add(_mapper.Map<AttendeeViewModel>(attendee));
            }

            return attendeeViewModels;
        }

        /// <summary>
        /// Get attendees with agency name included.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AttendeeWithAgencyModel> GetAttendeeWithAgencies(int pageNum, int pageSize, string searchValue = null)
        {
            var attendeeWithAgencyModels = new List<AttendeeWithAgencyModel>();

            // If search value has a space, take string up to space
            // Trim leading/trailing spaces first
            searchValue = searchValue.Trim();
            if (searchValue.Contains(" "))
                searchValue = searchValue.Substring(0, searchValue.IndexOf(" "));

            var attendeeWithAgencies = _attendeeReportProvider.AttendeeWithAgencies(
                pageNum, pageSize, searchValue);

            //foreach(var attendeeWithAgency in attendeeWithAgencies)
            //{
            //    attendeeWithAgencyModels.Add(_mapper.Map<AttendeeWithAgencyModel>(attendeeWithAgency));
            //}
            attendeeWithAgencyModels = _mapper.MapList<AttendeeWithAgency,
                AttendeeWithAgencyModel>(attendeeWithAgencies);

            return attendeeWithAgencyModels;
        }

        /// <summary>
        /// Searches attendees on both first and last names for any that
        /// contain given search value
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<AttendeeViewModel> Search(string searchValue)
        {
            return _mapper.MapList<Attendee, AttendeeViewModel>(
                _attendeeRepo.Search(searchValue));
        }

        /// <summary>
        /// Returns the total number of attendees.
        /// </summary>
        /// <returns></returns>
        public int Total()
        {
            return _attendeeRepo.Total();
        }

        /// <summary>
        /// Gets the attendee and a list of all agencies.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendeeEditModel GetForEdit(int id)
        {
            var attendeeEditModel = new AttendeeEditModel();

            var attendee = _attendeeRepo.Get(id);
            if(attendee != null)
            {
                attendeeEditModel = _mapper.Map<AttendeeEditModel>(attendee);

                // Get the agencies
                var agencies = _agencyRepo.GetEntities(1, 100); // update
                attendeeEditModel.Agencies = _mapper.MapList<Agency, AgencyViewModel>(agencies);
            }

            return attendeeEditModel;
        }

        public AttendeeDetailsModel GetAttendeeWithWorkshopHours(int id)
        {
            var attendeeDetailsModel = new AttendeeDetailsModel();

            var attendee = _attendeeRepo.Get(id);

            if(attendee != null)
            {
                attendeeDetailsModel = _mapper.Map<AttendeeDetailsModel>(attendee);

                // Get agency name
                var agency = _agencyRepo.Get(attendee.AgencyId);
                if (agency != null)
                    attendeeDetailsModel.AgencyName = agency.AgencyName;

                // Get attendee's workshop hours
                var attendeeWorkshopHours = _attendeeHourProvider.GetAttendeeWorkshopHours(id);
                if (attendeeWorkshopHours.Any())
                {
                    attendeeDetailsModel.WorkshopHours = _mapper.MapList<AttendeeWorkshopHour,
                        AttendeeWorkshopHourModel>(attendeeWorkshopHours);
                }
            }

            return attendeeDetailsModel;
        }

        /// <summary>
        /// Gets all attendees for given agency
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public List<AttendeeViewModel> ByAgency(int agencyId)
        {
            var attendeeViewModels = new List<AttendeeViewModel>();

            var attendees = _attendeeRepo.GetByAgency(agencyId);

            if (attendees.Any())
            {
                attendeeViewModels = _mapper.MapList<Attendee, AttendeeViewModel>(attendees);
            }

            return attendeeViewModels;
        }
    }
}
