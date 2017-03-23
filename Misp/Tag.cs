using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Misp
{
    /// <summary>
    /// A Tag is a simple method to classify an event with a simple tag name.
    /// </summary>
    [JsonObject]
    public class Tag
    {

        public Tag() { }
        public Tag(String name) : this()
        {
            this.Name = name;
        }
        public Tag(String name, Color color, Boolean isExportable) : this(name, ColorTranslator.ToHtml(color), isExportable)
        {
        }
        public Tag(String name, String color, Boolean isExportable) : this(name)
        {
            this.Color = color;
            this.Exportable = isExportable;
        }


        /// <summary>
        /// The tag name can be freely chosen. The tag name can be also chosen from a fixed machine-tag vocabulary called MISP taxonomies.
        /// </summary>
        [JsonProperty("name"), JsonRequired]
        public String Name { get; set; }

        /// <summary>
        /// id is a human-readable identifier that references the tag on the local instance. 
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public String Id { get; set; }

        /// <summary>
        /// colour represents an RGB value of the tag.
        /// </summary>
        [JsonProperty("colour", NullValueHandling = NullValueHandling.Ignore)]
        public String Color { get; set; }

        /// <summary>
        /// exportable represents a setting if the tag is kept local or exportable to other MISP instances.
        /// </summary>
        [JsonProperty("exportable", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? Exportable { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        [JsonProperty("org_id", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? OrgOnly { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        [JsonProperty("hide_tag", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? Hidden { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public uint? EventCount { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        [JsonProperty("attribute_count", NullValueHandling = NullValueHandling.Ignore)]
        public uint? AttributeCount { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        [JsonProperty("favourite", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? IsFavorite { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Tag FromJson(String json)
        {
            return JsonConvert.DeserializeObject<Tag>(json);
        }

    }

    [JsonObject]
    internal class TagWrapper
    {
        [JsonProperty("Tag")]
        public Tag[] Tags { get; set; }


        public TagWrapper(Tag[] tags)
        {
            this.Tags = tags;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static TagWrapper FromJson(String json)
        {
            return JsonConvert.DeserializeObject<TagWrapper>(json);
        }

    }
}
