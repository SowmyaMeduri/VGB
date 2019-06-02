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
    public class OrdersController : Controller
    {
        private VGBEntities db = new VGBEntities();

        // GET: Orders
        public ActionResult Index()
        {
           // var orders = db.Orders.Include(o => o.Orders1).Include(o => o.Order1);
            return View(/*orders.ToList()*/);
        }

        // GET: Orders/Details/5
        public ActionResult OrderDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        ////// GET: Orders/Create
        ////public ActionResult Create()
        ////{
        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId");
        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId");
        ////    return View();
        ////}

        ////// POST: Orders/Create
        ////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ////// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Create([Bind(Include = "OrderId,ordersInvoiceNo,Rsize,Gsm,Colour,NetWt,Type,BagSize,RollStatus,Date,Remarks,PartyName,PLNo,DCNo")] Order order)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        db.Orders.Add(order);
        ////        db.SaveChanges();
        ////        return RedirectToAction("Index");
        ////    }

        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId", order.ordersInvoiceNo);
        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId", order.ordersInvoiceNo);
        ////    return View(order);
        ////}

        ////// GET: Orders/Edit/5
        ////public ActionResult Edit(string id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    Order order = db.Orders.Find(id);
        ////    if (order == null)
        ////    {
        ////        return HttpNotFound();
        ////    }
        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId", order.ordersInvoiceNo);
        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId", order.ordersInvoiceNo);
        ////    return View(order);
        ////}

        ////// POST: Orders/Edit/5
        ////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ////// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Edit([Bind(Include = "OrderId,ordersInvoiceNo,Rsize,Gsm,Colour,NetWt,Type,BagSize,RollStatus,Date,Remarks,PartyName,PLNo,DCNo")] Order order)
        ////{
        ////    //if (ModelState.IsValid)
        ////    //{
        ////    //    db.Entry(order).State = EntityState.Modified;
        ////    //    db.SaveChanges();
        ////    //    return RedirectToAction("Index");
        ////    //}
        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId", order.ordersInvoiceNo);
        ////    ViewBag.ordersInvoiceNo = new SelectList(db.Orders, "ordersInvoiceNo", "OrderId", order.ordersInvoiceNo);
        ////    return View(order);
        ////}

        ////// GET: Orders/Delete/5
        ////public ActionResult Delete(string id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    Order order = db.Orders.Find(id);
        ////    if (order == null)
        ////    {
        ////        return HttpNotFound();
        ////    }
        ////    return View(order);
        ////}

        ////// POST: Orders/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult DeleteConfirmed(string id)
        ////{
        ////    Order order = db.Orders.Find(id);
        ////    db.Orders.Remove(order);
        ////    db.SaveChanges();
        ////    return RedirectToAction("Index");
        ////}

        ////protected override void Dispose(bool disposing)
        ////{
        ////    if (disposing)
        ////    {
        ////        db.Dispose();
        ////    }
        ////    base.Dispose(disposing);
        ////}
    }
}
