using Cinema.Data;
using Cinema.Models;
using Cinema.Repository.Interfaces;
using Cinema.Tools;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    //[Authorize]
    public class MovieController : Controller
    {

        private readonly IMovie _service;
        private readonly IActor _serviceActor;

        public MovieController(IMovie service, IActor serviceActor)
        {
            _service = service;
            _serviceActor = serviceActor;
        }

        private List<SelectListItem> GetActors()
        {
            var listActors = new List<SelectListItem>();

            PaginatedList<Actor> actors = _serviceActor.GetAll("FullName", SortOrder.Ascending, "", 1, 100);

            listActors = actors.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.FullName
            }).ToList();

            var item = new SelectListItem()
            {
                Value = "",
                Text = "---select---"
            };

            listActors.Insert(0, item);

            return listActors;
        }
        public IActionResult Index(string sortExpression = "", string searchText = "", int pg = 1, int pageSize = 3)
        {
            PaginatedList<Movie> movies;

            SortModel sortModel = new SortModel();

            sortModel.AddColumn("title");
            sortModel.AddColumn("actor");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.searchText = searchText;

            movies = _service.GetAll(sortModel.SortedProperty, sortModel.SortedOrder, searchText, pg, pageSize);

            var pager = new Pager(movies.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            return View(movies);
        }

        public IActionResult Create()
        {
            Movie movie = new Movie();

            ViewBag.Actors = GetActors();

            return View(movie);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            try
            {
                _service.Add(movie);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Movie movie = _service.GetById(id);

            return View(movie);
        }

        public IActionResult Edit(int id)
        {
            Movie movie = _service.GetById(id);

            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            try
            {
                _service.Update(movie);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Movie movie = _service.GetById(id);

            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            try
            {
                _service.Delete(movie);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult WriteComment(CommentViewModel model)
        {
            var comment = new Comment();

            comment.CommentItem = model.CommentItem;

            comment.MovieId = model.MovieId;

            comment.UserId = User.Identity.Name;

            comment.CreateDate = DateTime.Now;

            return View(comment);
        }
    }
}
