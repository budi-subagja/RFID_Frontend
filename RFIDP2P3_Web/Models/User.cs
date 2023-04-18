using Newtonsoft.Json;

namespace RFIDP2P3_Web.Models
{
    public class User
    {
        [JsonProperty("PIC_ID")]
        public string? PIC_ID { get; set; }
        [JsonProperty("password")]
        public string? password { get; set; }
        [JsonProperty("PIC_Name")]
        public string? PIC_Name { get; set; }
        [JsonProperty("UserGroup_Id")]
        public string? UserGroup_Id { get; set; }
        [JsonProperty("PlantId")]
        public string? PlantId { get; set; }
        [JsonProperty("Privileges")]
        public List<Privilege>? Privileges { get; set; }
    }
}
