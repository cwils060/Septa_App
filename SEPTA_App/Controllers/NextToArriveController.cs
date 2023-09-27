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
    public class NextToArriveController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISeptaAPIWorker _septaWorker;
       

        public NextToArriveController(ILogger<HomeController> logger, ISeptaAPIWorker worker)
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
        public async Task<ActionResult> ReturnSchedule(NextToArriveFormResponse formResponse)
        {

            var station1 = formResponse.Station1;
            var station2 = formResponse.Station2;

            NextToArrive schedule = new NextToArrive();
            NextToArrive schedule2 = new NextToArrive();

            var response = await NextToArrive(station1, station2);

            if(response == null || response.Count < 1)
            {
                schedule = null;
            }

            if( response.Count >= 1)
            {
                schedule = response[0];
                schedule2 = response[1];
            }

            ViewBag.station1 = station1;
            ViewBag.station2 = station2;

            ViewBag.dateTime = DateTime.Now;
            ViewBag.schedule = schedule;
            ViewBag.schedule2 = schedule2;

            return View("Index");
        }

        [HttpGet]
        public async Task<List<NextToArrive>> NextToArrive(string station1, string station2)
        {
            List<NextToArrive> response = new List<NextToArrive>();

            try
            {
                response = await _septaWorker.GetNextToArriveAsync(station1, station2);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(NextToArriveController), nameof(NextToArrive));


                return response;
            }

        }
    }
}

