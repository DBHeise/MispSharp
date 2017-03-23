using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misp
{
    [JsonObject]
    public class Galaxy
    {
        [JsonProperty("id")] public String Id { get; set; }
        [JsonProperty("uuid")] public Guid UUID { get; set; }
        [JsonProperty("name")] public String Name { get; set; }
        [JsonProperty("type")] public String Type { get; set; }
        [JsonProperty("description")] public String Description { get; set; }
        [JsonProperty("version")] public String Version { get; set; }
        [JsonProperty("GalaxyCluster")] public GalaxyCluster[] Clusters { get; set; }
    }
}
