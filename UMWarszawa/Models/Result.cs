using System.Collections.Generic;

namespace UMWarszawa.Models
{
    public class Result
    {
        public IList<Notification> Notifications { get; set; }
        public string ResponseDesc { get; set; }
        public string ResponseCode { get; set; }
    }
}