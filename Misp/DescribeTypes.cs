

namespace Misp
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class Result
    {

        [JsonProperty("sane_defaults")]
        public JObject SaneDefaults { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }

        [JsonProperty("categories")]
        public string[] Categories { get; set; }

        [JsonProperty("category_type_mappings")]
        public JObject CategoryTypeMappings { get; set; }
    }

    internal class DescribeTypes
    {

        [JsonProperty("result")]
        public Result Result { get; set; }
    }

}
