using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumProject1.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace ForumProject1.Controllers
{
    public class HomeController : Controller
    {
        ForumProjectEntities2 db = new ForumProjectEntities2();
        MultiModel multiModelObj = new MultiModel();

        private int pageSize = 10;
        private int pageIndex = 1;

        public ActionResult Index(int? page)
        {
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<QuestionTable> qListDB = db.QuestionTables.OrderByDescending(x => x.QuestionId).ToList();
            List<QuestionTableModel> qModelList = qListDB.Select(x => new QuestionTableModel
            {
                UserId = x.UserId,
                name = x.UserRegisteration.Name,
                QuestionTitle = x.QuestionTitle,
                QuestionDesc = x.QuestionDesc,
                QuestionId = x.QuestionId,
                LangName = x.LangTable.LangName
            }).ToList();

            multiModelObj.QuestionPagedList = qModelList.ToPagedList(pageIndex, pageSize);
            multiModelObj.QuestionModelList = qModelList;
            return View(multiModelObj);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MultiModel obj)
        {
            if (ModelState.IsValid == true)
            {
                db.procRegister(obj.UserModel.Name, obj.UserModel.Email, obj.UserModel.Password,
                    DateTime.Now.ToString("yyyy-MM-dd"));
            }
            return RedirectToAction("Index");
        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public ActionResult Login(MultiModel obj)
        {
            UserRegisteration user =
                db.UserRegisterations.FirstOrDefault(
                    x => String.Compare(x.Email, obj.Login.Username, StringComparison.Ordinal) == 0);
            if (user != null)
            {
                if (String.Compare(user.Password, obj.Login.Password, StringComparison.Ordinal) == 0)
                {
                    Session["date"] = user.RegisterDate;
                    Session["name"] = user.Name;
                    Session["email"] = user.Email;
                    Session["id"] = user.UserId;
                    System.Web.Security.FormsAuthentication.SetAuthCookie(obj.Login.Username, false);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            return RedirectToAction("Index");
        }

        public ActionResult PostQuestion()
        {
            if (Session["id"] != null)
            {
                List<LangTable> langTablesList = db.LangTables.ToList();
                ViewBag.langList = new SelectList(langTablesList, "LangId", "LangName");
            }
            else
            {
                Response.Write("<script>alert('You must log in to post question')</script>");
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult PostQuestion(MultiModel obj)
        {
            if (Session["id"] != null)
            {
                List<LangTable> list = db.LangTables.ToList();
                ViewBag.langList = new SelectList(list, "LangId", "LangName");
                QuestionTable tableModel = new QuestionTable();
                tableModel.UserId = (int) Session["id"];
                if (obj.QuestionModel.LangId != null) tableModel.LangId = (int) obj.QuestionModel.LangId;
                tableModel.QuestionTitle = obj.QuestionModel.QuestionTitle;
                tableModel.QuestionDesc = obj.QuestionModel.QuestionDesc;
                db.QuestionTables.Add(tableModel);
                db.SaveChanges();
                ModelState.Clear();
                Response.Write("<script>alert('Your question has been successfully posted')</script>");
            }
            else
            {
                Response.Write("<script>alert('You must log in to post question')</script>");
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /*for bringing all the answers from database*/
        public ActionResult Solution(int qId)
        {
            QuestionTableModel question = new QuestionTableModel();

            QuestionTable questionDb = db.QuestionTables.SingleOrDefault(y => y.QuestionId == qId);
            List<AnswerTable> answerDb = db.AnswerTables.Where(x => x.QuestionId == qId).ToList();

            List<AnswerTableModel> answer = answerDb.Select(x => new AnswerTableModel
            {
                AnswerDesc =  x.AnswerDesc,
                UserId = x.UserId,
                name = x.UserRegisteration.Name
            }).ToList();

            question.QuestionId = questionDb.QuestionId;
            question.QuestionTitle = questionDb.QuestionTitle;
            question.QuestionDesc = questionDb.QuestionDesc;
            question.LangId = questionDb.LangId;

            multiModelObj.QuestionModel = question;
            multiModelObj.AnswerModelList = answer;
            return View(multiModelObj);
        }
    }
}