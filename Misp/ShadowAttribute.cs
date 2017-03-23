using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misp
{

    [JsonObject]
    public class ShadowAttribute : Attribute
    {
        /// <summary>
        /// old_id represents a human-readable identifier referencing the Attribute object that the ShadowAttribute belongs to. A ShadowAttribute can this way target an existing Attribute, implying that it is a proposal to modify an existing Attribute, or alternatively it can be a proposal to create a new Attribute for the containing Event.
        /// </summary>
        [JsonProperty("old_id", NullValueHandling = NullValueHandling.Ignore)]
        public String OldId { get; set; }

        /// <summary>
        /// org_id represents a human-readable identifier referencing the proposal creator's Organisation object.
        /// Whilst attributes can only be created by the event creator organisation, shadow attributes can be created by third parties.org_id tracks the creator organisation.
        /// </summary>
        [JsonProperty("org_id", NullValueHandling = NullValueHandling.Ignore)]
        public String OrgId { get; set; }


        /// <summary>
        /// proposal_to_delete is a boolean flag that sets whether the shadow attribute proposes to alter an attribute, or whether it proposes to remove it completely.
        /// Accepting a shadow attribute with this flag set will remove the target attribute.
        /// </summary>
        [JsonProperty("proposal_to_delete", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? ProposalToDelete { get; set; }

        /// <summary>
        /// deleted represents a setting that allows shadow attributes to be revoked. Revoked shadow attributes only serve to inform other instances that the shadow attribute is no longer active.
        /// </summary>
        [JsonProperty("deleted", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? Deleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Org", NullValueHandling = NullValueHandling.Ignore)]
        public Org Org { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public new static ShadowAttribute FromJson(String json)
        {
            return JsonConvert.DeserializeObject<ShadowAttribute>(json);
        }

    }
}
