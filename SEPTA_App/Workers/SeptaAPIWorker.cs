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

    }
}

