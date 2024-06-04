using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BrokerPortalAPI.Interfaces
{
    public interface IInventory
    {
        Task<dynamic> getInventoryList(HttpRequest req, int projectId, int towerid, string type);
    }
}
