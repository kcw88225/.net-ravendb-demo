using RavenDbHelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RavenDbHelloWorld.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var models = this.GetAllData();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]        
        public void Create()
        {
            var schedule = new DemoModel
            {
                Message = "Created",
                CreatedTime = DateTime.Now
            };

            this.DocumentSession.Store(schedule);
        }


        [HttpPost]
        public void DeleteAll()
        {
            var models = this.GetAllData();

            models.ForEach(m => {
                this.DocumentSession.Delete<DemoModel>(m);
            });
        }

        private List<DemoModel> GetAllData() 
        {
            return this.DocumentSession.Query<DemoModel>().OrderByDescending(i => i.CreatedTime).ToList();
        }
    }
}
