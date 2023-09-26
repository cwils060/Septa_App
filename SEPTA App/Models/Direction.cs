using System;
using Newtonsoft.Json;

namespace SEPTA_App.Models
{
    public class Direction
    {
        
        public List<TrainSchedule> Northbound { get; set; }

        
        public List<TrainSchedule> Southbound { get; set; }
    }
}

