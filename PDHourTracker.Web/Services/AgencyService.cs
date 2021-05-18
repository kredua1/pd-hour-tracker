using AutoMapper;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Enums;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Extensions;
using PDHourTracker.Web.Models.Agencies;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class AgencyService : IAgencyService
    {
        private IAgencyRepo<Agency> _agencyRepo;
        private IMapper _mapper;

        public AgencyService(IAgencyRepo<Agency> agencyRepo,
            IMapper mapper)
        {
            _agencyRepo = agencyRepo;
            _mapper = mapper;
        }

        public void AddOrUpdate(AgencyViewModel agencyViewModel)
        {
            var agency = _mapper.Map<Agency>(agencyViewModel);

            if (agency.Id == 0)
                _agencyRepo.Add(agency);
            else
                _agencyRepo.Update(agency);
        }

        /// <summary>
        /// Returns true/false if given agency exists
        /// </summary>
        /// <param name="agencyName"></param>
        /// <returns></returns>
        public bool Exists(string agencyName)
        {
            return _agencyRepo.Exists(agencyName);
        }

        /// <summary>
        /// Gets agency view model by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AgencyViewModel Get(int id)
        {
            var agencyViewModel = new AgencyViewModel();

            var agency = _agencyRepo.Get(id);

            if(agency != null)
            {
                agencyViewModel = _mapper.Map<AgencyViewModel>(agency);
            }

            return agencyViewModel;
        }

        /// <summary>
        /// Gets list of agency view models.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AgencyViewModel> GetAgencies(int pageNum, int pageSize)
        {
            var agencyViewModels = new List<AgencyViewModel>();

            var agencies = _agencyRepo.GetEntities(a => a.AgencyName, Sorted.ASC, pageNum, pageSize);

            foreach(var agency in agencies)
            {
                agencyViewModels.Add(_mapper.Map<AgencyViewModel>(agency));
            }

            return agencyViewModels;
        }

        /// <summary>
        /// Searches for any agencies containing given search value.
        /// </summary>
        /// <param name="searchValue">Value to search for</param>
        /// <returns>Agencies containing given search string</returns>
        public List<AgencyViewModel> Search(string searchValue)
        {
            var agencyViewModels = new List<AgencyViewModel>();

            var agencies = _agencyRepo.Search(searchValue);

            agencyViewModels = _mapper.MapList<Agency, AgencyViewModel>(agencies);

            return agencyViewModels;
        }

        /// <summary>
        /// Gets the total number of agencies.
        /// </summary>
        /// <returns></returns>
        public int Total()
        {
            return _agencyRepo.Total();
        }
    }
}
