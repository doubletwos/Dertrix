﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;

namespace Dertrix.Controllers
{
    public class CalendarCategorysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CalendarCategorys
        public ActionResult Index()
        {
            return View(db.CalendarCategorys.ToList());
        }

        // GET: CalendarCategorys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarCategory calendarCategory = db.CalendarCategorys.Find(id);
            if (calendarCategory == null)
            {
                return HttpNotFound();
            }
            return View(calendarCategory);
        }

        // GET: CalendarCategorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalendarCategorys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalendarCategoryId,CategoryName")] CalendarCategory calendarCategory)
        {
            if (ModelState.IsValid)
            {
                db.CalendarCategorys.Add(calendarCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calendarCategory);
        }

        // GET: CalendarCategorys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarCategory calendarCategory = db.CalendarCategorys.Find(id);
            if (calendarCategory == null)
            {
                return HttpNotFound();
            }
            return View(calendarCategory);
        }

        // POST: CalendarCategorys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CalendarCategoryId,CategoryName")] CalendarCategory calendarCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calendarCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calendarCategory);
        }

        // GET: CalendarCategorys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarCategory calendarCategory = db.CalendarCategorys.Find(id);
            if (calendarCategory == null)
            {
                return HttpNotFound();
            }
            return View(calendarCategory);
        }

        // POST: CalendarCategorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CalendarCategory calendarCategory = db.CalendarCategorys.Find(id);
            db.CalendarCategorys.Remove(calendarCategory);
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
    }
}
