using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class UnitDetailList
    {
        [DataMember]
        public int Registration_Id { get; set; }
        [DataMember]
        public string ProjectPhoto { get; set; }
        [DataMember]
        public string Project_Name { get; set; }

        [DataMember]
        public string Tower { get; set; }

        [DataMember]
        public string TowerLabel { get; set; }

        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string Unit_No { get; set; }

        [DataMember]
        public string Plan { get; set; }
        [DataMember]
        public string Unit_Type { get; set; }


        [DataMember]
        public string Plot_Area { get; set; }
        [DataMember]
        public string Super_Area { get; set; }
        [DataMember]
        public string Build_Up_Area { get; set; }
        [DataMember]
        public string Carpet_Area { get; set; }
        [DataMember]
        public string Balcony_Area { get; set; }

        [DataMember]
        public string Balcony_Area1 { get; set; }

        [DataMember]
        public string Balcony_Area2 { get; set; }

        [DataMember]
        public string Balcony_Area3 { get; set; }

        [DataMember]
        public string UOM { get; set; }
        [DataMember]
        public string LoanInfo { get; set; }
        [DataMember]
        public string Bank_Name { get; set; }


        [DataMember]
        public bool IsShow_PlotArea { get; set; }
        [DataMember]
        public bool IsShow_SuperArea { get; set; }
        [DataMember]
        public bool IsShow_BuiltUpArea { get; set; }
        [DataMember]
        public bool IsShow_CarpetArea { get; set; }
        [DataMember]
        public bool IsShow_BalconyArea { get; set; }

        [DataMember]
        public bool IsShow_Floor { get; set; }

        [DataMember]
        public bool IsShow_BalconyArea1 { get; set; }

        [DataMember]
        public bool IsShow_BalconyArea2 { get; set; }

        [DataMember]
        public bool IsShow_BalconyArea3 { get; set; }


        [DataMember]
        public string ProjectPhoto_Color { get; set; }
        [DataMember]
        public string Project_Name_Color { get; set; }
        [DataMember]
        public string Tower_Color { get; set; }
        [DataMember]
        public string Floor_Color { get; set; }
        [DataMember]
        public string Unit_No_Color { get; set; }
        [DataMember]
        public string Plan_Color { get; set; }
        [DataMember]
        public string Unit_Type_Color { get; set; }
        [DataMember]
        public string LoanInfo_Color { get; set; }
        [DataMember]
        public string Bank_Name_Color { get; set; }


        [DataMember]
        public string Plot_Area_Color { get; set; }
        [DataMember]
        public string Super_Area_Color { get; set; }
        [DataMember]
        public string Build_Up_Area_Color { get; set; }
        [DataMember]
        public string Carpet_Area_Color { get; set; }
        [DataMember]
        public string Balcony_Area_Color { get; set; }


        [DataMember]
        public string Balcony1_Area_Color { get; set; }
        [DataMember]
        public string Balcony2_Area_Color { get; set; }
        [DataMember]
        public string Balcony3_Area_Color { get; set; }


        [DataMember]
        public string Balcony1_Area_Label { get; set; }

        [DataMember]
        public string Balcony2_Area_Label { get; set; }

        [DataMember]
        public string Balcony3_Area_Label { get; set; }

    }
}
