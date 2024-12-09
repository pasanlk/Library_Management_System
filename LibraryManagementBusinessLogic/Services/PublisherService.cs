using LibraryManagementData.DataModel;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBusinessLogic.Services
{
    public class PublisherService
    {
        private static LibraryEntities publisherDbContext = null;

        public PublisherService()
        {
            publisherDbContext = new LibraryEntities();
        }


        /*
         * All CRUD operations of the Publisher entity
         */
        public void AddPublisher(PublisherModel newPublisher)
        {
            try
            {
                if (newPublisher != null)
                {
                    Publisher newAddingPublisher = new Publisher()
                    {
                        //publisher_id = newPublisher.publisherId,
                        name = newPublisher.name,
                        year_of_publication = newPublisher.yearOfPublication,
                    };

                    publisherDbContext.Publishers.Add(newAddingPublisher);
                    publisherDbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePublisher(PublisherModel newPublisher)
        {
            try
            {

                var existPublisher = publisherDbContext.Publishers.Find(newPublisher.publisherId);
                if (existPublisher != null)
                {
                    existPublisher.name = newPublisher.name;
                    existPublisher.year_of_publication = newPublisher.yearOfPublication;

                    publisherDbContext.SaveChanges();



                

                    //publisherDbContext.Publishers.Append(newUpdatingPublisher);
                    //publisherDbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }


        public PublisherModel GetPublisher(int searchId)
        {
            try
            {
                //return publisherDbContext.Publishers.Find(searchId);

                var publisherModel = new PublisherModel();
                Publisher item = publisherDbContext.Publishers.Find(searchId);

                if (item != null)
                {
                    publisherModel.publisherId = item.publisher_id;
                    publisherModel.name = item.name;
                    publisherModel.yearOfPublication = item.year_of_publication;

                }

                return publisherModel;


            }
            catch (Exception ex) { throw ex; }
        }

        public void DeletePublisher(int searchId)
        {
            try
            {
                Publisher existing = publisherDbContext.Publishers.Find(searchId);
                if (existing != null)
                {
                    publisherDbContext.Publishers.Remove(existing);
                    publisherDbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public List<PublisherModel> GetAllPublishers()
        {
            var publisherModelList = new List<PublisherModel>();
            var publisherList = publisherDbContext.Publishers.ToList();
            if(publisherList != null)
            {
                foreach(var item in publisherList)
                {
                    var publisherModel = new PublisherModel();

                    publisherModel.publisherId = item.publisher_id;
                    publisherModel.name = item.name;
                    publisherModel.yearOfPublication=item.year_of_publication;

                    publisherModelList.Add(publisherModel);


                }

                
            }

            return publisherModelList;
        }
    }
}
