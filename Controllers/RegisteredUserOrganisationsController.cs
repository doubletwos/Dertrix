﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zeus.Models;

namespace Zeus.Controllers
{
    public class RegisteredUserOrganisationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegisteredUserOrganisations
        public ActionResult Index(int? id)
        {
          
            var test1  = Session["OrgId"].ToString();


            //var registereduser = db.RegisteredUserOrganisations.FirstOrDefault(c => c.OrgId == id);

            if (id == null)
            {

                int i = Convert.ToInt32(test1);
                id = i;



            }

            return View(db.RegisteredUserOrganisations.Where(i => i.OrgId == id).ToList());


        }

        // GET: RegisteredUserOrganisations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUserOrganisation registeredUserOrganisation = db.RegisteredUserOrganisations.Find(id);
            if (registeredUserOrganisation == null)
            {
                return HttpNotFound();
            }
            return View(registeredUserOrganisation);
        }

        // GET: RegisteredUserOrganisations/Create
        public ActionResult Create()
        {
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName");
            return View();
        }

        // POST: RegisteredUserOrganisations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegisteredUserOrganisationId,RegisteredUserId,Email,OrgId")] RegisteredUserOrganisation registeredUserOrganisation)
        {
            if (ModelState.IsValid)
            {
                db.RegisteredUserOrganisations.Add(registeredUserOrganisation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", registeredUserOrganisation.OrgId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", registeredUserOrganisation.RegisteredUserId);
            return View(registeredUserOrganisation);
        }

        // GET: RegisteredUserOrganisations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUserOrganisation registeredUserOrganisation = db.RegisteredUserOrganisations.Find(id);
            if (registeredUserOrganisation == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", registeredUserOrganisation.OrgId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", registeredUserOrganisation.RegisteredUserId);
            return View(registeredUserOrganisation);
        }

        // POST: RegisteredUserOrganisations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegisteredUserOrganisationId,RegisteredUserId,Email,OrgId")] RegisteredUserOrganisation registeredUserOrganisation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registeredUserOrganisation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", registeredUserOrganisation.OrgId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", registeredUserOrganisation.RegisteredUserId);
            return View(registeredUserOrganisation);
        }

        // GET: RegisteredUserOrganisations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUserOrganisation registeredUserOrganisation = db.RegisteredUserOrganisations.Find(id);
            if (registeredUserOrganisation == null)
            {
                return HttpNotFound();
            }
            return View(registeredUserOrganisation);
        }

        // POST: RegisteredUserOrganisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisteredUserOrganisation registeredUserOrganisation = db.RegisteredUserOrganisations.Find(id);
            db.RegisteredUserOrganisations.Remove(registeredUserOrganisation);
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