using MobAppCoreAPI.Models.Response;
using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IListAllLeads
    {
        Task<dynamic> LeadslistAll(HttpRequest req, LeadFilters leadsList,HttpContext context);
    }
}
