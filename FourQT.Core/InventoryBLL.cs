using FourQT.DAL;
using FourQT.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Core
{
    public  class InventoryBLL
    {
        public Object getInventoryList(string Key, int Login_Id, int ProjectId,int towerId,string type)
        {

            DataSet ds = new DataSet();
            InventoryResponseModel inven = new InventoryResponseModel();
            try
            {
                string spName = "API_B_UnitStatus";
                List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Project_Id", Value = ProjectId },
                new SqlParameter() {ParameterName = "@Tower_Id", Value = towerId},
                new SqlParameter() {ParameterName = "@Type", Value = type}
            };

                ds = DBHelper.GetDataset(Key, CommandType.StoredProcedure, spName, lstParam);
                
                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        Inventory f1 = new Inventory();
                        f1.ProjectName = rows["Project_name"].ToString();
                        f1.TowerName = rows["Project_Tower_name"].ToString();
                        f1.FloorName = rows["Project_Tower_Floor_Name"].ToString();
                        f1.GroupName = rows["UnitType_GroupName"].ToString();
                        f1.UnitType = rows["UnitType"].ToString();
                        f1.UnitNo = rows["UnitNo"].ToString();
                        f1.UnitLocation = rows["Unit_Location"].ToString();
                        f1.Status = rows["Status"].ToString();
                        f1.HoldBy = rows["Holdby"].ToString();
                        //    f1.meetingaddress = rows[9].ToString();
                        f1.HoldDate = rows["HoldDate"].ToString();
                        f1.SuperArea = rows["SuperArea"].ToString();
                        f1.CarpetArea = rows["Carpet_Area"].ToString();
                        f1.BuildUpArea = rows["Build_Up_Area"].ToString();


                        inven.InventoryList.Add(f1);

                    }

                
            }
            catch(Exception ex)
            {
                

            }
            return inven.InventoryList;
        }
    }
}
