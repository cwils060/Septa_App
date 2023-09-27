using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEPTA_App.Models;
using SEPTA_App.Workers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEPTA_App.Controllers
{
    public class ArrivalsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISeptaAPIWorker _septaWorker;



        public ArrivalsController(ILogger<HomeController> logger, ISeptaAPIWorker worker)
        {
            _logger = logger;
            _septaWorker = worker;
            
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var stations = _septaWorker.GetStations();


            ViewBag.thirtieth = stations[0].Name;
            ViewBag.allenLane = stations[1].Name;
            ViewBag.carpenter = stations[2].Name;
            ViewBag.chelten = stations[3].Name;
            ViewBag.marketEast = stations[4].Name;
            ViewBag.queenLane = stations[5].Name;
            ViewBag.suburban = stations[6].Name;

            ViewBag.stations = stations;

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> ReturnSchedule(ArrivalFormResponse formResponse)
        {

            var stationName = formResponse.StationName;
            var selectedDirection = formResponse.SelectedDirection;

            TrainSchedule schedule = new TrainSchedule();
            TrainSchedule schedule2 = new TrainSchedule();

            var arrival = await GetArrivals(stationName);

           if (arrival.Northbound == null || arrival.Southbound == null || arrival.Northbound.Count < 1 || arrival.Northbound.Count < 1)
            {
                schedule = null;
            }
            else if (selectedDirection == "NorthBound" && arrival.Northbound != null || arrival.Northbound.Count >= 1)
            {
                schedule = arrival.Northbound[0];
                schedule2 = arrival.Northbound[1];
            }
            else if (selectedDirection == "SouthBound" && arrival.Southbound != null || arrival.Southbound.Count >=1)
            {
                schedule = arrival.Southbound[0];
                schedule2 = arrival.Southbound[1];
            }
            

            ViewBag.stationName = stationName;
            ViewBag.dateTime = DateTime.Now;
            ViewBag.schedule = schedule;
            ViewBag.schedule2 = schedule2;

            return View("Index");
        }

        [HttpGet]
        public async Task<Direction> GetArrivals(string station)
        {
            Direction response = new Direction();
           
            try
            {
                response = await _septaWorker.GetDirections(station);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(ArrivalsController), nameof(GetArrivals));


                return response;
            }

        }
    }
}

