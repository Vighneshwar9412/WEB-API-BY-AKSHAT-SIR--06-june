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
    public  class SVListBLL
    {
        public Object listsvsiteVisit(string Key, int Login_Id,int Enquiry_ID )
        {
            DataSet ds = new DataSet();
            MeetingResponseModel meeting = new MeetingResponseModel();
            string spName = "API_GetSVDoneList";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id },
                new SqlParameter() { ParameterName = "@Enquiry_Id", Value = Enquiry_ID}
            };

            ds = DBHelper.GetDataset(Key, CommandType.StoredProcedure, spName, lstParam);

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                Meeting m1 = new Meeting();
                m1.Name = rows[0].ToString();
                m1.FollowedBy= rows[1].ToString();
                m1.MeetingDatetime= Convert.ToDateTime(rows[2].ToString());
                m1.Duration = rows[3].ToString();
                m1.Project_Name = rows[4].ToString();
                m1.Location = rows[5].ToString();
                meeting.meetingsources.Add(m1);

            }
            return meeting.meetingsources;
        }
    }
}
