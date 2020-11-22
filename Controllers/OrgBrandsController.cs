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
    public class OrgBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgBrands
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        
            return View(db.OrgBrands.ToList());
        }

        // GET: OrgBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            if (orgBrand == null)
            {
                return HttpNotFound();
            }
            return View(orgBrand);
        }

        // GET: OrgBrands/Create
        public ActionResult Create()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View();
        }

        // POST: OrgBrands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( OrgBrand orgBrand, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Logo,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    orgBrand.Files = new List<File> { avatar };
                }



                db.OrgBrands.Add(orgBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orgBrand);
        }

        // GET: OrgBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            OrgBrand orgbrand = db.OrgBrands.Include(s => s.Files).SingleOrDefault(s => s.OrgBrandId == id);

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            if (orgBrand == null)
            {
                return HttpNotFound();
            }

         


            return View(orgBrand);
        }

        // POST: OrgBrands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrgBrand orgBrand, HttpPostedFileBase upload)
        {
           

            if (ModelState.IsValid)
            {
                var orgBrandInDb = db.OrgBrands.Include(f => f.Files).Single(c => c.OrgBrandId == orgBrand.OrgBrandId);
                orgBrandInDb.OrgBrandName = orgBrand.OrgBrandName;
                orgBrandInDb.OrgBrandBar = orgBrand.OrgBrandBar;
                orgBrandInDb.OrgNavigationBar = orgBrand.OrgNavigationBar;
                orgBrandInDb.OrgNavBarTextColour = orgBrand.OrgNavBarTextColour;
                orgBrandInDb.OrgBrandButtonColour = orgBrand.OrgBrandButtonColour;

                if (upload != null && upload.ContentLength > 0)
                {
                    if (orgBrandInDb.Files.Any(f => f.FileType == FileType.Logo))
                    {
                        db.Files.Remove(orgBrandInDb.Files.First(f => f.FileType == FileType.Logo));
                    }
                    var logo = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Logo,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        logo.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    orgBrandInDb.Files = new List<File> {logo};
                }


                db.Entry(orgBrandInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgBrand);
        }

        // GET: OrgBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            if (orgBrand == null)
            {
                return HttpNotFound();
            }
            return View(orgBrand);
        }

        // POST: OrgBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            db.OrgBrands.Remove(orgBrand);
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
