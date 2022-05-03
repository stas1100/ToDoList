using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.ViewModels;
using AutoMapper;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper; 
        public CategoriesController(ICategoryRepository catRep, IMapper mapper)
        {
            _categoryRepository = catRep;
            _mapper = mapper;
        }
        [HttpGet]
        public ViewResult Index()
        {
            CategoriesViewModelPage viewModel = new CategoriesViewModelPage();
            viewModel.Categories = _categoryRepository.GetAllCategories();

            return View("Categories", viewModel);
        }
        [HttpPost]
        public ActionResult Delete(CategoryDeleteViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = _mapper.Map<CategoryModel>(category);
                bool res = _categoryRepository.Delete(categoryModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(CategoryCreateViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = _mapper.Map<CategoryModel>(category);
                bool res = _categoryRepository.Create(categoryModel);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = _categoryRepository.GetCategory(id);
                var viewModdelPage = _mapper.Map<CategoryViewModel>(categoryModel);
                return View("Edit", viewModdelPage);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = _mapper.Map<CategoryModel>(category);
                var res = _categoryRepository.Update(categoryModel);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", category);

            }
            return RedirectToAction("Edit", category);
        }
    }
}
