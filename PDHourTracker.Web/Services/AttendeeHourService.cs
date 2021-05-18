using AutoMapper;
using PDHourTracker.Core.Dtos;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Extensions;
using PDHourTracker.Web.Models.AttendeeHours;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class AttendeeHourService : IAttendeeHourService
    {
        private IAttendeeHourRepo<AttendeeHour> _attendeeHourRepo;
        private IAttendeeHourProvider _attendeeHourProvider;
        private IMapper _mapper;

        public AttendeeHourService(IAttendeeHourRepo<AttendeeHour> attendeeHourRepo,
            IAttendeeHourProvider attendeeHourProvider,
            IMapper mapper)
        {
            _attendeeHourRepo = attendeeHourRepo;
            _attendeeHourProvider = attendeeHourProvider;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new attendee hour if Id is zero or updates it.
        /// </summary>
        /// <param name="attendeeHourEditModel"></param>
        public AttendeeHourViewModel AddOrUpdate(AttendeeHourEditModel attendeeHourEditModel)
        {
            var attendeeHourViewModel = new AttendeeHourViewModel();
            attendeeHourViewModel.Id = attendeeHourEditModel.Id;
            attendeeHourViewModel.AttendeeId = attendeeHourEditModel.AttendeeId;
            attendeeHourViewModel.WorkshopId = attendeeHourEditModel.WorkshopId;
            attendeeHourViewModel.Comments = attendeeHourEditModel.Comments;

            var attendeeHour = _mapper.Map<AttendeeHour>(attendeeHourEditModel);

            if (attendeeHour.Id == 0)
            {
                attendeeHour = _attendeeHourRepo.Add(attendeeHour);
                attendeeHourViewModel = _mapper.Map<AttendeeHourViewModel>(attendeeHour);
            }
            else
                _attendeeHourRepo.Update(attendeeHour);

            return attendeeHourViewModel;
        }

        /// <summary>
        /// Get AttendeeHour view model by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendeeHourViewModel Get(int id)
        {
            var attendeeHourViewModel = new AttendeeHourViewModel();

            var attendeeHour = _attendeeHourRepo.Get(id);

            if(attendeeHour != null)
                attendeeHourViewModel = _mapper.Map<AttendeeHourViewModel>(attendeeHour);

            return attendeeHourViewModel;
        }

        public AttendeeHourViewModel GetByAttendeeAndWorkshop(int attendeeId, int workshopId)
        {
            var attendeeHourViewModel = new AttendeeHourViewModel();

            var attendeeHour = _attendeeHourRepo.GetByAttendeeAndWorkshop(attendeeId, workshopId);

            if (attendeeHour != null)
                attendeeHourViewModel = _mapper.Map<AttendeeHourViewModel>(attendeeHour);

            return attendeeHourViewModel;
        }

        /// <summary>
        /// Get AttendeeHour edit model by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendeeHourEditModel GetForEdit(int id)
        {
            var attendeeHourEditModel = new AttendeeHourEditModel();

            var attendeeHour = _attendeeHourRepo.Get(id);

            if (attendeeHour != null)
                attendeeHourEditModel = _mapper.Map<AttendeeHourEditModel>(attendeeHour);

            return attendeeHourEditModel;
        }

        /// <summary>
        /// Returns true/false if attendee has already been assigned to workshop
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public bool Exists(int attendeeId, int workshopId)
        {
            return _attendeeHourRepo.Exists(attendeeId, workshopId);
        }

        public void Delete(int id)
        {
            _attendeeHourRepo.Delete(id);
        }

        /// <summary>
        /// Gets all attendee workshop hours listing workshops, dates and hours.
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        public List<AttendeeWorkshopHourModel> GetAttendeeWorkshopHours(int attendeeId)
        {
            var workshopHourModels = new List<AttendeeWorkshopHourModel>();

            var attendeeWorkshopHours = _attendeeHourProvider.GetAttendeeWorkshopHours(attendeeId);
            if (attendeeWorkshopHours.Any())
            {
                workshopHourModels = _mapper.MapList<AttendeeWorkshopHour,
                                    AttendeeWorkshopHourModel>(attendeeWorkshopHours);
            }

            return workshopHourModels;
        }
    }
}
