using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Concrete;

namespace MvcProjeKampi.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        Context _context = new Context();
        public ActionResult Statistics()
        {
            var totalCategory = _context.Categories.Count(); // Toplam kategori sayısı
            ViewBag.totalNumberOfCategories = totalCategory;

            var softwareCategory = _context.Headings.Count(x => x.CategoryID == 8); // Başlık tablosunda "yazılım" kategorisine ait başlık sayısı
            ViewBag.softwareCategoryTitleNumber = softwareCategory;

            var writerNameSortByA = _context.Writers.Count(x => x.WriterName.Contains("a")); // Yazar adında 'a' harfi geçen yazar sayısı
            ViewBag.writerNameSortByA = writerNameSortByA;

            var mostTitles = _context.Headings.Max(x => x.Category.CategoryName); // En fazla başlığa sahip kategori adı
            ViewBag.categoryNameWithTheMostTitles = mostTitles;

            var categoryStatusTrue = _context.Categories.Count(x => x.CategoryStatus == true); // Kategori tablosunda durumu true olan kategoriler ile false olan kategoriler arasındaki sayısal fark
            ViewBag.activeCategories = categoryStatusTrue;

            return View();
        }
    }
}