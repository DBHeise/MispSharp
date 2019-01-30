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
    public class MispServerException : Exception
    {
        internal MispServerException() : base() { }
        internal MispServerException(String msg, Exception inner) : base(msg, inner) { }
        internal MispServerException(MispEventWrapper wrapper) : base(wrapper.Errors)
        {
            this.Name = wrapper.Name;
            this.Url = wrapper.Url;            
        }
        public String Name { get; set; }
        public String Url { get; set; }
    }


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

        private MispEvent handleResponse(String data)
        {
            var wrapper = MispEventWrapper.FromJson(data);
            if (wrapper.Event != null)
            {
                return wrapper.Event;
            }
            else
            {
                throw new MispServerException(wrapper);
            }
        }

        public MispEvent AddEvent(MispEvent evnt) {
           return handleResponse(this.Post("/events", new MispEventWrapper(evnt).ToString()));
        }
        public MispEvent GetEvent(String id) {
            try
            {
                return handleResponse(this.Get("/events/" + id));
            }
            catch (System.Net.WebException webException)
            {
                var r = (System.Net.HttpWebResponse)webException.Response;
                if (r.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new MispServerException("Not Found", webException);
                }
                else
                    throw;
            }
        }

        public MispEvent[] GetEvents()
        {
            var data = this.Get("/events");
            return JsonConvert.DeserializeObject<MispEvent[]>(data, JsonHelper.Settings);
        }
        public MispEvent[] GetEvents(String searchJson) { throw new NotImplementedException(); }

        public MispEvent UpdateEvent(MispEvent evnt) {            
            return handleResponse(this.Post("/events/" + evnt.Id, new MispEventWrapper(evnt).ToString()));
        }
        
        public MalwareSampleResponse DownloadMalware(string md5)
        {
            string jsonResult = this.Download("attributes/downloadSample/" + md5);
            return JsonConvert.DeserializeObject<MalwareSampleResponse>(jsonResult);
        }

        public void DeleteEvent(MispEvent evnt) {
            this.DeleteEvent(evnt.Id);
        }
        public void DeleteEvent(String id) {
            try
            {
                this.Delete("/events/" + id, null);
            }
            catch (System.Net.WebException webException)
            {

            }


        }

    }
}
