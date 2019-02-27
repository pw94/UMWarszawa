using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UMWarszawa.Converters;

namespace UMWarszawa.Models
{
    public class Notification
    {
        public string Category { get; set; }
        public string City { get; set; }
        public string Subcategory { get; set; }
        public string District { get; set; }
        public string AparmentNumber { get; set; }
        public string Street2 { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationType NotificationType { get; set; }
        [JsonConverter(typeof(MyDateTimeConverter))]
        public DateTime CreateDate { get; set; }
        public string SiebelEventId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Source Source { get; set; }
        public decimal YCoordOracle { get; set; }
        public string Street { get; set; }
        public string DeviceType { get; set; }
        public IList<Status> Statuses { get; set; }
        public decimal XCoordOracle { get; set; }
        public string NotificationNumber { get; set; }
        public string Event { get; set; }
        public decimal YCoordWGS84 { get; set; }
        public decimal XCoordWGS84 { get; set; }
    }
}