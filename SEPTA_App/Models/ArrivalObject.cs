using System;
using Newtonsoft.Json;
using SEPTA_App.Models;

namespace SEPTA_App
{
	
       
        public class ArrivalObject
        {
            public Dictionary<string, Direction> Arrivals{ get; set; }
        }
}

