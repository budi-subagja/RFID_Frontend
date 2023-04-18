using Newtonsoft.Json;

namespace RFIDP2P3_Web.Models
{
    public class Privilege
    {
        [JsonProperty("Menu_Id")]
        public string? Menu_Id { get; set; }
        [JsonProperty("checkedbox_read")]
        public string? checkedbox_read { get; set; }
        [JsonProperty("checkedbox_add")]
        public string? checkedbox_add { get; set; }
        [JsonProperty("checkedbox_edit")]
        public string? checkedbox_edit { get; set; }
        [JsonProperty("checkedbox_del")]
        public string? checkedbox_del { get; set; }
    }
}
