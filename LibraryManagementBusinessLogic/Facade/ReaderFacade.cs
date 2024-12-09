using LibraryManagementBusinessLogic.Services;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBusinessLogic.Facade
{
    public class ReaderFacade
    {
        public List<ReaderModel> GetAllReaders()
        {
            ReaderService readerService= new ReaderService();
            return readerService.GetAllReaders();
        }

        public void AddReader(ReaderModel readerModel)
        {
            ReaderService readerService = new ReaderService();
            readerService.AddReader(readerModel);
        }

        public ReaderModel GetReader(int searchId)
        {
            ReaderService readerService = new ReaderService();
            return readerService.GetReader(searchId);
        }

        public void updateReader(ReaderModel newReader)
        {

            ReaderService readerService = new ReaderService();
            readerService.UpdateReader(newReader);

        }

        public void DeleteReader(int searchId)
        {
            ReaderService readerService = new ReaderService();
            readerService.DeleteReader(searchId);
        }

    }
}
