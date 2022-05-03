using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.ViewModels;


namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private IToDoTaskRepository _taskRepository;
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ToDoListController(IToDoTaskRepository taskRep, ICategoryRepository catRep, IMapper mapper)
        {
            _taskRepository = taskRep;
            _categoryRepository = catRep;
            _mapper = mapper;
        }

        [HttpGet]
        public ViewResult Index(int categoryId)
        {
            ToDoTaskViewModelPage viewModelPage = new ToDoTaskViewModelPage();
            var currentTasks = _taskRepository.ListItems(isDone: false, categoryId: categoryId);
            var completedTasks = _taskRepository.ListItems(isDone: true, categoryId: categoryId);
            var categories = _categoryRepository.GetAllCategories();

            viewModelPage.CurrentTasks = _mapper.Map<List<ToDoTaskViewModel>>(currentTasks);
            viewModelPage.CompletedTasks = _mapper.Map<List<ToDoTaskViewModel>>(completedTasks);
            viewModelPage.Categories = _mapper.Map<List<CategoryViewModel>>(categories);
            viewModelPage.CurrentCategory = categoryId;
            return View("Index", viewModelPage);
        }
        [HttpPost]
        public IActionResult Create(ToDoTaskCreateViewModel task)
        {

            if (ModelState.IsValid)
            {
                var taskModel = _mapper.Map<ToDoTaskModel>(task);
                bool res = _taskRepository.Create(taskModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool res = _taskRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MarkAsDone(int id)
        {
            if (ModelState.IsValid)
            {
                bool res = _taskRepository.SetDoneStatus(id, true);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MarkAsNotDone(int id)
        {
            if (ModelState.IsValid)
            {
                bool res = _taskRepository.SetDoneStatus(id, false);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var pageViewModel = new ToDoTaskEditViewModelPage();

                var taskDetails = _taskRepository.GetTask(id);
                var categories = _categoryRepository.GetAllCategories();
                pageViewModel.ToDoTaskViewModel = _mapper.Map<ToDoTaskEditViewModel>(taskDetails);
                pageViewModel.Categories = _mapper.Map<List<CategoryViewModel>>(categories);
                return View("Edit", pageViewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(ToDoTaskEditViewModel task)
        {

            if (ModelState.IsValid)
            {
                var taskModel = _mapper.Map<ToDoTaskModel>(task);
                bool res = _taskRepository.Update(taskModel);
                Debug.WriteLine(res);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", task);

            }
                return RedirectToAction("Edit", task);
        }
    }
}
