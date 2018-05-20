using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProjectApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectApp.Controllers
{
   // [Route("[Controller]")]
    public class MainController : Controller
    {
        //[Route("[Action]")]
        public IActionResult Index()
        {
            //return new JsonResult("{bakshinder:sethi}");
            ViewBag.value = "My First MVC Rout!!";
            return View();
        }

        //[Route("[Action]")]
        public IActionResult FirstView()
        {
            var model = new MyData { MyId = 5, MyValue = "My View" };
            return View(model);
        }

        //[Route("[Action]")]
        public ContentResult IdRoute()
        {
            return Content("Attribuet");
        }

        //[Route("[Action]")]
        public ObjectResult MyObject()
        {
            var mymodel = new MyData { MyId=1,MyValue="MyFirstValue"};
            return new ObjectResult(mymodel);
        } 
    }
}