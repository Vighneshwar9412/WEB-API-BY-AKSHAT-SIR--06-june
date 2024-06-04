using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class EmployeeInventory
    {
        public int? id { get; set; }
        public string? name { get; set; }
    }

    public class EmployeeInventorySelect : EmployeeInventory
    {
        public int selected { get; set; }
    }

    public class InventoryProjectList : InventoryProject
    {
        public List<EmployeeInventory>? project { get; set; } = new List<EmployeeInventory>();
    }

    public class InventoryFloorList 
    {
        public List<EmployeeInventory>? floor { get; set; } = new List<EmployeeInventory>();
    }

    public class InventoryMastersFromProject : InventoryTower, InventoryUnitTypeGroup, InventoryUnitType, InventoryLocation, InventoryUnitOwner, InventoryBaseFloor
    {
        public List<EmployeeInventorySelect>? project { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventory>? tower { get; set; } = new List<EmployeeInventory>();
        public List<EmployeeInventory>? baseFloor { get; set; } = new List<EmployeeInventory>();
        public List<EmployeeInventory>? unitTypeGroup { get; set; } = new List<EmployeeInventory>();
        public List<EmployeeInventory>? unitType { get; set; } = new List<EmployeeInventory>();
        public List<EmployeeInventory>? location { get; set; } = new List<EmployeeInventory>();
        public List<EmployeeInventory>? unitOwner { get; set; } = new List<EmployeeInventory>();
        public List<EmployeeInventory>? channelPartner { get; set; } = new List<EmployeeInventory>();
    }

    public class LeadInventoryMasters
    {
        public List<EmployeeInventorySelect>? project { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventorySelect>? tower { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventorySelect>? floor { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventorySelect>? unitTypeGroup { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventorySelect>? unitType { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventorySelect>? location { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventorySelect>? unitOwner { get; set; } = new List<EmployeeInventorySelect>();
        public List<EmployeeInventorySelect>? channelPartner { get; set; } = new List<EmployeeInventorySelect>();
    }

    public interface InventoryProject
    {
        public List<EmployeeInventory>? project { get; set; }
    }

    public interface InventoryTower
    {
        public List<EmployeeInventory>? tower { get; set; }
    }

    public interface InventoryBaseFloor
    {
        public List<EmployeeInventory>? baseFloor { get; set; }
    }

    public interface InventoryFloor
    {
        public List<EmployeeInventory>? floor { get; set; }
    }

    public interface InventoryUnitTypeGroup
    {
        public List<EmployeeInventory>? unitTypeGroup { get; set; } 
    }

    public interface InventoryUnitType
    {
        public List<EmployeeInventory>? unitType { get; set; } 
    }

    public interface InventoryLocation
    {
        public List<EmployeeInventory>? location { get; set; }
    }

    public interface InventoryUnitOwner
    {
        public List<EmployeeInventory>? unitOwner { get; set; } 
    }

}
