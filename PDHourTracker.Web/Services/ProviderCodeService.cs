using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Models.ProviderCodes;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class ProviderCodeService : IProviderCodeService
    {
        IProviderCodeRepo<ProviderCode> _providerCodeRepo;
        IMapper _mapper;

        public ProviderCodeService(IProviderCodeRepo<ProviderCode> providerCodeRepo,
            IMapper mapper)
        {
            _providerCodeRepo = providerCodeRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new ProviderCode if Id is zero; otherwise updates ProviderCode
        /// </summary>
        /// <param name="providerCodeViewModel"></param>
        public void AddOrUpdate(ProviderCodeViewModel providerCodeViewModel)
        {
            var providerCode = _mapper.Map<ProviderCode>(providerCodeViewModel);

            if (providerCode.Id == 0)
                _providerCodeRepo.Add(providerCode);
            else
                _providerCodeRepo.Update(providerCode);
        }

        /// <summary>
        /// Return true/false if given code already exists.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Exists(string code)
        {
            return _providerCodeRepo.Exists(code);
        }

        /// <summary>
        /// Gets Provider Code by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProviderCodeViewModel Get(int id)
        {
            var providerCodeViewModel = new ProviderCodeViewModel();

            var providerCode = _providerCodeRepo.Get(id);

            if (providerCode != null)
                providerCodeViewModel = _mapper.Map<ProviderCodeViewModel>(providerCode);

            return providerCodeViewModel;
        }

        /// <summary>
        /// Gets the current Provider Code based on current date
        /// </summary>
        /// <returns></returns>
        public ProviderCodeViewModel GetCurrent()
        {
            var providerCodeViewModel = new ProviderCodeViewModel();

            var providerCode = _providerCodeRepo.GetCurrent();

            if (providerCode != null)
                providerCodeViewModel = _mapper.Map<ProviderCodeViewModel>(providerCode);

            return providerCodeViewModel;
        }

        /// <summary>
        /// Gets all Provider Codes
        /// </summary>
        /// <returns></returns>
        public List<ProviderCodeViewModel> GetProviderCodes(int pageNum, int pageSize)
        {
            var providerCodeViewModels = new List<ProviderCodeViewModel>();

            var providerCodes = _providerCodeRepo.GetEntities(pageNum, pageSize);

            foreach(var providerCode in providerCodes)
            {
                providerCodeViewModels.Add(_mapper.Map<ProviderCodeViewModel>(providerCode));
            }

            return providerCodeViewModels;
        }

        /// <summary>
        /// Creates a Select List of provider codes for dropdown.
        /// Current code based on current date is selected.
        /// </summary>
        /// <returns></returns>
        public SelectList ProviderCodesForView()
        {
            // Get all provider codes (not many expected)
            var providerCodes = _providerCodeRepo.GetEntities(1, 50);

            //var providerCodeItems = new List<ProviderCodeSelectItem>();

            // Show dates with code by appending dates to Code property
            foreach (var providerCode in providerCodes)
            {
                providerCode.Code += $" ({providerCode.StartDate.ToShortDateString()} - "
                    + $"{providerCode.EndDate.ToShortDateString()})";

                //providerCodeItems.Add(new ProviderCodeSelectItem
                //{
                //    CodeValue = providerCode.Code,
                //    CodeText = $"{providerCode.Code} "
                //        + $"({providerCode.StartDate.ToShortDateString()} - "
                //        + $"{providerCode.EndDate.ToShortDateString()})"
                //});
            }

            // Create the select list from the provider codes
            //var providerCodeSelectList = new SelectList(providerCodeItems, "CodeValue", "CodeText");
            var providerCodeSelectList = new SelectList(providerCodes, "Id", "Code");

            // Get the current provider code
            var currentProviderCode = _providerCodeRepo.GetCurrent();

            // Set the current code selected
            if (currentProviderCode != null)
            {
                //providerCodeSelectList = new SelectList(providerCodeItems, "CodeValue", "CodeText",
                //    currentProviderCode.Code);
                providerCodeSelectList = new SelectList(providerCodes, "Id", "Code",
                    currentProviderCode.Code);
            }

            return providerCodeSelectList;
        }
    }

    public class ProviderCodeSelectItem
    {
        public string CodeValue { get; set; }
        public string CodeText { get; set; }
    }
}
