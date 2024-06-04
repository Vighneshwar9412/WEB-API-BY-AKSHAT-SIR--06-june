using FourQT.DAL;
using FourQT.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Reports
{
    public class UserListBLL
    {
        public async Task<dynamic> transferuserlist(string Key, int Login_Id)
        {
            DataSet ds = new DataSet();
            UserListResponseModel users = new UserListResponseModel();
            string spName = "API_Udp_GetPreSalesCallerEmployee";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@LoginId", Value =  Login_Id },
            };

            ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                UserList u1 = new UserList(); 
                u1.id = Convert.ToInt32(rows["LOGIN_ID"].ToString());
                u1.name = rows["User_name"].ToString();
                u1.mobileNo= rows["MobileNo"].ToString();
                u1.empId= Convert.ToInt32(rows["Emp_ID"].ToString());
                users.userlistSources.Add(u1);
            }

            return users.userlistSources;
        }

        public async Task<dynamic> getTeamWiseEmployees(string Key, int Login_Id)
        {
            DataSet ds = new DataSet();
            List<EmployeeList> users = new List<EmployeeList>();

            string spName = "API_GetTeamwiseEmployee";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@LoginID", Value =  Login_Id },
            };

            ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                EmployeeList u1 = new EmployeeList();
                u1.id = (Int32.TryParse(rows["Login_Id"].ToString(), out int id) ? id : 0);
                u1.empName = (rows["Emp_Name"] != null ? rows["Emp_Name"].ToString() : "");
                u1.empId = (Int32.TryParse(rows["Emp_Id"].ToString(), out id) ? id : 0);
                u1.name = (rows["User_name"] != null ? rows["User_name"].ToString() : "");
                u1.active = (Boolean.TryParse(rows["Active"].ToString(), out Boolean active) ? active : false);
                users.Add(u1);
            }

            return users;
        }
    }
}
