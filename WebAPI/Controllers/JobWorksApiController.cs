using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class JobWorksApiController : ApiController
    {
        private VGBEntities db = new VGBEntities();

        // GET: api/JobWorksApi
        public IQueryable<JobWork> GetJobWorks()
        {
            return db.JobWorks;
        }

        // GET: api/JobWorksApi/5
        [ResponseType(typeof(JobWork))]
        public IHttpActionResult GetJobWork(int id)
        {
            JobWork jobWork = db.JobWorks.Find(id);
            if (jobWork == null)
            {
                return NotFound();
            }

            return Ok(jobWork);
        }

        // PUT: api/JobWorksApi/5
       [HttpPut]
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutJobWork(int id, JobWork jobWork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobWork.JobWorkId)
            {
                return BadRequest();
            }

            db.Entry(jobWork).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobWorkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(); 
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/JobWorksApi
        [ResponseType(typeof(JobWork))]
        public IHttpActionResult PostJobWork(JobWork jobWork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobWorks.Add(jobWork);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobWork.JobWorkId }, jobWork);
        }

        // DELETE: api/JobWorksApi/5
        [ResponseType(typeof(JobWork))]
        public IHttpActionResult DeleteJobWork(int id)
        {
            JobWork jobWork = db.JobWorks.Find(id);
            if (jobWork == null)
            {
                return NotFound();
            }

            db.JobWorks.Remove(jobWork);
            db.SaveChanges();

            return Ok(jobWork);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobWorkExists(int id)
        {
            return db.JobWorks.Count(e => e.JobWorkId == id) > 0;
        }
    }
}