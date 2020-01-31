using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(myspecies);
        }
        // Show
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.SqlQuery("select * from species where speciesid=@SpeciesID", new SqlParameter("@SpeciesID", id)).FirstOrDefault();
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }
        // Add
        public ActionResult Add()
        {
            return View();
        }
        // [HttpPost] Add
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            string query = "insert into species (Name) values (@SpeciesName)";

            SqlParameter sqlparams = new SqlParameter("@SpeciesName", SpeciesName);
            

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        // Update
        public ActionResult Update(int id)
        {
            Species selSpecies = db.Species.SqlQuery("select * from species where speciesid = @SpeciesId", new SqlParameter("@SpeciesID", id)).FirstOrDefault();
            return View(selSpecies);
        }
        // [HttpPost] Update
        [HttpPost]
        public ActionResult Update(string speciesName, int id)
        {
            
            string query = "update species set Name = @speciesName where SpeciesId = " + id;
            SqlParameter sqlparams = new SqlParameter("@speciesName", speciesName); 
           
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        // Show
        // Add
        // [HttpPost] Add
        // Update
        // [HttpPost] Update
        // (optional) delete
        // [HttpPost] Delete
        public ActionResult Delete(int id)
        {
            
            string query = "delete from species where SpeciesId= @id";
            SqlParameter parameter = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, parameter);

            return RedirectToAction("List");
        }
    }
}
