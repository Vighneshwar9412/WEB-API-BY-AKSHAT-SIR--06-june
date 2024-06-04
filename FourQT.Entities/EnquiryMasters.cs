using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class EnquiryMasters
    {

    }

    public class Country
    {
        public int id { get; set; }
        public string name { get; set; }
        public string isocode { get; set; }
        public string isdcode { get; set; }
        public byte mobdigits { get; set; }
    }
    public class State
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public bool isdefault { get; set; }
    }
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool defaultcity { get; set; }
        public bool haszone { get; set; }
        public bool haslocation { get; set; }
    }

    public class Zone
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cityid { get; set; }
    }
    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cityid { get; set; }
    }
    public class Locality
    {
        public int id { get; set; }
        public string name { get; set; }
        public int locationid { get; set; }
    }

    public class Landmark
    {
        public int id { get; set; }
        public string name { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? update_date { get; set; }

        public int created_By { get; set; }

        public int updated_By { get; set; }


    }

    public class Direction
    {

        public int id { get; set; }

        public string name { get; set; }

        public int order { get; set; }
    }

    public class Plan
    {
        public int project_id { get; set; }

        public int id { get; set; }

        public string name { get; set; }

    }


    public class Purpose
    {
        public int id { get; set; }

        public string POI { get; set; }

    }

    public class Project 
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int locationId { get; set; }
    
    }

    public class PaymentPlan
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string? name { get; set; }
    }

    public class Salutation
    {
        public string? name { get; set; }
    }

    public class Area
    {
        public int id { get; set; }
        public string? name { get; set; }
    }

    public class EnquiryMastersType0
    {
        public List<EnquirySource>? enqirySource { get; set; } = new List<EnquirySource>();
        public List<EnquiryType>? enquiryType { get; set; } = new List<EnquiryType>();
        public List<Project>? project { get; set; } = new List<Project>();
        public List<TimeFrame>? timeFrame { get; set; } = new List<TimeFrame>();
        public List<City>? city { get; set; } = new List<City>();
        public List<Zone>? zone { get; set; } = new List<Zone>();
        public List<Location>? location { get; set; } = new List<Location>();
        public List<Locality>? locality { get; set; } = new List<Locality>();
        public List<EnquiryFrom>? enquiryFrom { get; set; } = new List<EnquiryFrom>();
        public List<Budget>? budget { get; set; } = new List<Budget>();
        public List<UnitType>? unitType { get; set; } = new List<UnitType>();
        public List<DumpReason>? dumpReason { get; set; } = new List<DumpReason>();
        public List<Communication>? communication { get; set; } = new List<Communication>();
        public List<PaymentPlan>? paymentPlan { get; set; } = new List<PaymentPlan>();
        public List<MainFollowupType>? mainFollowUpType { get; set; } = new List<MainFollowupType>();
        public List<SubFollowupType>? subFollowUpType { get; set; } = new List<SubFollowupType>();
        public List<Country>? country { get; set; } = new List<Country>();
        public List<Salutation>? salutation { get; set; } = new List<Salutation>();
        public List<Area>? area { get; set; } = new List<Area>();

    }

    public class EnquiryMastersType1
    {
        public List<Country> country { get; set; } = new List<Country>();
        public List<State>? state { get; set; } = new List<State>();
        public List<City>? city { get; set; } = new List<City>();
        public List<Zone>? zone { get; set; } = new List<Zone>();
        public List<Location>? location { get; set; } = new List<Location>();
        public List<Locality>? locality { get; set; } = new List<Locality>();

        public List<Landmark>? landmark { get; set; } = new List<Landmark>();
        public List<Direction>? direction { get; set; } = new List<Direction>();
        public List<Plan>? plan { get; set; } = new List<Plan>();

        public List<Purpose>? purpose { get; set; } = new List<Purpose>();
    }


    public class EnquirySource
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cid { get; set; }
        public string category { get; set; }
        public int displaystatus { get; set; }
    }
    public class SourceCategory
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType2
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<EnquirySource> enquirySources { get; set; } = new List<EnquirySource>();
        public List<SourceCategory> sourceCategories { get; set; } = new List<SourceCategory>();
        //public HttpStatusCode StatusCode { get; set; }
    }

    public class PropertyType
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType3
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<PropertyType> propertytype { get; set; } = new List<PropertyType>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class RequirementType
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType4
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<RequirementType> requirementtype { get; set; } = new List<RequirementType>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class Budget
    {
        public int id { get; set; }
        public string name { get; set; }
        public int budgetvalue { get; set; }
    }
    public class EnquiryMastersType5
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<Budget> budget { get; set; } = new List<Budget>();
        //public HttpStatusCode StatusCode { get; set; }
    }

    public class EnquiryFrom
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType6
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<EnquiryFrom> enquiryFrom { get; set; } = new List<EnquiryFrom>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class EnquiryType
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType7
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<EnquiryType> enquiryType { get; set; } = new List<EnquiryType>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class FloorPreference
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType8
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<FloorPreference> floorPreference { get; set; } = new List<FloorPreference>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class TimeFrame
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType9
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<TimeFrame> timeframe { get; set; } = new List<TimeFrame>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class MainFollowupType
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int sortOrder {  get; set; }
    }
    public class EnquiryStage
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class SubFollowupType
    {
        public string? followuptypeid { get; set; }
        public string? name { get; set; }
        public int mainfollowuptypeid { get; set; }
        public string? mainfollowuptypename { get; set; }
        public string? subResponseIdStageId { get; set; }
        public int behaviour { get; set; }
    }
    public class EnquiryMastersType10
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<MainFollowupType> mainFollowupType { get; set; } = new List<MainFollowupType>();
        public List<EnquiryStage> enquiryStage { get; set; } = new List<EnquiryStage>();
        public List<SubFollowupType> subFollowupType { get; set; } = new List<SubFollowupType>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class DumpReason
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType11
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<DumpReason> dumpReason { get; set; } = new List<DumpReason>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class StatusLegends
    {
        public string status { get; set; }
        public string colorcode { get; set; }
    }
    public class EnquiryMastersType12
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<StatusLegends> statusLegends { get; set; } = new List<StatusLegends>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class Communication
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType13
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<Communication> communication { get; set; } = new List<Communication>();
        //public HttpStatusCode StatusCode { get; set; }
    }
    public class UnitType
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class EnquiryMastersType14
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<UnitType> unitType { get; set; } = new List<UnitType>();
        //public HttpStatusCode StatusCode { get; set; }
    }

    public class Template
    { 
        public int id { get; set; } 
        public string? name { get; set; } 
        public string? body { get; set; }
    }

    public class EnquiryMastersType20
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<Template> templateType { get; set; } = new List<Template>();
        //public HttpStatusCode StatusCode { get; set; }
    }

    public class EnquiryMastersType23
    {
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        public List<Template> emailType { get; set; } = new List<Template>();
        public List<Template> SMSType { get; set; } = new List<Template>();

        public List<Template> WhatsappType { get; set; } = new List<Template>();

        //public HttpStatusCode StatusCode { get; set; }
    }

   public class EnquiryMastersByEnqIdResp
    {
        public List<MainFollowupType>? mainFollowUpType { get; set; } = new List<MainFollowupType>();
        public List<Template> emailType { get; set; } = new List<Template>();
        public List<Template> SMSType { get; set; } = new List<Template>();
        public List<Template> WhatsappType { get; set; } = new List<Template>();
    }

}
