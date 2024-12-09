using LibraryManagementBusinessLogic.Services;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBusinessLogic.Facade
{

    //These are use to write the business logic 
    public class StaffFacade
    {

        StaffService staffService = new StaffService();
        public List<StaffModel> GetAllStaffs()
        {
            
            return staffService.GetAllStaffs();
        }


        public void AddStaff(StaffModel staffModel)
        {
            staffService.AddStaff(staffModel);
        }

        public StaffModel GetStaffs(int id)
        {
            return(staffService.GetStaff(id));
        }

        public void DeleteStaff(int id)
        {
            staffService.DeleteStaff(id);
        }
        public void UpdateStaff(StaffModel staffModel)
        {
            staffService.UpdateStaff(staffModel);
        }
    }
}
