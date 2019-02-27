using System;
using Newtonsoft.Json;

namespace UMWarszawa.Converters
{
    public class MyDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = (long)reader.Value;
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dt = (DateTime) value;
            var miliseconds = dt.Ticks/10000;
            writer.WriteValue(miliseconds);
        }
    }
}