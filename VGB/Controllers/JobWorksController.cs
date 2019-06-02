using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using VGB.Models;
using PagedList;
using PagedList.Mvc;

namespace VGB.Controllers
{
    public class JobWorksController : Controller
    {
        private VGBEntities db = new VGBEntities();
        public decimal? netWeights = 0;
        public int totalRolls;
        public bool isPackingListCreated = false;

        // GET: JobWorks
        public ActionResult Index(string name, int? pageNo)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View(db.JobWorks.ToList().ToPagedList(pageNo ?? 1, 3));
            }
            else
            {
                return View(db.JobWorks.Where(x => x.CustomerName.StartsWith(name) || string.IsNullOrEmpty(name)).ToList().ToPagedList(pageNo ?? 1, 3));
            }

        }

        // GET: JobWorks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobWork jobWork = db.JobWorks.Find(id);
            if (jobWork == null)
            {
                return HttpNotFound();
            }
            return View(jobWork);
        }

        // GET: JobWorks/Create
        public ActionResult Create()
        {
            JobWork jobWork = new JobWork();
            jobWork.Date = DateTime.Now;
            if (TempData["Weights"] != null && TempData["rolls"] != null)
            {
                jobWork.NetWt = TempData["Weights"]?.ToString();
                jobWork.TotalRolls = TempData["rolls"]?.ToString();
            }
            ModelState.Clear();
            return View(jobWork);
        }

        public ActionResult ExportToExcel(bool? isProductCard, bool? isDeliveryStatus)
        {
            var fileName = "INWARDMATERIALSTATUS_JOBWORK.xls";
            if (isProductCard ?? false)
            {
                fileName = "ProductionCard.xls";
            }
            if (isDeliveryStatus ?? false)
            {
                fileName = "DeliverySttaus.xls";
            }
            var gv = new GridView();
            using (VGBEntities vgb = new VGBEntities())
            {
                gv.DataSource = db.JobWorks.ToList();
            }
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View(db.JobWorks.ToList());
        }

        ////public ActionResult PrintAllReport()
        ////{

        ////    var report = new ActionAsPdf("Index");
        ////    return report;
        ////}
        // POST: JobWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Created_By,Date,CustomerName,Date,NetWt,TotalRolls,PLNo,DCNO,InvoiceNo")] JobWork jobWork)
        {
            if (ModelState.IsValid)
            {


                db.JobWorks.Add(jobWork);
                var customers = TempData["PakingList"];
                ProductionCard productionCard = new ProductionCard();
                productionCard.Created_By = jobWork.Created_By;
                // productionCard.BagSize = jobWork.PackingList.BagSize;
                //productionCard.Colour = jobWork.Colour;
                productionCard.Date = jobWork.Date;
                productionCard.DCNO = jobWork.DCNO;
                //productionCard.Gsm = jobWork.Gsm;
                // productionCard.Kgs = jobWork.NetWt;
                productionCard.partyName = jobWork.CustomerName;
                productionCard.PLNo = jobWork.PLNo;
                //productionCard.Remarks = jobWork.Remarks;
                // productionCard.Rsize = jobWork.Rsize;
                // productionCard.status = jobWork.RollStatus;
                // productionCard.Type = jobWork.Type;
                productionCard.JobWorkId = jobWork.JobWorkId;
                db.ProductionCards.Add(productionCard);
                List<PackingList> x = customers as List<PackingList>;
                foreach (var z in x)
                {
                    z.JobWorkId = jobWork.JobWorkId;
                    db.PackingLists.Add(z);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobWork);
        }

        // GET: JobWorks/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.JobWorks.Where(x => x.JobWorkId == id).FirstOrDefault());
        }

        // POST: JobWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Created_By,Rsize,Gsm,Colour,NetWt,Type,BagSize,RollStatus,Date,Remarks,PartyName,PLNo,DCNO,InvoiceNo")] JobWork jobWork)
        {
            try
            {
                //db.JobWorks.Attach(jobWork);
                db.Entry(jobWork).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }

        }

        //// GET: JobWorks/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    JobWork work = db.JobWorks.Where(x => x.JobWorkId == id).FirstOrDefault();
        //    return View(work);
        //}

        //// POST: JobWorks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    JobWork jobWork = db.JobWorks.Find(id);
        //    ProductionCard productionCard = db.ProductionCards.Where(x => x.JobWorkId == id).ToList().FirstOrDefault();
        //    db.ProductionCards.Remove(productionCard);
        //    List<PackingList> packingList = db.PackingLists.Where(y => y.JobWorkId == id).ToList();
        //    if (packingList.Count > 0)
        //    {
        //        foreach (var paking in packingList)
        //        {
        //            db.PackingLists.Remove(paking);
        //        }
        //    }
        //    db.JobWorks.Remove(jobWork);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult InsertCustomers(List<PackingList> customers)
        {
            decimal? Weights = 0;

            using (VGBEntities entities = new VGBEntities())
            {
                //Loop and insert records.
                foreach (PackingList customer in customers)
                {
                    customer.RollStatus = "Open";
                    customer.RollStatus.Trim();
                    Weights = Weights + customer.NetWt;
                    //entities.PackingLists.Add(customer);
                }
                TempData["Weights"] = Weights;
                TempData["PakingList"] = customers;
                Session["PakingList"] = customers;
                //TempData["rolls"] = entities.SaveChanges();
                TempData["rolls"] = customers.Count;
                return Json(Weights);

                // return RedirectToAction("newpage", "ProductionCard");
            }
        }

        [HttpGet]
        public PartialViewResult GetDeletePartial(int id)
        {
            var deleteItem = db.JobWorks.Find(id);  // I'm using 'Items' as a generic term for whatever item you have

            return PartialView("Delete", deleteItem);  // assumes your delete view is named "Delete"
        }

        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var jobworks = db.JobWorks.ToList<JobWork>();
            return Json(new { data = jobworks }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
                JobWork emp = db.JobWorks.Where(x => x.JobWorkId == id).FirstOrDefault<JobWork>();
                db.JobWorks.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}
