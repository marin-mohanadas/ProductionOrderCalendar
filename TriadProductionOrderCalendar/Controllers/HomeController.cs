using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TriadProductionOrderCalendar.Models;

namespace TriadProductionOrderCalendar.Controllers
{
    public class HomeController : Controller
    {
        private TKMDEntities db = new TKMDEntities();

        public ActionResult Index()
        {
            try
            {
                ViewBag.Tasks = db.mh_tbl_gannt_chart
                    .ToList().Select(task => new TaskViewModel
                    {
                        ProductionOrder = task.prod_order_number,
                        Start = task.order_date,
                        End = task.required_date,
                        Title = task.name + " - Project Manager: " + task.project_manager + " - Build Loc: " + task.source_location_id,
                        PercentComplete = task.perc_ready.GetValueOrDefault()
                }).AsQueryable();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return View();
        }


        public ActionResult mh_tbl_prod_Sched_Original_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.mh_tbl_prod_Sched_Original.ToList()
                        .Select(mh_tbl_prod_Sched_Original => new TaskViewModel())
                        .AsQueryable();

            return Json(data.ToDataSourceResult(request));
        }

        public virtual JsonResult mh_tbl_prod_Sched_Original_Create([DataSourceRequest] DataSourceRequest request, TaskViewModel taskVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(taskVM.Title))
                {
                    taskVM.Title = "";
                }

                var entity = taskVM.ToEntity();
                db.mh_tbl_gannt_chart.Add(entity);
                db.SaveChanges();
                taskVM.ProductionOrder = entity.prod_order_number;
            }

            return Json(new[] { taskVM }.ToDataSourceResult(request, ModelState));
        }
        public virtual JsonResult mh_tbl_prod_Sched_Original_Update([DataSourceRequest] DataSourceRequest request, TaskViewModel taskVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(taskVM.Title))
                {
                    taskVM.Title = "";
                }

                var entity = taskVM.ToEntity();
                db.mh_tbl_gannt_chart.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { taskVM }.ToDataSourceResult(request, ModelState));
        }
        public virtual JsonResult mh_tbl_prod_Sched_Original_Destroy([DataSourceRequest] DataSourceRequest request, TaskViewModel taskVM)
        {
            if (ModelState.IsValid)
            {
                var entity = taskVM.ToEntity();
                db.mh_tbl_gannt_chart.Attach(entity);
                db.mh_tbl_gannt_chart.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { taskVM }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}