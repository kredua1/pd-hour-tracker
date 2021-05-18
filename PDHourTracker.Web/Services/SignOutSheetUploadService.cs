using AutoMapper;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Models.SignOutSheets;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class SignOutSheetUploadService : ISignOutSheetUploadService
    {
        private ISignOutSheetUploadRepo<SignOutSheetUpload> _signOutSheetUploadRepo;
        private IMapper _mapper;

        public SignOutSheetUploadService(
            ISignOutSheetUploadRepo<SignOutSheetUpload> signOutSheetUploadRepo,
            IMapper mapper)
        {
            _signOutSheetUploadRepo = signOutSheetUploadRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Copy data from uploaded file to db.
        /// Returns id of new entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(SignOutSheetUploadModel model)
        {
            var signOutSheetUpload = new SignOutSheetUpload();

            signOutSheetUpload.WorkshopId = model.WorkshopId;
            signOutSheetUpload.FileName = model.UploadFile.FileName;
            signOutSheetUpload.FileSize = model.UploadFile.Length;
            signOutSheetUpload.ContentType = model.UploadFile.ContentType;

            // Get file data from input stream
            using (var memoryStream = new MemoryStream())
            {
                model.UploadFile.CopyTo(memoryStream);
                signOutSheetUpload.FileData = memoryStream.ToArray();
            }

            signOutSheetUpload = _signOutSheetUploadRepo.Add(signOutSheetUpload);

            return signOutSheetUpload.Id;
        }

        /// <summary>
        /// Deletes uploaded file for given id.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _signOutSheetUploadRepo.Delete(id);
        }

        /// <summary>
        /// Gets file for given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SignOutSheetUploadModel Get(int id)
        {
            var signOutSheetUploadModel = new SignOutSheetUploadModel();

            var signOutSheetUpload = _signOutSheetUploadRepo.Get(id);

            if(signOutSheetUpload != null)
            {
                signOutSheetUploadModel = _mapper.Map<SignOutSheetUploadModel>(
                    signOutSheetUpload);
            }

            return signOutSheetUploadModel;
        }

        /// <summary>
        /// Gets Id's of all uploaded files for given workshop
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public List<int> GetIdsByWorkshop(int workshopId)
        {
            return _signOutSheetUploadRepo.GetIdsByWorkshop(workshopId);
        }
    }
}
