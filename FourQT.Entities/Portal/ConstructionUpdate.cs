using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
  public  class ConstructionUpdate
  {
      [DataMember]
      public List<ProjectList>? ProjctList { get; set; }

      [DataMember]
      public List<TowerList>? TowerList { get; set; }
    }
}
