using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogTemp.Models;
using NotreDameHumber_Website.Models;

namespace BlogTemp.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blogs.Where(b => b.Publish == true).ToList());
        }

        //GET: both comments and blog in one view
        

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            List<Comment> comments = db.Comments.Where(m => m.BlogId == id).ToList();
            if (blog == null)
            {
                return HttpNotFound();
            }
            var model = new BlogViewModel();
            model.Blogs = blog;
            model.Comments = comments;

            ViewBag.CommentSubmit = "";
            if(Session["CommentSubmit"] != null)
            {
                ViewBag.CommentSubmit = Session["CommentSubmit"];
            }

            Session["blogId"] = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Details(FormCollection formCollection)
        {
            Comment comment = new Comment();

            comment.Name = formCollection["Name"];
            comment.Email = formCollection["Email"];
            comment.Content = formCollection["Content"];
            comment.Publish = false;
            comment.BlogId = Convert.ToInt32(Session["blogId"]);

            db.Comments.Add(comment);
            db.SaveChanges();

            Session["CommentSubmit"] = "Comment sent. Awaiting approval.";

            return RedirectToAction("Details", Session["blogId"]);
        }

        public PartialViewResult MakeBlogComment()
        {           
            return PartialView("_blog_make_comments");
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            if(Session["admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Subject,Image,Body,Publish,PublishDate")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Blog blog = db.Blogs.Find(id);
                if (blog == null)
                {
                    return HttpNotFound();
                }
                return View(blog);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Subject,Image,Body,Publish,PublishDate")] Blog blog)
        {
            if (Session["admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(blog).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(blog);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Blog blog = db.Blogs.Find(id);
                if (blog == null)
                {
                    return HttpNotFound();
                }
                return View(blog);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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

        //GET: Admin
        public ActionResult Admin()
        {
            if (Session["admin"] != null)
            {
                return View(db.Blogs.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(BlogAdmin blogAdmin)
        {
            var admn = db.BlogAdmins.Where(a => a.Username == blogAdmin.Username && a.Password == blogAdmin.Password).FirstOrDefault();
            if(admn != null)
            {
                Session["admin"] = blogAdmin.Username.ToString();
                return RedirectToAction("Admin");
            }
            else
            {
                ModelState.AddModelError("", "Username or password were incorrect");
            }
            return View();
        }

        
        public ActionResult Logout()
        {
            ViewBag.LogoutMessage = "Logging you out";
            if(Session["admin"] != null)
            {
                Session.Abandon();
            }

            return RedirectToAction("Login");
        }

        //MODERATE COMMENTS
        [HttpGet]
        public ActionResult Comments()
        {
            if(Session["admin"]!=null)
            {
                return View(db.Comments.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpGet]
        public ActionResult SingleComment(int? id)
        {
            if(Session["admin"]!= null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                Session["c_id"] = id;
                return View(comment);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult RemoveComment(int? id)
        {
            if (Session["admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                return View(comment);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult SingleComment(FormCollection formCollection)
        {
            
            if (Session["admin"] != null)
            {
                Comment comment = db.Comments.Find(Session["c_id"]);
                if (ModelState.IsValid)
                {                    
                    comment.Content = formCollection["Content"];
                    comment.Publish = Convert.ToBoolean(formCollection["Publish"]);
                    comment.Name = formCollection["Name"];
                    comment.Email = formCollection["Email"];

                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Comments");
                }
                return View(comment);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost, ActionName("RemoveComment")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCommentConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Comments");
        }
    }
}
