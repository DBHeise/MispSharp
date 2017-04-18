using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misp
{
    [JsonObject]
    public class Attribute
    {

        public Attribute()
        {
            this.Distribution = "5";
        }

        public Attribute(String type, String value) : this()
        {
            this.Type = type;
            this.Value = value;
        }
        public Attribute(String type, String category, String value) : this(type, value)
        {
            this.Category = category;
        }
        public Attribute(String type, String category, String value, Boolean toIDS) : this(type, category, value)
        {
            this.ToIDS = toIDS;
        }

        /// <summary>
        /// uuid represents the Universally Unique IDentifier (UUID) [@!RFC4122] of the attribute.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid? UUID { get; set; }

        /// <summary>
        /// id represents the human-readable identifier associated to the attribute for a specific MISP instance.
        /// </summary>
        [JsonProperty("id")]
        public String Id { get; set; }

        /// <summary>
        /// type represents the means through which an attribute tries to describe the intent of the attribute creator, using a list of pre-defined attribute types.
        /// </summary>
        [JsonProperty("type"), JsonRequired]
        public String Type { get; set; }

        /// <summary>
        /// category represents the intent of what the attribute is describing as selected by the attribute creator, using a list of pre-defined attribute categories.
        /// </summary>
        [JsonProperty("category"), JsonRequired]
        public String Category { get; set; }

        /// <summary>
        /// to_ids represents whether the attribute is meant to be actionable. Actionable defined attributes that can be used in automated processes as a pattern for detection in Local or Network Intrusion Detection System, log analysis tools or even filtering mechanisms.
        /// </summary>
        [JsonProperty("to_ids")]
        public Boolean ToIDS { get; set; }


        /// <summary>
        /// event_id represents a human-readable identifier referencing the Event object that the attribute belongs to.
        /// </summary>
        [JsonProperty("event_id")]
        public String EventId { get; set; }

        /// <summary>
        /// distribution represents the basic distribution rules of the attribute. The system must adhere to the distribution setting for access control and for dissemination of the attribute.
        /// </summary>
        [JsonProperty("distribution")]
        public String Distribution { get; set; }

        /// <summary>
        /// timestamp represents a reference time when the attribute was created or last modified. timestamp is expressed in seconds (decimal) since 1st of January 1970 (Unix timestamp). The time zone MUST be UTC.
        /// </summary>
        [JsonProperty("timestamp")]
        public String Timestamp { get; set; }

        /// <summary>
        /// comment is a contextual comment field.
        /// </summary>
        [JsonProperty("comment")]
        public String Comment { get; set; }

        /// <summary>
        /// sharing_group_id represents a human-readable identifier referencing a Sharing Group object that defines the distribution of the attribute, if distribution level "4" is set. If a distribution level other than "4" is chosen the sharing_group_id MUST be set to "0".
        /// </summary>
        [JsonProperty("sharing_group_id")]
        public String SharingGroupId { get; set; }

        /// <summary>
        /// data contains the base64 encoded contents of an attachment or a malware sample. For malware samples, the sample MUST be encrypted using a password protected zip archive, with the password being "infected". 
        /// data is represented by a JSON string in base64 encoding. data MUST be set for attributes of type malware-sample and attachment.
        /// </summary>
        [JsonProperty("data")]
        public String Data { get; set; }

        /// <summary>
        /// RelatedAttribute is an array of attributes correlating with the current attribute. Only the correlations found on the local instance are shown in RelatedAttribute.
        /// </summary>
        [JsonProperty("RelatedAttribute")]
        public Attribute[] RelatedAttribute { get; set; }

        /// <summary>
        /// ShadowAttribute is an array of shadow attributes that serve as proposals by third parties to alter the containing attribute.
        /// </summary>
        [JsonProperty("ShadowAttribute")]
        public ShadowAttribute[] ShadowAttribute { get; set; }

        /// <summary>
        /// value represents the payload of an attribute.
        /// </summary>
        [JsonProperty("value"), JsonRequired]
        public String Value { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        [JsonProperty("disable_correlation")]
        public Boolean? DisableCorrelation { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        [JsonProperty("ShadowGroup")]
        public Object[] ShadowGroup { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, JsonHelper.Settings);
        }
        public static Attribute FromJson(String json)
        {
            return JsonConvert.DeserializeObject<Attribute>(json, JsonHelper.Settings);
        }

    }
}
