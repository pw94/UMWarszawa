using System;
using Newtonsoft.Json;
using UMWarszawa.Converters;

namespace UMWarszawa.Models
{
    public class Status
    {
        [JsonProperty(PropertyName = "Status")]
        public string StatusName { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(MyDateTimeConverter))]
        public DateTime ChangeDate { get; set; }
    }
}