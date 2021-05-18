using Microsoft.AspNetCore.Mvc.Rendering;
using PDHourTracker.Web.Models.ProviderCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IProviderCodeService
    {
        void AddOrUpdate(ProviderCodeViewModel providerCodeViewModel);
        ProviderCodeViewModel Get(int id);
        ProviderCodeViewModel GetCurrent();
        List<ProviderCodeViewModel> GetProviderCodes(int pageNum, int pageSize);
        SelectList ProviderCodesForView();
        bool Exists(string code);
    }
}
