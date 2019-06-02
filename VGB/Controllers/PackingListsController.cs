using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VGB.Models;

namespace VGB.Controllers
{
    public class PackingListsController : Controller
    {
        const string status = "Close";
        private VGBEntities db = new VGBEntities();

        // GET: PackingLists
        public ActionResult Index()
        {
            return View(new List<PackingList>());
        }

        // GET: PackingLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackingList packingList = db.PackingLists.Find(id);
            if (packingList == null)
            {
                return HttpNotFound();
            }
            return View(packingList);
        }

        // GET: PackingLists/Create
        public ActionResult Create()
        {
            PackingList objDept = new PackingList();
            return PartialView("CreatepackingList", objDept);
        }

        // POST: PackingLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,S_No,Item,Roll_No,Colour,Gsm,Width,Output,NetWt,Remarks")] PackingList packingList)
        {
            if (ModelState.IsValid)
            {
                db.PackingLists.Add(packingList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(packingList);
        }

        // GET: PackingLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackingList packingList = db.PackingLists.Find(id);
            if (packingList == null)
            {
                return HttpNotFound();
            }
            return View(packingList);
        }

        // POST: PackingLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,S_No,Item,Roll_No,Colour,Gsm,Width,Output,NetWt,Remarks")] PackingList packingList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(packingList).State = System.Data.Entity.EntityState.Modified;
        db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(packingList);
        }

        // GET: PackingLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackingList packingList = db.PackingLists.Find(id);
            if (packingList == null)
            {
                return HttpNotFound();
            }
            return View(packingList);
        }

        // POST: PackingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackingList packingList = db.PackingLists.Find(id);
            db.PackingLists.Remove(packingList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult PackingListData(int? id)
        {
           
            db.Configuration.ProxyCreationEnabled = false;
            if (id==0 || id==null)
            {
                TempData["pakingLists"] = db.PackingLists.ToList();
                return View(db.PackingLists.ToList());
            }
            else
            {
                List<PackingList> pakingData = db.PackingLists.Where(x => x.JobWorkId == id).ToList();
                TempData["pakingLists"] = pakingData;
                return View(pakingData);
            }
           
        }

        public ActionResult GetpakingList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var pakings = TempData["pakingLists"] as List<PackingList>;
            return Json(new { data = pakings }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeRollStatus(int id)
        {
            List<PackingList> pakingData = db.PackingLists.Where(x => x.Id == id).ToList();
            if(pakingData.Count > 1)
            {
                foreach(var packingStatus in pakingData)
                {
                    packingStatus.RollStatus = status;
                    db.PackingLists.Attach(packingStatus);
                    db.SaveChanges();
                }

            }
            else
            {
                var data = pakingData.FirstOrDefault();
                data.RollStatus = status;
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
             
                db.PackingLists.Attach(pakingData.FirstOrDefault());
                db.SaveChanges();
            }

            return RedirectToAction("Index","JobWorks");
        }
    }
}
