using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Misp
{
    public class MispServer : RestServer
    {
        protected String key = null;
        protected String[] types = null;
        protected String[] categories = null;
        protected Dictionary<String, String> typeCatMap;
        protected Dictionary<String, String[]> catTypeMap;
        protected Tag[] tags;

        public MispServer(String baseurl, String authKey) : base(baseurl)
        {
            this.key = authKey;            
        }

        public void Init()
        {
            String data = this.Get("/attributes/describeTypes.json");
            var obj = JsonConvert.DeserializeObject<DescribeTypes>(data);
            this.types = obj.Result.Types;
            this.categories = obj.Result.Categories;
            this.typeCatMap = new Dictionary<string, string>();
            foreach(var t in obj.Result.SaneDefaults)
            {
                this.typeCatMap.Add(t.Key, t.Value.Value<String>("default_category"));
            }
            this.catTypeMap = new Dictionary<string, string[]>();
            foreach(var c in obj.Result.CategoryTypeMappings)
            {
                this.catTypeMap.Add(c.Key, c.Value.Values<String>().ToArray());
            }

            String tagData = this.Get("/tags");
            this.tags = JsonConvert.DeserializeObject<TagWrapper>(tagData).Tags;
        }

        public String[] GetAvailableTagNames()
        {
            SortedSet<String> tagnames = new SortedSet<string>();
            foreach(Tag t in this.tags)
            {
                tagnames.Add(t.Name);
            }
            return tagnames.ToArray();
        }
        public String GetDefaultCategoryForType(String type)
        {
            if (this.typeCatMap.ContainsKey(type))
                return this.typeCatMap[type];
            else
                return null;
        }
        public String[] GetTypesForCategory(String category)
        {
            if (this.catTypeMap.ContainsKey(category))
                return this.catTypeMap[category];
            else
                return null;
        }

        protected override HttpWebRequest NewRequest(string relativeUri, string verb)
        {
            HttpWebRequest request = base.NewRequest(relativeUri, verb);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", key);
            return request;
        }

        public MispEvent AddEvent(MispEvent evnt) {
           return MispEventWrapper.FromJson(this.Post("/events", new MispEventWrapper(evnt).ToString())).Event;
        }
        public MispEvent GetEvent(String id) {
            String data = this.Get("/events/" + id);
            return MispEventWrapper.FromJson(data).Event;
        }

        public MispEvent[] GetEvents() { throw new NotImplementedException(); }
        public MispEvent[] GetEvents(String searchJson) { throw new NotImplementedException(); }

        public void UpdateEvent(MispEvent evnt) { throw new NotImplementedException(); }

        public void DeleteEvent(MispEvent evnt) { throw new NotImplementedException(); }
        public void DeleteEvent(String id) { throw new NotImplementedException(); }

    }
}
