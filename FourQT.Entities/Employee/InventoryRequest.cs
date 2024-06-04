using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class FilterDropdownRequest : FloorRequest
    { 
        
    }
    public class InventoryRequestShort : InventoryRequestCore
    {
        public int projectId { get; set; }
    }

    public class InventoryRequestMasters : InventoryRequestCore, InventoryRequestAdd
    {
        public int projectId { get; set; }
        public int towerId { get; set; }
        public int? floorId { get; set; }
        public int? unitTypeGroupId { get; set; }
        public int? unitTypeId { get; set; }
        public string? locationId { get; set; }
        public int? ownerId { get; set; }
        public string? type { get; set; }
        public int brokerId { get; set; }
    }

    public class InventoryRequestLong : InventoryRequestCore,InventoryRequestAdd, InventoryRequestFilter
    {
        public int projectId { get; set; }
        public int towerId { get; set; }
        public int? floorId { get; set; }
        public int? unitTypeGroupId { get; set; }
        public int? unitTypeId { get; set; }
        public string? locationId { get; set; }
        public int? ownerId { get; set; }
        public string? type { get; set; }
        public string? unitNo { get; set; }       
        public decimal? areaMin { get; set; }
        public decimal? areaMax { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public string? bookingDateFrom { get; set; }
        public string? bookingDateTo { get; set; }
    }

    public class FloorRequest : InventoryRequestCore
    {
        public int projectId { get; set; }
        public int towerId { get; set; }
    }

    public interface InventoryRequestCore
    {
        public int projectId { get; set; }
    }

    public interface InventoryRequestAdd
    {
        public int towerId { get; set; }
        public int? floorId { get; set; }
        public int? unitTypeGroupId { get; set; }
        public int? unitTypeId { get; set; }
        public string? locationId { get; set; }
        public int? ownerId { get; set; }
        public string? type { get; set; }

    }

    public interface InventoryRequestFilter
    {
        public string? unitNo { get; set; }
        public decimal? areaMin { get; set; }
        public decimal? areaMax { get; set; }
        public string? bookingDateFrom { get; set; }
        public string? bookingDateTo { get; set; }
    }

}
