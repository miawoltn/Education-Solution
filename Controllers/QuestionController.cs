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
using Education_Solution.Models;

namespace Education_Solution.Controllers
{
   [RoutePrefix("api")]
    public class QuestionController : ApiController
    {
        private EducationDbModel db = new EducationDbModel();
       
        // GET api/Question
       
       // [Authorize]
        [Route("q")]
       
        public IQueryable<Question> GetQuestion()
        {
            return db.Question;
        }

        // GET api/q/owner/paper
        [ResponseType(typeof(Question))]
        //[Authorize]
        [Route("q/{owner}/{questionName}")]
        public IHttpActionResult GetQuestion(string owner, string questionName)
        {
            var question = db.Question.Where(x => x.Owner == owner && x.QuestionName == questionName);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [ResponseType(typeof(Question))]
       // [Authorize]
        [Route("q/{owner}/{paper}/{questionName}")]
        public IHttpActionResult GetQuestion(string owner, string paper, string questionName)
        {
            var question = db.Question.Where(x => x.Owner == owner && x.Paper == paper && x.QuestionName == questionName);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT api/Question/5
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }

            db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/q
        [Route("q")]
        [ResponseType(typeof(Question))]
        [Authorize]
        [HttpPost]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //checking if the owner of the questions is provided
            if (question.Owner == "" || question.Owner == null)
                question.Owner = User.Identity.Name;

            //setting the paper field to the owners name if empty
            if (question.Paper == null || question.Paper == "")
                question.Paper = question.Owner;
            
            db.Question.Add(question);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (QuestionExists(question.Id))
                {
                    return Conflict();
                }
                else if(db.Question.Where(x => x.Owner == question.Owner && x.Paper == question.Paper && x.QuestionName  == question.QuestionName) != null)
                {
                    return BadRequest("Conflict: Paper and Question Id Must be unique");
                }
            }
            var baseUri = "api/q";
            if (question.Paper == question.Owner)
                return Created(baseUri + "/" + question.Owner + "/" + question.QuestionName, question); //CreatedAtRoute("DefaultApi", new { controller = "GetQuestion", owner = question.Owner, questionName = question.QuestionName }, question);
            else
                return Created(baseUri + "/" + question.Owner + "/" + question.Paper + "/" + question.QuestionName, question); //CreatedAtRoute("DefaultApi", new { controller = "GetQuestion", owner = question.Owner, paper = question.Paper, questionName = question.QuestionName }, question);
        }

        // DELETE api/Question/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(string id)
        {
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.Question.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Question.Count(e => e.Id == id) > 0;
        }
    }
}