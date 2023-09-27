using System;
using Newtonsoft.Json;
using SEPTA_App.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace SEPTA_App.Workers
{
    public class SeptaAPIWorker : ISeptaAPIWorker
    {
        public async Task<List<NextToArrive>> GetNextToArriveAsync(string station1, string station2)
        {
            var nextToArrive = new List<NextToArrive>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www3.septa.org/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/NextToArrive/index.php?req1=" + station1 + "&req2=" + station2);

                response.EnsureSuccessStatusCode();

                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    //var mockJsonString = "[{\"orig_train\":\"9827\",\"orig_line\":\"Chestnut Hill West\",\"orig_departure_time\":\"12:48PM\",\"orig_arrival_time\":\"1:10PM\",\"orig_delay\":\"On time\",\"isdirect\":\"true\"},{\"orig_train\":\"9831\",\"orig_line\":\"Chestnut Hill West\",\"orig_departure_time\":\"1:48PM\",\"orig_arrival_time\":\"2:10PM\",\"orig_delay\":\"On time\",\"isdirect\":\"true\"},{\"orig_train\":\"9835\",\"orig_line\":\"Chestnut Hill West\",\"orig_departure_time\":\"2:48PM\",\"orig_arrival_time\":\"3:10PM\",\"orig_delay\":\"On time\",\"isdirect\":\"true\"},{\"orig_train\":\"7821\",\"orig_line\":\"Chestnut Hill West\",\"orig_departure_time\":\"3:47PM\",\"orig_arrival_time\":\"4:06PM\",\"orig_delay\":\"On time\",\"isdirect\":\"true\"}]";

                    var formatJsonString = jsonString.Remove(0, 1).TrimEnd(']').Split('}').ToList();
                    var shortenArray = ShortenResponseArray(formatJsonString);
                    var removeCommas = TrimCommasFromResponseArray(shortenArray);
                    var updatedArray = ReturnJsonArray(removeCommas);
                    nextToArrive = CreateNextToArrive(updatedArray);

                    return nextToArrive;

                }

                return nextToArrive;
            }
        }

        public async Task<Direction> GetDirections(string station)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www3.septa.org/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Arrivals/index.php?station=" + station);

                response.EnsureSuccessStatusCode();

                if (response != null)
                {

                    var JsonString = await response.Content.ReadAsStringAsync();


                    //var mockJsonString = "\n{\"Queen Lane Departures: September 17, 2023, 6:14 pm\":[{\"Northbound\":[{\"direction\":\"N\",\"path\":\"R8\\/3N\",\"train_id\":\"8360\",\"origin\":\"Chestnut Hill West\",\"destination\":\"West Trenton\",\"line\":\"Chestnut Hill West\",\"status\":\"On Time\",\"service_type\":\"LOCAL\",\"next_station\":null,\"sched_time\":\"2023-09-17 19:32:00.000\",\"depart_time\":\"2023-09-17 19:32:00.000\",\"track\":\"2\",\"track_change\":null,\"platform\":\"\",\"platform_change\":null},{\"direction\":\"N\",\"path\":\"R8N\",\"train_id\":\"9872\",\"origin\":\"Chestnut Hill West\",\"destination\":\"Temple U\",\"line\":\"Chestnut Hill West\",\"status\":\"On Time\",\"service_type\":\"LOCAL\",\"next_station\":null,\"sched_time\":\"2023-09-17 21:32:00.000\",\"depart_time\":\"2023-09-17 21:32:00.000\",\"track\":\"2\",\"track_change\":null,\"platform\":\"\",\"platform_change\":null}]},{\"Southbound\":[{\"direction\":\"S\",\"path\":\"R3\\/8S\",\"train_id\":\"3855\",\"origin\":\"West Trenton\",\"destination\":\"Chestnut H West\",\"line\":\"West Trenton\",\"status\":\"On Time\",\"service_type\":\"LOCAL\",\"next_station\":\"Jefferson\",\"sched_time\":\"2023-09-17 18:42:00.000\",\"depart_time\":\"2023-09-17 18:42:00.000\",\"track\":\"1\",\"track_change\":null,\"platform\":\"\",\"platform_change\":null},{\"direction\":\"S\",\"path\":\"R3\\/8S\",\"train_id\":\"3863\",\"origin\":\"West Trenton\",\"destination\":\"Chestnut H West\",\"line\":\"West Trenton\",\"status\":\"On Time\",\"service_type\":\"LOCAL\",\"next_station\":null,\"sched_time\":\"2023-09-17 20:42:00.000\",\"depart_time\":\"2023-09-17 20:42:00.000\",\"track\":\"1\",\"track_change\":null,\"platform\":\"\",\"platform_change\":null}]}]}";

                    //var mockNullJsonRespone = "{\"Queen Lane Departures: September 19, 2023, 12:23 am\":[[],[]]}";

                    //char[] trimList = { '\\', 'n', '{', ':', '"' };



                    //NorthBound//
                    var arrivalNorthBoundLists = JsonString.Split("[")[2];
                    var northBoundArray = arrivalNorthBoundLists.Split("}").ToList();
                    var shortNorthBound = ShortenResponseArray(northBoundArray);
                    var formattedNorthBound = TrimCommasFromResponseArray(shortNorthBound);
                    var northBoundTrainScheduleArray = ReturnJsonArray(formattedNorthBound);

                    //SouthBound//
                    var arrivalSouthBoundLists = JsonString.Split("[")[3];
                    var southBoundArray = arrivalSouthBoundLists.Split("}").ToList();
                    var shortSouthBound = ShortenResponseArray(southBoundArray);
                    var formattedSouthBound = TrimCommasFromResponseArray(shortSouthBound);
                    var southBoundTrainScheduleArray = ReturnJsonArray(formattedSouthBound);

                    //Return NorthBound Train Schedules//
                    var trainSchedulesN = CreateTrainSchedules(northBoundTrainScheduleArray);

                    //Return SouthBound Train Schedules//
                    var trainSchedulesS = CreateTrainSchedules(southBoundTrainScheduleArray);

                    Direction directions = new Direction();

                    directions.Northbound = trainSchedulesN;
                    directions.Southbound = trainSchedulesS;

                    return directions;

                }

                return new Direction();
            }
        }

        public List<string> ShortenResponseArray(List<string> scheduleLists)
        {
            List<string> shortenedArray = new List<string>();

            foreach (var item in scheduleLists)
            {
                if (item.Length > 50)
                {
                    shortenedArray.Add(item);
                }

            }

            return shortenedArray;
        }

        public List<string> TrimCommasFromResponseArray(List<string> scheduleLists)
        {
            List<string> valuesWithCommas = new List<string>();
            List<string> response = new List<string>();

            foreach (var item in scheduleLists)
            {

                if (item[0] == ',')
                {
                    valuesWithCommas.Add(item);
                }
                else
                {
                    response.Add(item);
                }
            }

            foreach (var value in valuesWithCommas)
            {
                var trimmed = value.Substring(1, value.Length - 1);
                response.Add(trimmed);
            }

            return response;
        }

        public List<string> ReturnJsonArray(List<string> array)
        {
            List<string> formattedString = new List<string>();

            foreach (var value in array)
            {

                var udpatedValue = value + "}";
                formattedString.Add(udpatedValue);
            }

            return formattedString;
        }

        public List<TrainSchedule> CreateTrainSchedules(List<string> arrays)
        {
            List<TrainSchedule> trainSchedules = new List<TrainSchedule>();

            TrainSchedule schedule = new TrainSchedule();

            foreach (var item in arrays)
            {
                schedule = JsonConvert.DeserializeObject<TrainSchedule>(item);

                trainSchedules.Add(schedule);
            }

            return trainSchedules;
        }

        public List<NextToArrive> CreateNextToArrive(List<string> arrays)
        {
            List<NextToArrive> nextToArrive = new List<NextToArrive>();

            NextToArrive schedule = new NextToArrive();

            foreach (var item in arrays)
            {
                schedule = JsonConvert.DeserializeObject<NextToArrive>(item);


                nextToArrive.Add(schedule);
            }

            return nextToArrive;
        }

        public List<Station> GetStations()
        {
            var result = new List<Station>();

             Station ThirtiethStreet = new Station()
             {
                 Id = 1,
                 Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/30th_Street_Station_east_entrance_from_PA_3_WB.jpeg/300px-30th_Street_Station_east_entrance_from_PA_3_WB.jpeg",
                 Name = "30th Street Station",
                 Description = "30th Street Station, officially William H. Gray III 30th Street Station, is a major intermodal transit station in Philadelphia, Pennsylvania, United States. It is metropolitan Philadelphia's main railroad station and a major stop on Amtrak's Northeast and Keystone corridors. It was named in memory of U.S. representative William H. Gray III in 2020.The station is also a major commuter rail station served by all SEPTA Regional Rail lines and is the western terminus for NJ Transit's Atlantic City Line. The station is also served by several SEPTA city and suburban buses and by NJ Transit, Amtrak Thruway, and intercity operators."
             };

           Station AllensLane = new Station()
            {
                Id = 2,
                Image = "https://assets1.cbsnewsstatic.com/i/cbslocal/wp-content/uploads/sites/15116066/2011/05/allen-lane-bridge-mcdev.jpg",
                Name = "Allen Lane",
                Description = "Richard Allen Lane station (formerly Allen Lane station) is a SEPTA Regional Rail station in Philadelphia. It is located at 200 West Allens Lane in the Mount Airy neighborhood and serves the Chestnut Hill West Line. The station building was built circa 1880, according to the Philadelphia Architects and Buildings project. Like many in Philadelphia, it retains much of its Victorian/Edwardian appearance.The former station building now houses a coffee shop, the High Point Cafe."
            };

              Station Carpenter = new Station()
             {
                 Id = 3,
                 Image = "https://upload.wikimedia.org/wikipedia/commons/4/44/Carpenter_Lane_SEPTA.JPG",
                 Name = "Carpenter",
                 Description = "Carpenter station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located at 201 Carpenter Lane, it serves the Chestnut Hill West Line.\nThe historic station building has been listed in the Philadelphia Register of Historic Places since August 6, 1981.It is in zone 2 on the Chestnut Hill West Line, on former Pennsylvania Railroad tracks, and is 9.8 track miles from Suburban Station. In fiscal 2012, this station saw 371 boardings on an average weekday."
             };

              Station Chelten = new Station()
             {
                 Id = 4,
                 Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Chelten_Avenue_Station.jpg/300px-Chelten_Avenue_Station.jpg",
                 Name = "Chelten Avenue",
                 Description = "Chelten Avenue station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located on West Chelten Avenue in the Germantown neighborhood, it serves the Chestnut Hill West Line. The concrete station structure, part of a Pennsylvania Railroad grade-separation project completed in 1918 in conjunction with electrification of the line, was designed by William Holmes Cookman.\n\nA station has been at this location since June 11, 1884. Known initially as Germantown, the 1918 station was named Chelten Avenue to avoid confusion with the Philadelphia & Reading Railroad's Germantown."
             };

             Station Jefferson = new Station()
             {
                 Id = 5,
                 Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Train_emerging_from_the_Center_City_Commuter_Connection_at_the_Market_East_Station%2C_Philadelphia_PA.jpg/300px-Train_emerging_from_the_Center_City_Commuter_Connection_at_the_Market_East_Station%2C_Philadelphia_PA.jpg",
                 Name = "Market East",
                 Description = "Jefferson Station (formerly named Market East Station) is an underground SEPTA Regional Rail station located on Market Street in Philadelphia, Pennsylvania. It is the easternmost of the three Center City stations of the SEPTA Regional Rail system and is part of the Center City Commuter Connection, which connects the former Penn Central commuter lines with the former Reading Company commuter lines. In 2014, the station saw approximately 26,000 passengers every weekday."
             };

            Station QueenLane = new Station()
                  {
                      Id = 6,
                      Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f1/Queen_Lane_SEPTA.JPG/300px-Queen_Lane_SEPTA.JPG",
                      Name = "Queen Lane",
                      Description = "Queen Lane station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located at 5319 Wissahickon Avenue facing West Queen Lane, it serves the Chestnut Hill West Line.\n\nThe station is 7.4 miles (11.9 km) from Suburban Station. In 2004, this station saw 470 boardings on an average weekday. It was built for the Philadelphia, Germantown and Chestnut Hill Railroad, a subsidiary of the Pennsylvania Railroad, in 1885 to a design by Washington Bleddyn Powell."
                  };

            Station Suburban = new Station()
            {
                Id = 7,
                Image = "https://www.inquirer.com/resizer/sMLUk9etP8Znm_3sYtB1G0jPsus=/700x467/smart/filters:format(webp)/arc-anglerfish-arc2-prod-pmn.s3.amazonaws.com/public/P5BSWEX2VFBKVF72QJBEAWPNDU.jpg",
                Name = "Suburban Station",
                Description = "Suburban Station is an art deco office building and underground commuter rail station in Penn Center in Philadelphia. Its official SEPTA address is 16th Street and JFK Boulevard. The station is owned and operated by SEPTA and is one of the three core Center City stations on the SEPTA Regional Rail and one of the busiest stations in the Regional Rail System."
            };

            result.Add(ThirtiethStreet);
            result.Add(AllensLane);
            result.Add(Carpenter);
            result.Add(Chelten);
            result.Add(Jefferson);
            result.Add(QueenLane);
            result.Add(Suburban);

            return result;
    }

 
    }
}

