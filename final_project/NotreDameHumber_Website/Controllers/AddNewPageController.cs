using NotreDameHumber_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class AddNewPageController : Controller
    {
        AddNewPageContext newpage = new AddNewPageContext();
        MenusContext menuContext = new MenusContext();
        // GET: AddNewPage
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Page page)
        {
            if (ModelState.IsValid)
            {
                newpage.Pages.Add(page);
                newpage.SaveChanges();

                return RedirectToAction("ShowPages");
            }
            return View(page);
        }
        public ActionResult NewPageContent(int id)
        {
            return View(newpage.Pages.Where(x => x.Id == id).First());
        }

        public ActionResult ShowPages()
        {
            return View(newpage.Pages.ToList());
        }
        public ActionResult Edit(int id)
        {
            return View(newpage.Pages.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                newpage.Entry(page).State = System.Data.Entity.EntityState.Modified;
                newpage.SaveChanges();
                return RedirectToAction("ShowPages");
            }

            return View(page);
        }
        public ActionResult Delete(int id)
        {
            return View(newpage.Pages.Find(id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Page page = newpage.Pages.Find(id);
            int? menuId = page.MenuId;
            Menu menu = menuContext.Menus.Find(menuId);
            if (menu != null)
            {
                menuContext.Menus.Remove(menu);
                menuContext.SaveChanges();
            }
            newpage.Pages.Remove(page);
            newpage.SaveChanges();

            return RedirectToAction("ShowPages");
        }
        public ActionResult AddToMenu(int id)
        {
            ViewBag.Parents = menuContext.Menus;
            Page page = newpage.Pages.Where(x => x.Id == id).First();
            ViewBag.MenuName = page.Title;
            ViewBag.PageId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToMenu([Bind(Include = "Id,MenuName,ParentId,Action,Controller")] Menu menu, string id)
        {
            int Id = Convert.ToInt32(id);
            Page page = newpage.Pages.Where(x => x.Id == Id).First();
            menu.Name = page.Title;
            menu.Action = "NewPageContent/" + id;
            menu.Controller = "AddNewPage";
            menuContext.Menus.Add(menu);
            menuContext.SaveChanges();
            page.MenuId = menu.Id;
            newpage.Entry(page).State = System.Data.Entity.EntityState.Modified;
            newpage.SaveChanges();

            return RedirectToAction("ShowPages");
        }
    }
}