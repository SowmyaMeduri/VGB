using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VGB.Models;

namespace VGB.Controllers
{
    public class ProductionCardController : Controller
    {
        const string status = "Close";
        private VGBEntities db = new VGBEntities();
        // GET: ProductionCard
        public ActionResult Index(int? pageNo, string searchText)
        {
            var totalWeight = 0;
            var closedWeight = 0;
            var remainingWeight = 0;
            bool isSearchApplied=false;
            if (string.IsNullOrEmpty(searchText))
            {
                return View(db.ProductionCards.ToList().ToPagedList(pageNo ?? 1, 3));
            }
            else
            {
                var clientResults = db.ProductionCards.Where(x => x.partyName.ToUpper().StartsWith(searchText.ToUpper()) || string.IsNullOrEmpty(searchText.ToUpper())).ToList().ToPagedList(pageNo ?? 1, 3).ToList();
                foreach(var res in clientResults)
                {
                    totalWeight = totalWeight+Convert.ToInt32(res.Kgs);
                    if (res.status.Trim() == status.Trim())
                    {
                        closedWeight = closedWeight + Convert.ToInt32(res.Kgs);
                    }
                    isSearchApplied = true;
                    remainingWeight = totalWeight - closedWeight;
                }
                ViewBag.TotalWeight = totalWeight;
                ViewBag.ClosedWeight = closedWeight;
                ViewBag.Bool = isSearchApplied;
                ViewBag.remainingWeight = remainingWeight;
               // var clientResults.Where(x => x.status == "Close").Select(x => x.Kgs);
                return View(db.ProductionCards.Where(x => x.partyName.ToUpper().StartsWith(searchText.ToUpper()) || string.IsNullOrEmpty(searchText.ToUpper())).ToList().ToPagedList(pageNo ?? 1, 3));
            }
        }

        // GET: ProductionCard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductionCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductionCard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ExportToExcel()
        {
            bool? isProductCard = true;
            return RedirectToAction("ExportToExcel", "JobWorks", new { isProductCard = isProductCard });
        }
        // GET: ProductionCard/Edit/5
        public ActionResult Edit(int id)
        {
            ProductionCard card = new ProductionCard();
            ViewBag.Teachers = card;
            return View(db.ProductionCards.Where(x => x.productionId == id).FirstOrDefault());
        }

        // POST: ProductionCard/Edit/5
        [HttpPost]
        public ActionResult Edit( ProductionCard production,int id)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(production).State = System.Data.Entity.EntityState.Modified;
                    //db.ProductionCards.Add(productionCard);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(production);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult PopUp(int id, ProductionCard production)
        {
            IList<JobWork> jobWork = db.JobWorks.Select(x => x).ToList();
            IList<ProductionCard> productionCard = db.ProductionCards.Where(x => x.productionId == id).ToList();
            int jobworkId = productionCard.FirstOrDefault().JobWorkId;

            foreach (var product in productionCard)
            {
                foreach (var jobDetails in jobWork)
                {
                    JobWork job = jobDetails as JobWork;
                    if (job.JobWorkId == jobworkId)
                    {
                        db.JobWorks.Attach(job);
                      //  job.RollStatus= status;
                        db.SaveChanges();
                        break;
                    }
                }
                if (product.productionId == id)
                {
                    db.ProductionCards.Attach(product);
                    product.status= status;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        // GET: ProductionCard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductionCard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
