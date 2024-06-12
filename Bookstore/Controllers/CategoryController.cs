using Bookstore.Data;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
       public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categoryOpjects = _context.Categories.ToList();
            return View(categoryOpjects);
        }
        //-----------------------Create Action ------------------------------------------
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //===============================================================================//

        //-----------------------Edit Action --------------------------------------------
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            //her three way to get object data from database 
            Category categoryFromDB = _context.Categories.Find(id);
            //Category categoryFromDB2 = _context.Categories.FirstOrDefault(u=> u.Id == id);
            //Category categoryFromDB3 = _context.Categories.Where(u=> u.Id == id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"]= "Category Edit successfully";
                return RedirectToAction("Edit");
            }
            return View(obj);
        }
        //===============================================================================//
        //-----------------------Delete Action -------------------------------------------
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //her three way to get object data from database 
            Category categoryFromDB = _context.Categories.Find(id);
            //Category categoryFromDB2 = _context.Categories.FirstOrDefault(u=> u.Id == id);
            //Category categoryFromDB3 = _context.Categories.Where(u=> u.Id == id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost ,ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? deleteCategory = _context.Categories.Find(Id);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(deleteCategory);
            _context.SaveChanges();
            TempData["success"] = "Category delete successfully";
            return RedirectToAction("Index");
        }
        //===============================================================================//
    }
}
