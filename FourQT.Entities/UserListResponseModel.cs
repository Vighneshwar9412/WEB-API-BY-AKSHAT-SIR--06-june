using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public interface IUserList 
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int empId { get; set; }
    }

    public class UserList : IUserList
    { 
        public int id { get; set; }
        public string? name { get; set; }
        
        public int  empId { get; set; }

        public string? mobileNo { get; set; }
    }

    public class EmployeeList : IUserList
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? empName { get; set; }      
        public int empId { get; set; }
        public Boolean active { get; set; }
    }

    public  class UserListResponseModel
    {
        public List<UserList> userlistSources { get; set; } = new List<UserList>();
    }
}
