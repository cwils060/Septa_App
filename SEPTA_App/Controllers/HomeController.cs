using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEPTA_App.Models;
using SEPTA_App.Workers;


namespace SEPTA_App.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISeptaAPIWorker _septaWorker;
   


    public HomeController(ILogger<HomeController> logger, ISeptaAPIWorker worker)
    {
        _logger = logger;
        _septaWorker = worker;
        
    }

    public IActionResult Index()
    {
       var stations = _septaWorker.GetStations();

        ViewBag.thirtieth = stations[0];
        ViewBag.allenLane = stations[1];
        ViewBag.carpenter = stations[2];
        ViewBag.chelten = stations[3];
        ViewBag.marketEast = stations[4];
        ViewBag.queenLane = stations[5];


        ViewBag.stations = stations;

        return View();
    }


    public IActionResult Arrivals()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}