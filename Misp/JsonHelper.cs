
namespace Misp
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal static class JsonHelper
    {

        static JsonHelper()
        {
            settings = new JsonSerializerSettings();
            settings.DateFormatString = "YYYY-MM-DD";
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DefaultValueHandling = DefaultValueHandling.Ignore;
            settings.Formatting = Formatting.None;
            settings.StringEscapeHandling = StringEscapeHandling.Default;            
        }

        private static JsonSerializerSettings settings;

        internal static JsonSerializerSettings Settings { get { return JsonHelper.settings; } }
    }

    internal class DateConverter1 : DateTimeConverterBase
    {
        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType);
        }
        public override bool CanRead
        {
            get
            {
                return base.CanRead;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return base.CanWrite;
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
