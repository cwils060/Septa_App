using System;
using Newtonsoft.Json;

namespace SEPTA_App.Models
{
	public class NextToArrive
	{

        [JsonProperty("orig_train")]

        public string OrigTrain { get; set; }

        [JsonProperty("orig_line")]

        public string OrigLine { get; set; }

        [JsonProperty("orig_departure_time")]

        public string OrigDepartureTime { get; set; }

        [JsonProperty("orig_arrival_time")]

        public string OrigArrivalTime { get; set; }

        [JsonProperty("orig_delay")]

        public string OrigDelay { get; set; }
	}
}

