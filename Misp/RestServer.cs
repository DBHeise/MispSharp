using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Misp
{
    public abstract class RestServer
    {
        protected Uri baseUrl;
        public RestServer(String baseUri)
        {
            if (!Uri.TryCreate(baseUri, UriKind.Absolute, out this.baseUrl)) {
                throw new ArgumentException("Not a valid URI", nameof(baseUri));
            }
        }

        protected virtual HttpWebRequest NewRequest(String relativeUri, String verb)
        {
            Uri rUri = new Uri(this.baseUrl, relativeUri);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rUri);
            request.Method = verb;
            return request;
        }

        protected String Do(String relativeUri, String verb, String data)
        {
            HttpWebRequest request = this.NewRequest(relativeUri, verb);

            if (!String.IsNullOrWhiteSpace(data))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(dataBytes, 0, dataBytes.Length);
                reqStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            String body = reader.ReadToEnd();
            response.Close();
            resStream.Close();

            return body;
        }

        protected String Post(String relativeUri, String data) { return this.Do(relativeUri, "POST", data); }
        protected String Put(String relativeUri, String data) { return this.Do(relativeUri, "PUT", data); }
        protected String Get(String relativeUri) { return this.Do(relativeUri, "GET", null); }
        protected String Delete(String relativeUri, String data) { return this.Do(relativeUri, "DELETE", data); }
    }
}
