using System;
using Newtonsoft.Json;

namespace SEPTA_App.Models
{
    public class TrainSchedule
    {

        [JsonProperty("direction")]
        public string direction { get; set; }

        [JsonProperty("path")]
        public string path { get; set; }

        [JsonProperty("train_id")]
        public string train_id { get; set; }

        [JsonProperty("origin")]
        public string origin { get; set; }

        [JsonProperty("destination")]
        public string destination { get; set; }

        [JsonProperty("line")]
        public string line { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("service_type")]
        public string service_type { get; set; }

        [JsonProperty("next_station")]
        public object next_station { get; set; }

        [JsonProperty("sched_time")]
        public DateTime sched_time { get; set; }

        [JsonProperty("depart_time")]
        public DateTime depart_time { get; set; }

        [JsonProperty("track")]
        public string track { get; set; }

        [JsonProperty("track_change")]
        public object track_change { get; set; }

        [JsonProperty("platform")]
        public string platform { get; set; }

        [JsonProperty("platform_change")]

        public object platform_change { get; set; }
    }
}

