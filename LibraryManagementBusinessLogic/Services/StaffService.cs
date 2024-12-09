using LibraryManagementData.DataModel;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBusinessLogic.Services
{
    internal class StaffService
    {

        private static LibraryEntities staffDbContext=null;

        public StaffService()
        {
            staffDbContext = new LibraryEntities();
        }

        
        public void AddStaff(StaffModel newStaff)
        {



            try
            {
                if (newStaff != null)
                {
                    Staff newAddingStaff = new Staff()
                    {
                        //staff_id = newStaff.staffId,
                        name = newStaff.name,
                        email= newStaff.email,
                        phone= newStaff.phone,
                        password= newStaff.password,
                        
                    };


                    staffDbContext.Staffs.Add(newAddingStaff);
                    staffDbContext.SaveChanges();   
                }

                

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateStaff(StaffModel newStaff)
        {
            try
            {

                var existStaff = staffDbContext.Staffs.Find(newStaff.staffId);
                if (newStaff != null)
                {
                    //existStaff.staff_id = newStaff.staffId;
                    existStaff.name = newStaff.name;
                    existStaff.email = newStaff.email;
                    existStaff.phone = newStaff.phone;
                    existStaff.password = newStaff.password;

                    staffDbContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get single staffs details from staff id
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns></returns>
        public StaffModel GetStaff(int searchId)
        {
            try
            {
                //return staffDbContext.Staffs.Find(searchId);
                var staffModel = new StaffModel();
                var item = staffDbContext.Staffs.Find(searchId);

                if(item != null)
                {
                    staffModel.staffId = item.staff_id;
                    staffModel.name = item.name;
                    staffModel.email = item.email;
                    staffModel.phone = item.phone;
                    staffModel.password = item.password;
                }

                return staffModel;

            }
            catch(Exception ex) {
                throw ex;
            }
        }

        public void DeleteStaff(int searchId)
        {
            try
            {
                Staff existing = staffDbContext.Staffs.Find(searchId);
                staffDbContext.Staffs.Remove(existing);
                staffDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<StaffModel> GetAllStaffs()
        {
            List<StaffModel> staffModelList = new List<StaffModel>();
            List<Staff> staffList = staffDbContext.Staffs.ToList();

            if(staffList != null)
            {
                foreach(var item in staffList)
                {
                    var staffModel= new StaffModel();

                    staffModel.staffId = item.staff_id;
                    staffModel.name = item.name;
                    staffModel.email = item.email;
                    staffModel.phone = item.phone;
                    staffModel.password = item.password;


                    staffModelList.Add(staffModel);
                }
            }

            return staffModelList;
        }





    }
}
