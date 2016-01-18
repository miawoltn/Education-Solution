using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Education_Solution.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace Education_Solution.Controllers
{
    public class HomeController : Controller
    {
        EducationDbModel Educationdb = new EducationDbModel();
        QuestionRESTService service = new QuestionRESTService();
        
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ViewQuestions()
        {
            return View(service.GetQuestions());
        }

        public ActionResult GetQuestion(string Id)
        {
            return View(service.GetQuestion(Id));
        }

        public ActionResult PostQuestion()
        {           
            return View();           
        }

        [HttpPost]
        public async Task<ActionResult> PostQuestion(Question question)
        {
            HttpResponseMessage response = await service.PostQuestion(question);
            return RedirectToAction("GetQuestion", new { id = question.Id });
            
        }

        [HttpGet]
        public ActionResult DeleteQuestion(string id)
        {
            return View(Educationdb.Question.Find(id));
        }

        
        [HttpPost, ActionName("DeleteQuestion")]
        public ActionResult DeleteTheQuestion(string id)
        {
            service.DeleteQuestion(id);
           return Redirect("/Home/ViewQuestions");
        }
    }

    public class QuestionRESTService
    {
        readonly string baseUri = "http://localhost:44109//api/";

        public List<Question> GetQuestions()
        {
            string uri = baseUri + "Questions";
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return new List<Question>();
            }
        }

        public Question GetQuestion(string id)
        {
            string uri = baseUri  +"Question/"+ id;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Question>(response.Result)).Result;
            }
        }

        public Task<HttpResponseMessage> PostQuestion(Question question)
        {
            string uri = baseUri  + "Question";
            HttpClient httpClient = new HttpClient();
            
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(question);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(uri,content);
                return response;          
        }

        public Task<HttpResponseMessage> DeleteQuestion(string id)
        {
            string uri = baseUri + "Question/" + id;
            HttpClient httpClient = new HttpClient();
                var response = httpClient.DeleteAsync(uri);
                return response;
        }
    }
}
