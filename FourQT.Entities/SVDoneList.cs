using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class SVDoneList : Lead
    {
      //  public string Name { get; set; }

        public string Followed_By { get; set; }

        public string Meeting_Datetime    { get; set; }

        public string Duration  { get; set; }

        public string Project_Name   { get; set;}

        public string Location { get; set;}

    }
}
