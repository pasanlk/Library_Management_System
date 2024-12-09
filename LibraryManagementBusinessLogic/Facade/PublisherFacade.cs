using LibraryManagementBusinessLogic.Services;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBusinessLogic.Facade
{
    public class PublisherFacade
    {
        public List<PublisherModel> GetAllPublishers()
        {
            PublisherService publisherService = new PublisherService();
            return publisherService.GetAllPublishers();
        }

        public void AddPublisher(PublisherModel publisherModel)
        {
            PublisherService publisherService = new PublisherService();
            publisherService.AddPublisher(publisherModel);      
        }

        public PublisherModel GetPublisher(int searchId) {
            PublisherService publisherService = new PublisherService();
            return publisherService.GetPublisher(searchId);
        }

        public void updatePublisher(PublisherModel newPublisher) {

            PublisherService publisherService = new PublisherService();
            publisherService.UpdatePublisher(newPublisher);
        
        }

        public void DeletePublisher(int searchId)
        {
            PublisherService publisherService = new PublisherService();
            publisherService.DeletePublisher(searchId);
        }
    }
}
