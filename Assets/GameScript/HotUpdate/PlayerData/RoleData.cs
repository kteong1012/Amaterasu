using Nino.Serialization;

namespace Game
{
    [NinoSerialize]
    public partial class RoleData : PlayerData
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int RoleLevel { get; set; }
        public int RoleExp { get; set; }
    }
}
