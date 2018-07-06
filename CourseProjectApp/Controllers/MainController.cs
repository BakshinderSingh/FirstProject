using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProjectApp.Entities;
using CourseProjectApp.Models;
using CourseProjectApp.Services;
using CourseProjectApp.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectApp.Controllers
{
   // [Route("[Controller]")]
    public class MainController : Controller
    {
        private IMyInjectedService myService;
        private ApplicationDbContext _myContext;
        public MainController(IMyInjectedService myService,ApplicationDbContext context)
        {
            this.myService = myService;
            this._myContext = context;
        }
        //[Route("[Action]")]
        public IActionResult Index()
        {
            //return new JsonResult("{bakshinder:sethi}");
            ViewBag.value = "My First MVC Rout!!";
            ViewBag.myobject = this.myService;
            SeedData.Seed(this._myContext);
            return View();
        }

        //[Route("[Action]")]
        public IActionResult FirstView()
        {
            //var model = new MyData { MyId = 5, MyValue = "My View" };
            var model = new List<MyData>
            {
                new MyData{MyId=1,MyValue="First"},
                new MyData{MyValue="Second", MyId=2}
            };
            var viewmodel = new FirstViewViewModel();
            viewmodel.MyType = model;
            return View(viewmodel);
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

            var model = new List<MyData>
            {
                new MyData{MyId=1,MyValue="First"},
                new MyData{MyValue="Second", MyId=2}
            };
            return new ObjectResult(mymodel);
        } 
    }
}