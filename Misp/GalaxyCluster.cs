using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misp
{
    [JsonObject]
    public class GalaxyCluster
    {
        [JsonProperty("id")] public String Id { get; set; }
        [JsonProperty("uuid")] public Guid UUID { get; set; }
        [JsonProperty("type")] public String Type { get; set; }
        [JsonProperty("tag_name")] public String TagName { get; set; }
        [JsonProperty("description")] public String Description { get; set; }
        [JsonProperty("galaxy_id")] public String GalaxyId { get; set; }
        [JsonProperty("source")] public String Source { get; set; }
        [JsonProperty("authors")] public String[] Authors { get; set; }
        [JsonProperty("tag_id")] public String TagId { get; set; }
        [JsonProperty("meta")] public object[] Meta { get; set; }

    }
}
